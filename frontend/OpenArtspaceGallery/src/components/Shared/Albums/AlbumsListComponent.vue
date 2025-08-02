<script setup lang="ts">
  import AlbumComponent from "./AlbumComponent.vue";
  import {onMounted, PropType, ref} from "vue";
  import {DecodeAlbumDto} from "../../../ts/Albums/libAlbums.ts";
  import type {Album} from "../../../ts/Albums/libAlbums.ts";
  import {WebClientSendGetRequest} from "../../../ts/libWebClient.ts";
  import AlbumsHierarchyComponent from "../../Albums/AlbumsHierarchyComponent.vue";
  import LoadingSymbolComponent from "../LoadingSymbol/LoadingSymbolComponent.vue";
  import AddNewAlbumComponent from "../SelectedMenu/AddNewAlbumComponent.vue";

  defineExpose({
    RefreshAsync: RefreshAlbumsListAsync
  })

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: false
    }
  })

  const isLoading = ref<boolean>(true)

  const albums = ref<Album[]>([])

  const emit = defineEmits(["createAlbum", "uploadImage"])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    albums.value = await GetAlbumsListAsync(props.currentAlbumId)

    isLoading.value = false
  }

  async function GetAlbumsListAsync(currentAlbumId: String | undefined | null): Promise<Album[]>
  {
    let albumsList: Album[]

    if (currentAlbumId === undefined || currentAlbumId === null)
    {
      // Showing root albums' children
      albumsList = (await (await WebClientSendGetRequest("/Albums/TopLevel")).json())
          .albums
          .map(DecodeAlbumDto)
    }
    else
    {
      // Showing given albums children
      albumsList = (await (await WebClientSendGetRequest("/Albums/ChildrenOf/" + currentAlbumId)).json())
          .albums
          .map(DecodeAlbumDto)
    }

    return albumsList
        .sort(function(a: Album, b: Album) { return a.creationTime.getTime() - b.creationTime.getTime() })
  }

  async function RefreshAlbumsListAsync(): Promise<void>
  {
    isLoading.value = true

    albums.value = await GetAlbumsListAsync(props.currentAlbumId)

    isLoading.value = false
  }

  async function OnAlbumDeletedAsync(): Promise<void>
  {
    await RefreshAlbumsListAsync()
  }

  async function OnAlbumRenamedAsync(): Promise<void>
  {
    await RefreshAlbumsListAsync()
  }

  async function OnCreateAlbumActionAsync(): Promise<void>
  {
    emit("createAlbum")
  }

</script>

<template>

  <LoadingSymbolComponent v-if="isLoading" />

  <div v-if="!isLoading">

    <AlbumsHierarchyComponent :albumId="props.currentAlbumId"/>

    <div v-if="albums.length === 0">
      No albums
    </div>

    <div class="albums-container">

      <AddNewAlbumComponent
          @createAlbum="async () => await OnCreateAlbumActionAsync()"/>

      <AlbumComponent
          v-for="album in albums"
          :key="album.id"
          :info="album"
          @albumDeleted="async () => await OnAlbumDeletedAsync()"
          @albumRenamed="async () => await OnAlbumRenamedAsync()"/>

    </div>

  </div>

</template>
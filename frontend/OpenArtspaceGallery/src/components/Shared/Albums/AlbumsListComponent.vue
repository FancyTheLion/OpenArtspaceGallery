<script setup lang="ts">
  import AlbumComponent from "./AlbumComponent.vue";
  import {onMounted, PropType, ref} from "vue";
  import {DecodeAlbumDto} from "../../../ts/Albums/libAlbums.ts";
  import type {Album} from "../../../ts/Albums/libAlbums.ts";
  import {WebClientSendGetRequest} from "../../../ts/libWebClient.ts";
  import AlbumsHierarchyComponent from "../../Albums/AlbumsHierarchyComponent.vue";
  import NewAlbumComponent from "./NewAlbumComponent.vue";
  import LoadingSymbolComponent from "../LoadyngSymbol/LoadingSymbolComponent.vue";
  import AddContentComponent from "../SelectedMenu/AddContentComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    },
    shouldRefresh: Boolean
  })

  const isLoading = ref<boolean>(true)

  const albums = ref<Album[]>([])

  const neverShovComponent = ref<boolean>(false)

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

  async function GetAlbumsListAsync(currentAlbumId: String | undefined | null)
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

  async function RefreshAlbumsListAsync()
  {
    isLoading.value = true

    albums.value = await GetAlbumsListAsync(props.currentAlbumId)

    isLoading.value = false
  }

  async function OnAlbumAddedAsync()
  {
    alert("REFRESH")

    await RefreshAlbumsListAsync()
  }

  async function OnAlbumDeletedAsync()
  {
    await RefreshAlbumsListAsync()
  }

  async function OnAlbumRenamedAsync()
  {
    await RefreshAlbumsListAsync()
  }

  async function AlbumCreated()
  {
    emit("createAlbum")
  }

  async function UploadImage()
  {
    emit("uploadImage")
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

      <AlbumComponent
          v-for="album in albums"
          :key="album.id"
          :info="album"
          @albumDeleted="async () => await OnAlbumDeletedAsync()"
          @albumRenamed="async () => await OnAlbumRenamedAsync()"/>


      <!-- For refresh   -->
      <NewAlbumComponent
          v-if="neverShovComponent"
          :currentAlbumId="props.currentAlbumId"
          @newAlbumCreated="async () => await OnAlbumAddedAsync()"/>

      <AddContentComponent
          :currentAlbumId="props.currentAlbumId"
          @createAlbum="async () => await AlbumCreated()"
          @uploadImage="async () => await UploadImage()"/>

    </div>

  </div>

</template>
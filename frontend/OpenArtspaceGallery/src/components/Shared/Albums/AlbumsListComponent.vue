<script setup lang="ts">
  import AlbumComponent from "./AlbumComponent.vue";
  import {onMounted, PropType, ref} from "vue";
  import {Album, DecodeAlbumDto } from "../../../ts/Albums/libAlbums.ts";
  import {WebClientSendGetRequest} from "../../../ts/libWebClient.ts";
  import AlbumsHierarchyComponent from "../../Albums/AlbumsHierarchyComponent.vue";
  import NewAlbumComponent from "./NewAlbumComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string | null>,
      required: false
    }
  })

  const isLoading = ref<boolean>(true)
  const albums = ref<Album[]>([])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    albums.value = await GetAlbumsList(props.currentAlbumId)

    isLoading.value = false
  }

  async function GetAlbumsList(currentAlbumId: String | undefined | null)
  {
    let albumsList: Album[]

    if (currentAlbumId === undefined || currentAlbumId === null)
    {
      // Showing root albums's children
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
</script>

<template>

  <div v-if="!isLoading">

    <AlbumsHierarchyComponent :albumId="props.currentAlbumId"/>

    <div
        v-if="albums.length === 0">
      Альбомов нет
    </div>

    <div class="albums-container">

      <AlbumComponent
          v-for="album in albums"
          :key="album.id"
          :info="album" />

      <NewAlbumComponent/>

    </div>



  </div>

</template>
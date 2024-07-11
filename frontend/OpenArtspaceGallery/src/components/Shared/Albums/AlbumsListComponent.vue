<script setup lang="ts">
  import AlbumComponent from "./AlbumComponent.vue";
  import {onMounted, ref} from "vue";
  import {Album, DecodeAlbumDto } from "../../../ts/Albums/libAlbums.ts";
  import {WebClientSendGetRequest} from "../../../ts/libWebClient.ts";

  const props = defineProps({
    parentAlbumId: String
  })

  const isLoading = ref<boolean>(true)
  const albums = ref<Album[]>([])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    albums.value = await GetAlbumsList(props.parentAlbumId)

    isLoading.value = false
  }

  async function GetAlbumsList(parentAlbumId: String | undefined)
  {
    let albumsList: Album[]

    if (parentAlbumId === undefined)
    {
      // Showing root album's children
      albumsList = (await (await WebClientSendGetRequest("/Albums/TopLevel")).json())
          .albums
          .map(DecodeAlbumDto)
    }
    else
    {
      // Showing given album children
      albumsList = (await (await WebClientSendGetRequest("/Albums/ChildrenOf/" + parentAlbumId)).json())
          .albums
          .map(DecodeAlbumDto)
    }

    return albumsList
        .sort(function(a: Album, b: Album) { return a.creationTime.getTime() - b.creationTime.getTime() })
  }
</script>

<template>

  <div v-if="!isLoading">

    <a :href="'/albums/' + props.parentAlbumId">

      <div v-if="props.parentAlbumId !== undefined">
        Ссылка на родительский альбом
      </div>

    </a>

    <div
        v-if="albums.length === 0">
      Альбомов нет
    </div>

    <div class="albums-container">

      <AlbumComponent
          v-for="album in albums"
          :key="album.id"
          :info="album" />

    </div>

  </div>

</template>
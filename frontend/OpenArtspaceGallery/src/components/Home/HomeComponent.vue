<script setup lang="ts">
  import {onMounted, ref} from 'vue'
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import LoadingSymbol from "../LoadingSymbolComponent.vue";
  import AlbumComponent from "../Shared/Albums/AlbumComponent.vue";
  import {Album} from "../../ts/Albums/libAlbums.ts";

  const isLoading = ref<boolean>(true)
  const albums = ref<Album[]>([])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    albums.value = await GetAlbumList()

    isLoading.value = false
  }

  async function GetAlbumList()
  {
    return (await (await WebClientSendGetRequest("/Albums/TopLevel")).json())
        .albums
        .sort(function(a: Album, b: Album) { return a.creationTime > b.creationTime })
  }

</script>

<template>

  <LoadingSymbol v-if="isLoading"/>

  <div v-if="!isLoading">

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
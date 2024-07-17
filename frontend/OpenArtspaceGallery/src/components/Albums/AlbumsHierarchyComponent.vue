<script setup lang="ts">
import {onMounted, PropType, ref} from "vue";
import LoadingSymbolComponent from "../Shared/LoadingSymbolComponent.vue";
import {AlbumHierarchy, DecodeAlbumHierarchyDto} from "../../ts/Albums/libAlbums.ts";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";

  const props = defineProps({
    albumId: {
      type: String as PropType<string | null>,
      required: false
    }
  })

  const isLoading = ref<boolean>(true)

  const albumsHierarchy = ref<AlbumHierarchy[]>([])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    if (props.albumId !== undefined && props.albumId !== null)
    {
      albumsHierarchy.value = (await (await WebClientSendGetRequest("/Albums/Hierarchy/" + props.albumId)).json())
          .albumHierarchy
          .map(DecodeAlbumHierarchyDto)
    }

    isLoading.value = false
  }

</script>

<template>

  <LoadingSymbolComponent v-if="isLoading" />

  <div v-if="!isLoading">

    <div v-if="props.albumId === null || props.albumId === undefined">
      <!-- For case when ID is not provided -->
      Home
    </div>

    <div v-else>
      <!-- For case when ID is provided -->
      <a href="/" title="Back to home page">
        Home
      </a>

      <span v-if="albumsHierarchy.length > 0">
        &gt;&gt;

        <span v-for="album in albumsHierarchy" :key="album.id">

           <span v-if="album.id !== albumsHierarchy[albumsHierarchy.length - 1].id">

            <a :href="'/albums/' + album.id" :title="'Go to album ' + album.name">
              {{ album.name }}
            </a>

           &gt;&gt;

          </span>

          <span v-else>
            {{ album.name }}
          </span>

        </span>

      </span>

    </div>

  </div>

</template>
<script setup lang="ts">

import {onMounted, PropType, ref} from "vue";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
import {DecodeImageDto, DecodeImagesResponse, Image} from "../../ts/Images/libImages.ts";

const props = defineProps({
  currentAlbumId: {
    type: String as PropType<string>,
    required: false
  }
})

const images = ref<Image[]>([])

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
    images.value = await GetImagesListAsync(props.currentAlbumId)
}

async function GetImagesListAsync(currentAlbumId: String | undefined)
{
  return DecodeImagesResponse((await (await WebClientSendGetRequest("/Images/ByAlbum/" + currentAlbumId)).json()))
      .images
      .map(DecodeImageDto)
      .sort((a: Image, b: Image) => a.creationTime.getTime() - b.creationTime.getTime())
}

</script>

<template>

  <div class="albums-container">

    <div
    v-if="images.length === 0">
      Images not found
    </div>

    <div
    v-for="image in images"
    :key="image.id">

      <div> {{ image.name }} </div>

    </div>

  </div>

</template>
<script setup lang="ts">

import {onMounted, ref} from "vue";
import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";

const imagesSizes = ref<ImageSize[]>([])

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
  imagesSizes.value = await GetImagesSizesList()

  for (let index = 0; index < imagesSizes.value.length; index++) {
    alert(imagesSizes.value[index].name);
  }
}

async function GetImagesSizesList()
{
    return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetImagesSizesList")).json()))
      .imagesSizes
      .map(DecodeImageSizeDto)
}

</script>

<template>

  <div class="admin-Panel-Images-Sizes-conteiner">

    <div class="admin-Panel-Images-Sizes-header">
      Images sizes
    </div>

  </div>

</template>
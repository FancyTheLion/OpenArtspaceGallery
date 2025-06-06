<script setup lang="ts">

import {Image} from "../../ts/Images/libImages.ts";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
import {onMounted, ref} from "vue";

const { image } = defineProps<{ image: Image }>()

const thumbnail = ref<string | undefined >(undefined)

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
  thumbnail.value = await GetPreview(image.thumbnailId)
}

async function GetPreview(previewId: string)
{
  const response = await WebClientSendGetRequest("/Files/" + previewId);

  const blob = await response.blob();

  return URL.createObjectURL(blob);

}

</script>

<template>

  <div class="thumbnail-container">

    <img
        :src="thumbnail ?? '/public/images/icons/imageNotFound.webp'"
        alt="Preview"
        class="thumbnail-image"/>

    <div class="thumbnail-name">
      {{ image.name }}
    </div>

  </div>
</template>
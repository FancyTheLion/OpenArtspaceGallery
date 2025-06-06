<script setup lang="ts">

import {Image} from "../../ts/Images/libImages.ts";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
import {onMounted, ref} from "vue";

const { image } = defineProps<{ image: Image }>()

const defaultThumbnail = '/public/images/icons/imageNotFound.webp';

const thumbnail = ref<string | undefined >(undefined)

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
  thumbnail.value = await GetPreview(image.thumbnailId)
}

function onImageError(event: Event)
{
  const target = event.target as HTMLImageElement;
  target.src = defaultThumbnail;
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
        :src="thumbnail"
        alt="Preview"
        class="thumbnail-image"
        @error="onImageError"/>

    <div class="thumbnail-name">
      {{ image.name }}
    </div>

  </div>
</template>
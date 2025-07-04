<script setup lang="ts">

  import {Image} from "../../ts/Images/libImages.ts";

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const { image } = defineProps<{ image: Image }>()

  const brokenThumbnail = '/images/icons/brokenImage.webp';

  function OnPreviewImageError(event: Event): void
  {
    const target = event.target as HTMLImageElement;
    target.src = brokenThumbnail;
  }

</script>

<template>

  <div class="thumbnail-container">

    <img
        :src="apiBaseUrl + '/Files/' + image.thumbnailId"
        alt="Preview"
        class="thumbnail-image"
        @error="OnPreviewImageError"/>

    <div class="thumbnail-name">
      {{ image.name }}
    </div>

  </div>
</template>
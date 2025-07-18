<script setup lang="ts">

  import {Image} from "../../ts/Images/libImages.ts";
  import {PropType} from "vue";

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const props = defineProps({
    image: {
      type: Object as PropType<Image>,
      required: true
    },
    isShowImageName: {
      type: Boolean,
      required: true
    }
  })

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

    <div
        v-if="props.isShowImageName"
        class="thumbnail-name">
      {{ image.name }}
    </div>

  </div>
</template>
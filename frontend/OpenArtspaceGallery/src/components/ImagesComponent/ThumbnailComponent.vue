<script setup lang="ts">

import {Image} from "../../ts/Images/libImages.ts";

const { image } = defineProps<{ image: Image }>()

</script>

<template>
  <div class="thumbnail-container">
    <img
        :src="`http://localhost:5271/api/Files/${image.thumbnailId}`"
        alt="Preview"
        class="thumbnail-image"

    />
    <p class="thumbnail-name">{{ image.name }}</p>
  </div>
</template>
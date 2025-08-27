<script setup lang="ts">

const props = defineProps({
  imageId: String
})

alert(props.imageId)

</script>

<template>

  <div
    class="tool-bar">
    Полоса
  </div>

  <div
    class="image-content-container">

    <div
        class="image-section">
      Фото
    </div>

    <div
        class="info-section">
      информация
    </div>

  </div>

</template>
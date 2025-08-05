<script setup lang="ts">

  const emit = defineEmits(["uploadImage"])

  async function SelectedUploadImagePopup(): Promise<void>
  {
    emit("uploadImage")
  }

</script>

<template>

  <!-- Add image button -->
  <div class="selected-menu-button-container">

    <div
        class="selected-menu-button">

      <img class="selected-menu-button-image"
           src="/images/icons/AddContent.webp"
           alt="Add image"
           title="Add image"
           @click="SelectedUploadImagePopup"/>

    </div>

  </div>

</template>
<script setup lang="ts">

  const emit = defineEmits(["createAlbum"])

  async function SelectedCreateAlbumPopup(): Promise<void>
  {
    emit("createAlbum")
  }

</script>

<template>

  <!-- Add album button -->
  <div class="selected-menu-button-container">

    <div
        class="selected-menu-button">

      <img class="selected-menu-button-image"
           src="/images/icons/AddContent.webp"
           alt="Create new album"
           title="Create new album"
           @click="SelectedCreateAlbumPopup"/>

    </div>

  </div>

</template>
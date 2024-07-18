<script setup lang="ts">
  import AlbumsListComponent from "../Shared/Albums/AlbumsListComponent.vue";

  const props = defineProps({
    currentAlbumId: String
  })

</script>

<template>

  <AlbumsListComponent :currentAlbumId="props.currentAlbumId"/>

</template>

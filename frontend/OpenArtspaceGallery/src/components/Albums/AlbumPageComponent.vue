<script setup lang="ts">
  import AlbumsListComponent from "../Shared/Albums/AlbumsListComponent.vue";

  const props = defineProps({
    parentAlbumId: String
  })



</script>

<template>

  <AlbumsListComponent :parentAlbumId="props.parentAlbumId"/>

</template>

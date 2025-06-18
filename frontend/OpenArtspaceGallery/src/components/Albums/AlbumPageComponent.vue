<script setup lang="ts">
  import AlbumsListComponent from "../Shared/Albums/AlbumsListComponent.vue";
  import {PropType} from "vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

</script>

<template>

  <AlbumsListComponent :currentAlbumId="props.currentAlbumId"/>

</template>

<script setup lang="ts">
  import FooterComponent from "../components/Shared/Foooter/FooterComponent.vue";
  import HeaderComponent from "../components/Shared/Header/HeaderComponent.vue";
  import AlbumPageComponent from "../components/Albums/AlbumPageComponent.vue";

  const props = defineProps({
    parentAlbumId: String
  })
</script>

<template>
  <HeaderComponent/>

  <AlbumPageComponent :parentAlbumId="props.parentAlbumId" />

  <FooterComponent/>
</template>
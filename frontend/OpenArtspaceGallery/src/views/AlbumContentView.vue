<script setup lang="ts">
  import FooterComponent from "../components/Shared/Foooter/FooterComponent.vue";
  import HeaderComponent from "../components/Shared/Header/HeaderComponent.vue";
  import AlbumPageComponent from "../components/Albums/AlbumPageComponent.vue";
  import ImageComponent from "../components/ImagesComponent/ImageComponent.vue";

  const props = defineProps({
    currentAlbumId: String
  })
</script>

<template>
  <HeaderComponent />

  <AlbumPageComponent :currentAlbumId="props.currentAlbumId" />

  <ImageComponent :currentAlbumId="props.currentAlbumId"/>

  <FooterComponent />
</template>
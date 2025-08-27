<script setup lang="ts">

import HeaderComponent from "../../components/Shared/Header/HeaderComponent.vue";
import FooterComponent from "../../components/Shared/Foooter/FooterComponent.vue";
import PhotoComponent from "../../components/Images/PhotoComponent.vue";

const props = defineProps({
  imageId: String
})
</script>

<template>
  <HeaderComponent />

  <PhotoComponent :imageId="props.imageId" />

  <FooterComponent />
</template>
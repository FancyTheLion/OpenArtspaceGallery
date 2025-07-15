<script setup lang="ts">
  import HeaderComponent from "../components/Shared/Header/HeaderComponent.vue";
  import FooterComponent from "../components/Shared/Foooter/FooterComponent.vue";
  import HomePageWrapperComponent from "../components/Shared/SelectedMenu/HomePageWrapperComponent.vue";
  import {PropType} from "vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })
</script>

<template>

    <HeaderComponent />

    <HomePageWrapperComponent
        :currentAlbumId="props.currentAlbumId"/>

    <FooterComponent/>

</template>
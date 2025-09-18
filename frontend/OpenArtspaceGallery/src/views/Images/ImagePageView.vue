<script setup lang="ts">

import HeaderComponent from "../../components/Shared/Header/HeaderComponent.vue";
import FooterComponent from "../../components/Shared/Foooter/FooterComponent.vue";
import ImageComponent from "../../components/Images/ImageComponent.vue";
import {PropType} from "vue";

  const props = defineProps({
    imageId: {
      type: String as PropType<string>,
      required: true
    }
  })

</script>

<template>
    <HeaderComponent />

    <ImageComponent :imageId="props.imageId" />

    <FooterComponent />

</template>
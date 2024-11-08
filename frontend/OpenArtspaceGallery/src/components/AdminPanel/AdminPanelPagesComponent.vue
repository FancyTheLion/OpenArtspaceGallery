<script setup lang="ts">

  import AdminPanelImagesSizesComponent from "./AdminPanelImagesSizesComponent.vue";
  import AdminPanelOtherComponent from "./AdminPanelOtherComponent.vue";
  import {ImageSizeButtonId, OtherButtonId} from "../../ts/Shared/Constans/libAdminPanelButton.ts";

  const props = defineProps({
    id: {
      type: String,
      default: "",
      required: true
    }
  })

</script>

<template>

  <AdminPanelImagesSizesComponent v-if="props.id === ImageSizeButtonId" />

  <AdminPanelOtherComponent v-else-if="props.id === OtherButtonId" />

</template>
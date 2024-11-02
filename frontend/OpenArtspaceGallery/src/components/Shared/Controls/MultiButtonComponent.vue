<script setup lang="ts">

  import {PropType} from "vue";
  import {MultiButtonButton} from "../../../ts/Shared/Controls/libMultiButton.ts";

  const props = defineProps({
    buttons: {
      type: Object as PropType<Array<MultiButtonButton>>,
      required: true
    }
  })

</script>

<template>

  <div> Hello there </div>

  <div v-for="button in props.buttons" :key="button.name">

    {{ button.name }}

  </div>

</template>
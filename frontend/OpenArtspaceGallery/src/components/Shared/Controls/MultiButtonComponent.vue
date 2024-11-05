<script setup lang="ts">
  import {PropType, ref} from "vue";
  import {MultiButtonButton} from "../../../ts/Shared/Controls/libMultiButton.ts";
  import ButtonComponent from "./ButtonComponent.vue";

  const props = defineProps({
    buttons: {
      type: Object as PropType<Array<MultiButtonButton>>,
      required: true
    }
  })

  const activeButtonId = ref<string>("")

  function ButtonOutputId(id: string)
  {
    activeButtonId.value = id
  }

</script>

<template>

  <div v-for="button in props.buttons" :key="activeButtonId.value">

    <ButtonComponent
      :name="button.name"
      :id="button.id"
      :isActive="button.id === activeButtonId"
      @clicked="ButtonOutputId"
    />

  </div>

</template>
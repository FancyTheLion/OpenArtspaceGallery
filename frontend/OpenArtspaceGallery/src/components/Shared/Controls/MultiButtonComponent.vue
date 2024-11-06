<script setup lang="ts">
  import {PropType, ref} from "vue";
  import {MultiButtonButton} from "../../../ts/Shared/Controls/libMultiButton.ts";
  import SimpleButtonComponent from "./SimpleButtonComponent.vue";

  const props = defineProps({
    buttons: {
      type: Object as PropType<Array<MultiButtonButton>>,
      required: true
    },
    activeButton: {
      type: String,
      required: true
    }
  })

  const emit = defineEmits([ "buttonSelected" ])

  const activeButtonId = ref<string>(props.activeButton)

  function OnButtonClicked(id: string)
  {
    activeButtonId.value = id
    emit("buttonSelected", activeButtonId.value)
  }

</script>

<template>

  <div class="multibutton">

      <SimpleButtonComponent
        v-for="button in props.buttons" :key="activeButtonId.value"
        :name="button.name"
        :id="button.id"
        :isActive="button.id === activeButtonId"
        @clicked="OnButtonClicked"
      />

  </div>

</template>
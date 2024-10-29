<script setup lang="ts">

  const props = defineProps({
    name: {
      type: String,
      default: "",
      required: true
    }
  })

  const isActiveButton = ref<boolean>(false)

  function ToggleButton()
  {
    isActiveButton.value = !isActiveButton.value
  }

</script>

<template>

  <div class="admin-panel-action-button-conteiner admin-panel-action-button">

    <div :class="{'active-button': isActiveButton}"
    @click="ToggleButton">

      {{props.name}}

    </div>


  </div>

</template>
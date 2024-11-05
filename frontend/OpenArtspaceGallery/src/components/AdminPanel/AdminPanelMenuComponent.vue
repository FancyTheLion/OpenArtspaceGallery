<script setup lang="ts">
  import {ref} from "vue";
  import MultiButtonComponent from "../Shared/Controls/MultiButtonComponent.vue";

  const buttonsList: Array<MultiButtonButton> = ref ([
    { name: "one", id: "f16d0a3e-2bdf-4634-9cf7-7ae9e9b12a10"},
    { name: "two", id: "417cff23-217d-43e8-b59d-65bf65bc62dc" },
    { name: "three", id: "76060c36-d2f5-4fdd-9455-8b20ce983d6c" }
  ])

  function OnClick(currentButtonId: string)
  {
    alert(currentButtonId)
  }

</script>

<template>

  <div class="admin-panel-menu-list">

    <MultiButtonComponent
      :buttons="buttonsList"
      activeButton="f16d0a3e-2bdf-4634-9cf7-7ae9e9b12a10"
      @buttonSelected="OnClick"/>

  </div>

</template>
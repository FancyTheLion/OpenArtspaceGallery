<script setup lang="ts">
  import ButtonComponent from "../Shared/Controls/ButtonComponent.vue";
  import ColumnOfButtons from "../Shared/Controls/MultiButtonComponent.vue";
  import {MultiButtonButton} from "../../ts/Shared/Controls/libMultiButton.ts";
  import {ref} from "vue";

  const listButtons: Array<MultiButtonButton> = ref ([
    { name: "one" },
    { name: "two" },
    { name: "three" }
  ])

  async function ButtonOneClickedAsync()
  {
    alert("Button 1 clicked")
  }

  async function ButtonTwoClickedAsync()
  {
    alert("Button 2 clicked")
  }

</script>

<template>

  <div class="admin-panel-menu-list">

    <ButtonComponent
      name="Big button"
      :isActive="false"
      @clicked="async() => await ButtonOneClickedAsync()" />

    <ButtonComponent
      name="Button 2"
      :isActive="true"
      @clicked="async() => await ButtonTwoClickedAsync()"/>

    <ColumnOfButtons
      :buttons="listButtons" />

  </div>

</template>
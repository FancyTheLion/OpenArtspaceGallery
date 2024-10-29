<script setup lang="ts">
  import ButtonComponent from "../Shared/Controls/ButtonComponent.vue";

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

  <div class="admin-panel-menu-container">

    <ButtonComponent
      name="Big button"
      :isActive="false"
      @clicked="async() => await ButtonOneClickedAsync()" />

    <ButtonComponent
      name="Button 2"
      :isActive="true"
      @clicked="async() => await ButtonTwoClickedAsync()"/>

  </div>

</template>
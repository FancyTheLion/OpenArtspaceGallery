<script setup lang="ts">
  import MultiButtonComponent from "../Shared/Controls/MultiButtonComponent.vue";
  import {buttonsList} from "../../ts/Shared/Constans/libAdminPanelButton.ts";

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
<script setup lang="ts">
  import MultiButtonComponent from "../Shared/Controls/MultiButtonComponent.vue";
  import {ImageSizeButtonId, ImageSizeButtonName,
    OtherButtonId, OtherButtonName} from "../../ts/Shared/Constans/libAdminPanelButton.ts";

  const buttonList = [
    { name: ImageSizeButtonName, id: ImageSizeButtonId },
    { name: OtherButtonName, id: OtherButtonId },
  ]

  const emit = defineEmits([ "activatedButton" ])

  function OnButtonClicked(currentButtonId: string)
  {
    emit("activatedButton", currentButtonId)
  }

</script>

<template>

  <div class="admin-panel-menu-list">

    <MultiButtonComponent
      :buttons="buttonList"
      activeButton="f16d0a3e-2bdf-4634-9cf7-7ae9e9b12a10"
      @buttonSelected="OnButtonClicked"/>

  </div>

</template>
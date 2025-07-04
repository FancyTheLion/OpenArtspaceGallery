<script setup lang="ts">
  import AdminPanelPagesComponent from "./AdminPanelPagesComponent.vue";
  import AdminPanelMenuComponent from "./AdminPanelMenuComponent.vue";
  import {ref} from "vue";
  import {ImageSizeButtonId} from "../../ts/Shared/Constans/libAdminPanelButton.ts";

  const activePageId = ref<string>(ImageSizeButtonId)

  function OnButtonClicked(currentButtonId: string): void
  {
    activePageId.value = currentButtonId;
  }

</script>

<template>

  <div class="admin-panel-main-container">

    <div class="admin-panel-menu-container">

      <AdminPanelMenuComponent
        @activatedButton="OnButtonClicked"/>

    </div>

    <div class="admin-panel-info">

      <AdminPanelPagesComponent
      :key="activePageId"
      :id="activePageId"/>

    </div>

  </div>

</template>
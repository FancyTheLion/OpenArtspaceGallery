<script setup lang="ts">
import AdminActionPanelComponent from "./AdminActionPanelComponent.vue";
import AdminPanelInfoComponent from "./AdminPanelInfoComponent.vue";
</script>

<template>

  <div class="admin-panel-main-container">

    <div class="admin-panel-action">

      <div class="admin-panel-action-container">

        <AdminActionPanelComponent />

        <AdminPanelInfoComponent />

        <div>
          Something third
        </div>

      </div>

    </div>

    <div class="admin-panel-info">
      Right part
    </div>
  </div>

</template>
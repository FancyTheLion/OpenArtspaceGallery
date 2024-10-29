<script setup lang="ts">
import AdminActionPanelComponent from "./AdminPanelMenuComponent.vue";
import AdminPanelInfoComponent from "./AdminPanelInfoComponent.vue";
</script>

<template>

  <div class="admin-panel-main-container">

    <div class="admin-panel-action">

      <div class="admin-panel-action-container">

        <AdminActionPanelComponent />

        <div>
          Something second
        </div>

      </div>

    </div>

    <div class="admin-panel-info">

      <AdminPanelInfoComponent />

    </div>

  </div>

</template>
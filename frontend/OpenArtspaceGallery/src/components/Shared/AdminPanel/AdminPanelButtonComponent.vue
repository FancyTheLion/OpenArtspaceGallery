<script setup lang="ts">

const props = defineProps({
  name: {
    type: String,
    default: "",
    required: true
  }
})

</script>

<template>

  <div class="admin-panel-action-button-conteiner">

    <button>
      {{props.name}}
    </button>

  </div>

</template>
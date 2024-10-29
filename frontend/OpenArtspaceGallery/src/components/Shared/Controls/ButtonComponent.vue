<script setup lang="ts">

  const props = defineProps({
    name: {
      type: String,
      default: "",
      required: true
    },
    isActive: {
      type: Boolean,
      default: false,
      required: true
    }
  })

  const emit = defineEmits([ "clicked" ])

  function OnClick()
  {
    emit("clicked")
  }

</script>

<template>

    <div
        :class="(props.isActive)?'simple-button-active':'simple-button'"
        @click="OnClick">

      {{props.name}}

    </div>

</template>
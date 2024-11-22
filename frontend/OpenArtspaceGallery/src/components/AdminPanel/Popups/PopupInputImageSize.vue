<script setup lang="ts">

  import {ref} from "vue";

  defineExpose({
    Show: ShowPopup
  })

  const isDisplayed = ref<boolean>(false)

  async function ShowPopup()
  {
    isDisplayed.value = true

    alert("Show image size popup!")
  }

  async function HidePopup()
  {
    isDisplayed.value = false
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div>
      Yiffyuff!
    </div>

  </div>


</template>
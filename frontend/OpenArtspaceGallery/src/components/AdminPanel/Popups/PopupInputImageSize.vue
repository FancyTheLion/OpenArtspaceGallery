<script setup lang="ts">

import {reactive, ref} from "vue";

  defineExpose({
    Show: ShowPopup
  })

  const isDisplayed = ref<boolean>(false)

  const newImageSizeFormData = reactive({
    name: "",
    width: 0,
    height: 0
  })

  const emit = defineEmits([ "cancel", "ok" ])

  async function OnOk()
  {
    await HidePopup()

    emit("ok", newImageSizeFormData)
  }

  async function OnCancel()
  {
    await HidePopup()

    emit("cancel")
  }

  async function ShowPopup()
  {
    isDisplayed.value = true
  }

  async function HidePopup()
  {
    isDisplayed.value = false
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="popup-text-input-content">

          <div>
            Add new image size
          </div>

          Size name
          <input
            class="popup-input"
            v-model="newImageSizeFormData.name">

          Width
          <input
            class="popup-input"
            v-model="newImageSizeFormData.width">

          Height
          <input
            class="popup-input"
            v-model="newImageSizeFormData.height">

          <div class="popup-button-separation-container">

            <button
                @click="async () => await OnCancel()">
              Cancel
            </button>

            <button
                @click ="async () => await OnOk()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>


</template>
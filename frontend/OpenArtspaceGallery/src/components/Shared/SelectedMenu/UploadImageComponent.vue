<script setup lang="ts">

import { ref} from "vue";

  const isAddImagePopupVisible = ref<boolean>(false)

  const emit = defineEmits(["uploadImage"])

  function ShowAddImageFormPopup(): void
  {
    isAddImagePopupVisible.value = true
  }

  function HideAddNewImageFormPopup(): void
  {
    isAddImagePopupVisible.value = false
  }

  async function SelectedUploadImagePopup(): Promise<void>
  {
    emit("uploadImage")

    HideAddNewImageFormPopup()
  }

</script>

<template>

  <!-- Add image button -->
  <div class="selected-menu-button-container">

    <div
        class="selected-menu-button">

      <img class="selected-menu-button-image"
           src="/images/icons/AddContent.webp"
           alt="Add image"
           title="Add image"
           @click="ShowAddImageFormPopup"/>

    </div>

  </div>

  <div v-if="isAddImagePopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="selected-menu">

          <button
              class="selected-menu-form-buttons"
              type="button"
              @click="async() => await SelectedUploadImagePopup()">
            Upload image
          </button>

          <button
              type="button"
              @click="HideAddNewImageFormPopup()">
            Cancel
          </button>

        </div>

      </div>

    </div>

  </div>

</template>
<script setup lang="ts">

import { ref} from "vue";

  const props = defineProps({
    isAddImageButtonVisible: Boolean
  })

  const isSelectedMenuPopupVisible = ref<boolean>(false)

  const emit = defineEmits(["uploadImage", "createAlbum"])

  function ShowAddMenuPopup(): void
  {
    isSelectedMenuPopupVisible.value = true
  }

  function HideAddMenuPopup(): void
  {
    isSelectedMenuPopupVisible.value = false
  }

  async function SelectedCreateAlbumPopup(): Promise<void>
  {
    emit("createAlbum")

    HideAddMenuPopup()
  }

  async function SelectedUploadImagePopup(): Promise<void>
  {
    emit("uploadImage")

    HideAddMenuPopup()
  }

</script>

<template>

  <!-- Add image or album button -->
  <div class="selected-menu-button-container">

    <div
        class="selected-menu-button">

      <img class="selected-menu-button-image"
           src="/images/icons/AddContent.webp"
           alt="Create new album or add image"
           title="Create new album or add image"
            @click="ShowAddMenuPopup"/>

    </div>

  </div>

  <div v-if="isSelectedMenuPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="selected-menu">

          <button
              class="selected-menu-form-buttons"
              type="button"
              @click="async() => await SelectedCreateAlbumPopup()">
            Create album
          </button>

          <button
              v-if="props.isAddImageButtonVisible"
              class="selected-menu-form-buttons"
              type="button"
              @click="async() => await SelectedUploadImagePopup()">
            Upload image
          </button>

          <button
              type="button"
              @click="HideAddMenuPopup()">
            Cancel
          </button>

          </div>

        </div>

      </div>

    </div>

</template>
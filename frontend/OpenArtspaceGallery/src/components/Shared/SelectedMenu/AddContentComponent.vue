<script setup lang="ts">

import {ref} from "vue";

  const isSelectedMenuPopupVisible = ref<boolean>(false)

  const emit = defineEmits(["uploadImage", "createAlbum"])


  function ShowAddMenuPopup()
  {
    isSelectedMenuPopupVisible.value = true
  }

  function HideAddMenuPopup()
  {
    isSelectedMenuPopupVisible.value = false
  }

  async function SelectedCreateAlbumPopup()
  {
    emit("createAlbum")

    HideAddMenuPopup()
  }

  async function SelectedUploadImagePopup()
  {
    emit("uploadImage")

    HideAddMenuPopup()
  }

</script>

<template>

  <!-- Add image or album button -->
  <div class="new-album-button-container">

    <div
        class="new-album-button">

      <img class="new-album-button-image"
           src="/images/icons/addNewAlbum.webp"
           alt="Create new album or add image"
           title="Create new album or add image"
            @click="ShowAddMenuPopup"/>

    </div>

  </div>

  <div v-if="isSelectedMenuPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

          <button
              type="button"
              @click="HideAddMenuPopup()">
            Cancel
          </button>

          <button
              class="new-album-form-buttons"
              type="button"
              @click="async() => await SelectedCreateAlbumPopup()">
            Create album
          </button>

          <button
              class="new-album-form-buttons"
              type="button"
              @click="async() => await SelectedUploadImagePopup()">
            Upload image
          </button>

          </div>

        </div>

      </div>

    </div>

</template>
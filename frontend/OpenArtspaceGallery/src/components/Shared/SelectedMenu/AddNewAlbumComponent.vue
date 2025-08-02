<script setup lang="ts">

import { ref} from "vue";

  const isAddNewAlbumPopupVisible = ref<boolean>(false)

  const emit = defineEmits(["createAlbum"])

  function ShowAddNewAlbumFormPopup(): void
  {
    isAddNewAlbumPopupVisible.value = true
  }

  function HideAddNewAlbumFormPopup(): void
  {
    isAddNewAlbumPopupVisible.value = false
  }

  async function SelectedCreateAlbumPopup(): Promise<void>
  {
    emit("createAlbum")

    HideAddNewAlbumFormPopup()
  }

</script>

<template>

  <!-- Add album button -->
  <div class="selected-menu-button-container">

    <div
        class="selected-menu-button">

      <img class="selected-menu-button-image"
           src="/images/icons/AddContent.webp"
           alt="Create new album"
           title="Create new album"
            @click="ShowAddNewAlbumFormPopup"/>

    </div>

  </div>

  <div v-if="isAddNewAlbumPopupVisible">

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
              type="button"
              @click="HideAddNewAlbumFormPopup()">
            Cancel
          </button>

          </div>

        </div>

      </div>

    </div>

</template>
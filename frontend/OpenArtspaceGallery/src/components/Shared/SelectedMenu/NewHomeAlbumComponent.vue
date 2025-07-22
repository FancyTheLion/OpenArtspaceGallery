<script setup lang="ts">

import {ref} from "vue";

const isSelectedMenuPopupVisible = ref<boolean>(false)

const emit = defineEmits(["createAlbum"])


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

</script>

<template>

  <!-- Add image or album button -->
  <div class="new-album-button-container">

    <div
        class="new-album-button">

      <img class="new-album-button-image"
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


        </div>

      </div>

    </div>

  </div>

</template>
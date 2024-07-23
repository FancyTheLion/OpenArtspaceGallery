<script setup lang="ts">

  import {ref} from "vue";

  const isNewAlbumPopupVisible = ref<boolean>(false)

  async function ShowNewAlbumPopup()
  {
    isNewAlbumPopupVisible.value = true
  }

  async function HideNewAlbumPopup()
  {
    isNewAlbumPopupVisible.value = false
  }

</script>

<template>

  <!-- New album button -->
  <div class="new-album-button-container">

    <div
        class="new-album-button"
        @click="async () => await ShowNewAlbumPopup()">

        <img class="new-album-button-image" src="/images/AddNewAlbum.webp" alt="Create new album" title="Create new album"/>
    </div>

  </div>

  <!-- New album form popup -->
  <div v-if="isNewAlbumPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

          <div>
            Название альбома:
          </div>

          <div>
            <input />
          </div>

          <div>
            <button @click="async() => await HideNewAlbumPopup()">
              Отмена
            </button>

            <button>
              Загрузить
            </button>

          </div>

        </div>

      </div>

    </div>


  </div>

</template>
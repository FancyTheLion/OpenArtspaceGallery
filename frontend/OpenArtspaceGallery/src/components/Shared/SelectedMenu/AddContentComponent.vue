<script setup lang="ts">

import {PropType, ref} from "vue";
import NewAlbumComponent from "../Albums/NewAlbumComponent.vue";
import AddImageComponent from "../../Images/AddImageComponent.vue";

const props = defineProps({
  currentAlbumId: {
    type: String as PropType<string>,
    required: true
  }
})

const isNewAlbumPopupVisible = ref<boolean>(false)

const isAddImagePopupVisible = ref<boolean>(false)

const isSelectedMenuPopupVisible = ref<boolean>(false)

/*function ShowAddImagePopup()
{
  isAddImagePopupVisible.value = true
}

function ShowNewAlbumPopup()
{
  isNewAlbumPopupVisible.value = true
}*/

function ShowAddMenuPopup()
{
  isSelectedMenuPopupVisible.value = true
}

</script>

<template>

  <!-- Add image or album button -->
  <div class="new-album-button-container">

    <div
        class="new-album-button">

      <img class="new-album-button-image"
           src="/images/icons/addNewAlbum.webp"
           alt="Create new album"
           title="Create new album or add image"
            @click="ShowAddMenuPopup"/>

    </div>

  </div>

  <div v-if="isSelectedMenuPopupVisible">

    <div v-if="isNewAlbumPopupVisible">

      <NewAlbumComponent
          :currentAlbumId="props.currentAlbumId"/>

    </div>

    <!-- New image button -->
    <div class="new-album-button-container">

      <AddImageComponent
          :currentAlbumId="props.currentAlbumId"/>

    </div>

  </div>



</template>
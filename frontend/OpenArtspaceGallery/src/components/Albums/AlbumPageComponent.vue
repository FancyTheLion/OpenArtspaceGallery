<script setup lang="ts">
  import AlbumsListComponent from "../Shared/Albums/AlbumsListComponent.vue";
  import {PropType, ref} from "vue";
  import NewAlbumComponent from "../Shared/Albums/NewAlbumComponent.vue";
  import AddImageComponent from "../Images/AddImageComponent.vue";
  import ImagesListComponent from "../Images/ImagesListComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const isNewAlbumPopupVisible = ref(false);
  const isAddImagePopupVisible = ref(false);

  function ShowNewAlbumPopup() {
    isNewAlbumPopupVisible.value = true;
  }

  function ShowAddImagePopup() {
    isAddImagePopupVisible.value = true;
  }

</script>

<template>

  <AlbumsListComponent
      :currentAlbumId="props.currentAlbumId"
      @createAlbum="ShowNewAlbumPopup"
      @uploadImage="ShowAddImagePopup"
  />

  <NewAlbumComponent
      v-if="isNewAlbumPopupVisible"
      :currentAlbumId="props.currentAlbumId"
      @close="isNewAlbumPopupVisible = false"
  />

  <ImagesListComponent
      :currentAlbumId="props.currentAlbumId"
      @uploadImage="ShowAddImagePopup"
      @createAlbum="ShowNewAlbumPopup"
  />

  <AddImageComponent
      v-if="isAddImagePopupVisible"
      :currentAlbumId="props.currentAlbumId"
      @close="isAddImagePopupVisible = false"
  />

</template>

<script setup lang="ts">
import {computed, PropType, ref} from "vue";
import AlbumsListComponent from "../Albums/AlbumsListComponent.vue";
import NewAlbumComponent from "../Albums/NewAlbumComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const albumsListComponent = ref<InstanceType<typeof AlbumsListComponent>>();

  const isNewAlbumPopupVisible = ref(false);
  const isAddImagePopupVisible = ref(false);

  const isHomePage = computed(() => props.currentAlbumId == null);

  function ShowNewAlbumPopup() {
    isNewAlbumPopupVisible.value = true;
  }

  function ShowAddImagePopup() {
    isAddImagePopupVisible.value = true;
  }

  function OnNewAlbumCreationCancelled() {
    isNewAlbumPopupVisible.value = false;
  }

  async function OnNewAlbumCreatedAsync() {
    isNewAlbumPopupVisible.value = false;
    await albumsListComponent.value?.RefreshAsync();
  }

</script>

<template>

    <AlbumsListComponent
        ref="albumsListComponent"
        :isAddImageButtonVisible="!isHomePage"
        @createAlbum="ShowNewAlbumPopup"
        @uploadImage="ShowAddImagePopup"
    />

    <NewAlbumComponent
        v-if="isNewAlbumPopupVisible"
        :currentAlbumId="props.currentAlbumId"
        @cancelled="OnNewAlbumCreationCancelled"
        @newAlbumCreated="OnNewAlbumCreatedAsync"
    />

</template>
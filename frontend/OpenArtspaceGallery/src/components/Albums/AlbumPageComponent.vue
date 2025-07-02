<script setup lang="ts">
  import AlbumsListComponent from "../Shared/Albums/AlbumsListComponent.vue";
  import {PropType, ref} from "vue";
  import NewAlbumComponent from "../Shared/Albums/NewAlbumComponent.vue";
  import NewImageComponent from "../Images/NewImageComponent.vue";
  import ImagesListComponent from "../Images/ImagesListComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const albumsListComponent = ref<InstanceType<typeof AlbumsListComponent>>();
  const imagesListComponent = ref<InstanceType<typeof ImagesListComponent>>();

  const isNewAlbumPopupVisible = ref(false);
  const isAddImagePopupVisible = ref(false);

  function ShowNewAlbumPopup() : void {
    isNewAlbumPopupVisible.value = true;
  }

  function ShowAddImagePopup() : void {
    isAddImagePopupVisible.value = true;
  }

  function OnNewAlbumCreationCancelled() : void
  {
    HideNewAlbumPopup()
  }

  async function OnNewAlbumCreatedAsync() : Promise<void>
  {
    HideNewAlbumPopup()
    await albumsListComponent.value!.RefreshAsync()
  }

  function HideNewAlbumPopup(): void {
    isNewAlbumPopupVisible.value = false;
  }

  function OnNewImageCreationCancelled(): void {
    HideNewImagePopup()
  }

  async function OnNewImageCreatedAsync(): Promise<void> {
    HideNewImagePopup()
    await imagesListComponent.value!.RefreshAsync()
  }

  function HideNewImagePopup(): void {
    isAddImagePopupVisible.value = false;
  }

</script>

<template>

  <AlbumsListComponent
      ref="albumsListComponent"
      :currentAlbumId="props.currentAlbumId"
      @uploadImage="ShowAddImagePopup"
      @createAlbum="ShowNewAlbumPopup"
  />

  <NewAlbumComponent
      v-if="isNewAlbumPopupVisible"
      :currentAlbumId="props.currentAlbumId"
      @cancelled="OnNewAlbumCreationCancelled"
      @newAlbumCreated="async () => await OnNewAlbumCreatedAsync()"
  />

  <ImagesListComponent
      ref="imagesListComponent"
      :currentAlbumId="props.currentAlbumId"
      @uploadImage="ShowAddImagePopup"
      @createAlbum="ShowNewAlbumPopup"
  />

  <NewImageComponent
      v-if="isAddImagePopupVisible"
      :currentAlbumId="props.currentAlbumId"
      @cancelled="OnNewImageCreationCancelled"
      @imageUploaded="async () => await OnNewImageCreatedAsync()"
  />

</template>

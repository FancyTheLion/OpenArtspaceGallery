<script setup lang="ts">

  import {onMounted, PropType, ref} from "vue";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import {DecodeImagesResponse, Image} from "../../ts/Images/libImages.ts";
  import ThumbnailComponent from "./ThumbnailComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

/*  const isLoading = ref<boolean>(true)*/

  const images = ref<Image[]>([])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
      images.value = await GetImagesListAsync(props.currentAlbumId)

/*      isLoading.value = false*/
  }

/*  async function RefreshImageListAsync()
  {
/!*    isLoading.value = true*!/

    images.value = await GetImagesListAsync(props.currentAlbumId)

/!*    isLoading.value = false*!/
  }*/

/*
  async function OnImageAddedAsync()
  {
    await RefreshImageListAsync()
  }
*/

  async function GetImagesListAsync(currentAlbumId: String)
  {
    return DecodeImagesResponse((await (await WebClientSendGetRequest("/Images/ByAlbum/" + currentAlbumId)).json()))
        .images
        .sort((a: Image, b: Image) => a.creationTime.getTime() - b.creationTime.getTime())
  }

</script>

<template>

  <div class="images-container">

    <div
      v-if="images.length === 0">
      Album is empty
    </div>

    <div
      v-for="image in images"
      :key="image.id">

      <ThumbnailComponent
          :image="image" />

    </div>

<!--    <AddMediaComponent
        @newImageAdded="async () => await OnImageAddedAsync()"/>-->

  </div>

</template>
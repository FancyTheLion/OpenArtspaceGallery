<script setup lang="ts">

  import {onMounted, PropType, ref} from "vue";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import {DecodeImagesResponse, Image} from "../../ts/Images/libImages.ts";
  import ThumbnailComponent from "./ThumbnailComponent.vue";
  import LoadingSymbolComponent from "../Shared/LoadingSymbol/LoadingSymbolComponent.vue";
  import UploadImageButtonComponent from "../Shared/SelectedMenu/UploadImageButtonComponent.vue";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

  defineExpose({
    RefreshAsync: RefreshImageListAsync
  })

  const isLoading = ref<boolean>(true)

  const images = ref<Image[]>([])

  const isShowImageName = true

  const emit = defineEmits(["createAlbum", "uploadImage"])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
      await RefreshImageListAsync()
  }

  async function GetImagesListAsync(currentAlbumId: String): Promise<Image[]>
  {
    return DecodeImagesResponse((await (await WebClientSendGetRequest("/Images/ByAlbum/" + currentAlbumId)).json()))
        .images
        .sort((a: Image, b: Image) => a.creationTime.getTime() - b.creationTime.getTime())
  }

  async function RefreshImageListAsync(): Promise<void>
  {
    isLoading.value = true

    images.value = await GetImagesListAsync(props.currentAlbumId)

    isLoading.value = false
  }

  async function UploadImageAsync(): Promise<void>
  {
    emit("uploadImage")
  }

</script>

<template>
  <LoadingSymbolComponent v-if="isLoading" />

  <div v-if="!isLoading">

    <div class="images-container">

      <UploadImageButtonComponent
          @uploadImage="async () => await UploadImageAsync()"/>

      <div
        v-if="images.length === 0">
        Image is empty
      </div>

      <div
        v-for="image in images"
        :key="image.id">

        <ThumbnailComponent
            :isShowImageName="isShowImageName"
            :image="image" />

      </div>

      <UploadImageButtonComponent
          @uploadImage="async () => await UploadImageAsync()"/>

    </div>

  </div>

</template>
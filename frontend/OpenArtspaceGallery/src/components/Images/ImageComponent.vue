<script setup lang="ts">

import {onMounted, PropType, ref} from "vue";
import {
  DecodeImageSizeDto,
  DecodeImagesSizesResponse,
  ImageSize,
  ImagesSizeType
} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
import {DecodeImageResponse, ImageFile, ImageModel} from "../../ts/Images/libImageFiles.ts";
  import Menu from "../Shared/Controls/Menu.vue";

  const props = defineProps({
    imageId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const sizes = ref<ImageSize[]>([])

  const defaultImageSizeId = ref<string>()

  const originalImageSizeId = ref<string>()

  const currentImageFileId = ref<string | null>(null)

  const originalImageFileId = ref<string | null>(null)

  const image = ref<ImageModel | null>(null)

  const isVisible = ref<boolean>(false)

  onMounted(async () =>
  {
    await OnLoad()
  })

  async function OnLoad()
  {
    sizes.value = await LoadSizes()
    image.value = await GetImageAsync(props.imageId)

    defaultImageSizeId.value = GetRequiredSizeId(sizes.value, ImagesSizeType.DefaultMedium)
    originalImageSizeId.value = GetRequiredSizeId(sizes.value, ImagesSizeType.Original)

    currentImageFileId.value = GetRequiredFileId(image.value.files, defaultImageSizeId.value)
    originalImageFileId.value = GetRequiredFileId(image.value.files, originalImageSizeId.value)
  }

  async function GetImagesSizesListAsync(): Promise<ImageSize[]>
  {
    return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetList")).json()))
        .imagesSizes
        .map(DecodeImageSizeDto)
  }

  async function GetImageAsync(id: string): Promise<ImageModel>
  {
    return DecodeImageResponse((await (await WebClientSendGetRequest("/Images/" + id)).json()))
        .image
  }

  async function LoadSizes(): Promise<ImageSize[]>
  {
    return (await GetImagesSizesListAsync())
        .sort((a, b) => a.name.localeCompare(b.name))
  }

  function GetRequiredSizeId(sizes: ImageSize[], type: ImagesSizeType): string
  {
    const size = sizes.find(s => s.type === type)

    if (size === undefined)
    {
      throw new Error(`Backend didn't return an image size with type = ${type}`)
    }

    return size.id
  }

  function GetRequiredFileId(files: ImageFile[], sizeId: string): string
  {
    const file = files.find(f => f.sizeId === sizeId)

    if (file === undefined)
    {
      throw new Error(`Can't find image file with size ID = ${sizeId}`)
    }

    return file.id
  }

  async function ShowFullSizePhoto()
  {
    isVisible.value = true
  }

  async function HideFullSizePhoto()
  {
    isVisible.value = false
  }

  function OnSizeSelected(selectedSizeId: string): void
  {
    if (!image.value)
    {
      throw new Error("Image not loaded yet")
    }

    const newfileId = GetRequiredFileId(image.value.files, selectedSizeId)

    currentImageFileId.value = newfileId

    const downloadLink = document.createElement("a");

    downloadLink.href = apiBaseUrl + "/Files/" + newfileId
    downloadLink.download = "image.value.name"
    downloadLink.click();
  }

</script>

<template>

  <div
    class="toolbar">

    <div
      class="menu-container">

      <Menu
          :menuItemsExtended="sizes"
          @stringSelected="OnSizeSelected"/>

    </div>

    <a
      :href="apiBaseUrl + '/Files/' + currentImageFileId" download>

      <img
          class="icon-download-button"
          src="/images/icons/download.webp"
          alt="Download image"
          title="Download file"/>

    </a>

  </div>

  <div
    class="image-content-container">

    <div
        class="image-section">

      <img
          class="image"
          v-if="currentImageFileId"
          :src="apiBaseUrl + '/Files/' + currentImageFileId"
          alt="Image"
          @click="async() => await ShowFullSizePhoto()"/>

      <div v-if="isVisible">

        <!-- Popup lower layer -->
        <div class="popup-lower-layer">

        </div>

        <!-- Popup upper layer -->
        <div class="popup-upper-layer"
             @click="async() => await HideFullSizePhoto()">

          <div class="popup-main-image-section">

            <img
                class="popup-close-button"
                src="/images/icons/close.webp"
                alt="Close full size image"
                @click="async() => await HideFullSizePhoto()" />

            <img
                class="popup-image"
                :src="apiBaseUrl + '/Files/' +  originalImageFileId"
                alt="Full size image"/>

          </div>

        </div>

      </div>

    </div>

    <div
        class="info-section">

      <div v-if="image">

          <p>Название: {{ image.name }}</p>
          <p>Описание: {{ image.description }}</p>

      </div>

    </div>

  </div>

</template>
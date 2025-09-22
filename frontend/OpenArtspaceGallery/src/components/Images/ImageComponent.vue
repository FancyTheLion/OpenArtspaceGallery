<script setup lang="ts">

import {onMounted, PropType, ref} from "vue";
import {
  DecodeImageSizeDto,
  DecodeImagesSizesResponse,
  ImageSize,
  ImagesSizeType
} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import {DecodeImageResponse, ImageModel} from "../../ts/Images/libImageFiles.ts";
  import ImageSizeMenuComponent from "../Shared/SelectedMenu/ImageSizeMenuComponent.vue";

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
    await OnLoad();
  })

  async function OnLoad()
  {
    /* Function call */
    sizes.value = (await GetImagesSizesListAsync())
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))

    image.value = await GetImageAsync(props.imageId);

    /* Default size */
    defaultImageSizeId.value = sizes
        .value
        .find(s => s.type === ImagesSizeType.DefaultMedium)
        ?.id

    if (defaultImageSizeId.value === undefined)
    {
      throw new Error("Backend didn't return an image size with default flag!");
    }

    const defaultImageFile = image
        .value
        .files
        .find(f => f.sizeId === defaultImageSizeId.value!)

    if (defaultImageFile === undefined)
    {
      throw new Error("Can't find image file with size ID = " + defaultImageSizeId.value!);
    }

    currentImageFileId.value = defaultImageFile!.id;

    /* Original size */
    originalImageSizeId.value = sizes
        .value
        .find(s => s.type === ImagesSizeType.Original)
        ?.id

    if (originalImageSizeId.value === undefined)
    {
      throw new Error("Backend didn't return an image size with original flag!");
    }

    const origimalImageFile = image
        .value
        .files
        .find(f => f.sizeId === originalImageSizeId.value!)

    if (origimalImageFile === undefined)
    {
      throw new Error("Can't find image file with size ID = " + defaultImageSizeId.value!);
    }

    originalImageFileId.value = origimalImageFile!.id;
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

  async function ShowFullSizePhoto()
  {
    isVisible.value = true
  }

  async function HideFullSizePhoto()
  {
    isVisible.value = false
  }

</script>

<template>

  <div
    class="toolbar">

    <div
      class="menu-container">

      <ImageSizeMenuComponent
          :imageSizes="sizes"
      />

    </div>

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
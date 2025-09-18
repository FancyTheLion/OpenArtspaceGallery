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
  import PhotoSizeMenuComponent from "../Shared/SelectedMenu/PhotoSizeMenuComponent.vue";

  const props = defineProps({
    imageId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const sizes = ref<ImageSize[]>([])

  const defaultImageSizeId = ref<string>()

  const currentImageFileId = ref<string | null>(null)

  const image = ref<ImageModel | null>(null)

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    sizes.value = (await GetImagesSizesListAsync())
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))

    defaultImageSizeId.value = sizes
        .value
        .find(s => s.type === ImagesSizeType.MediumDefault)
        ?.id

    if (defaultImageSizeId.value === undefined)
    {
      throw new Error("Backend didn't return an image size with default flag!");
    }

    image.value = await GetImageAsync(props.imageId);

    const defaultImageFile = image
        .value
        .files
        .find(f => f.sizeId === defaultImageSizeId.value!)

    if (defaultImageFile === undefined)
    {
      throw new Error("Can't find image file with size ID = " + defaultImageSizeId.value!);
    }

    currentImageFileId.value = defaultImageFile!.id;
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

</script>

<template>

  <div
    class="tool-bar"> <!-- TODO: Rename to toolbar -->

    <div
      class="menu-container">

      <PhotoSizeMenuComponent
          :photoSizes="sizes"
      />

    </div>

  </div>

  <div
    class="image-content-container">

    <div
        class="image-section">

      <img
          v-if="currentImageFileId"
          :src="apiBaseUrl + '/Files/' + currentImageFileId"
          alt="Preview"/>


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
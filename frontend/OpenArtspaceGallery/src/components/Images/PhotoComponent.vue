<script setup lang="ts">

import {computed, onMounted, PropType, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
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

  const defaultSize = ref()

  const defaultMediumFile = ref<{ id: string; sizeId: string } | null>(null)

  const files = ref<ImageModel | null>(null)

  const imageName = computed(() => files.value?.name || '');

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    sizes.value = await GetImagesSizesListAsync();

    const defaultMediumSize = sizes.value.find(s => s.type === 3)
    defaultSize.value = defaultMediumSize?.id || null

    files.value = await GetImageFilesAsync(props.imageId);

    const imageSizeId = files.value?.files.find(f => f.sizeId === defaultSize.value);

    if (imageSizeId === undefined)
    {
      throw new Error("Unknown image size type: " + defaultSize.value);
    }

    defaultMediumFile.value = imageSizeId!;
  }

  async function GetImagesSizesListAsync(): Promise<ImageSize[]>
  {
    return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetList")).json()))
        .imagesSizes
        .map(DecodeImageSizeDto)
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))
  }

  async function GetImageFilesAsync(imageId: string): Promise<ImageModel>
  {
    return DecodeImageResponse((await (await WebClientSendGetRequest("/Images/" + imageId)).json()))
        .image
  }



</script>

<template>

  <div
    class="tool-bar">

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
          v-if="defaultMediumFile"
          :src="apiBaseUrl + '/Files/' + defaultMediumFile.id"
          alt="Preview"/>


    </div>

    <div
        class="info-section">

      Info 1

      <div v-if="files">

        <div
            v-for="imageFile in files.files"
            :key="imageFile.id">

          {{ imageName }}
          {{ files.name }}
          {{ imageFile.id }}

        </div>

      </div>

    </div>

  </div>

</template>
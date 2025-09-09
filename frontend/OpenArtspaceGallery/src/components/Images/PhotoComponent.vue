<script setup lang="ts">

import {computed, onMounted, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import {DecodeImageResponse, ImageModel} from "../../ts/Images/libImageFiles.ts";
  import {useRoute} from "vue-router";

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const isMenuOpen = ref(false);

  const sizes = ref<ImageSize[]>([])

  const originalSize = ref()

  const originalFile = ref<{ id: string; sizeId: string } | null>(null)

  const files = ref<ImageModel | null>(null)

  const imageName = computed(() => files.value?.name || '');

  const route = useRoute()
  const imageId = route.params.imageId as string

  const toggleMenu = () => {
    isMenuOpen.value = !isMenuOpen.value;
  };

  const closeMenu = (e: MouseEvent) => {
    const target = e.target as HTMLElement;

    if (!target.closest(".menu-container"))
    {
      isMenuOpen.value = false;
    }
  };

  document.addEventListener("click", closeMenu);

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    sizes.value = await GetImagesSizesListAsync();

    const original = sizes.value.find(s => s.type === 3)
    originalSize.value = original?.id || null

    files.value = await GetImageFilesAsync(imageId);

    const imageSizeId = files.value?.files.find(f => f.sizeId === originalSize.value);

    if (imageSizeId === undefined)
    {
      throw new Error("Unknown image size type: " + originalSize.value);
    }

    originalFile.value = imageSizeId!;
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

    <div class="menu-container">
      <button
          class="icon-button"
          @click="toggleMenu">☰</button>

      <div
          class="menu"
          :class="{ active: isMenuOpen }">

        <div>Photo sizes:</div>

        <div v-for="size in sizes" :key="size.id">

          <a href="#">{{size.name}} {{size.width}} {{size.height}}</a>

        </div>

      </div>

    </div>

  </div>

  <div
    class="image-content-container">

    <div
        class="image-section">

      <img
          v-if="originalFile"
          :src="apiBaseUrl + '/Files/' + originalFile.id"
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
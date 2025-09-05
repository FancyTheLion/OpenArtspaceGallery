<script setup lang="ts">

  import {onMounted, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import {DecodeImageResponse, ImageModel} from "../../ts/Images/libImageFiles.ts";
  import {useRoute} from "vue-router";

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const isMenuOpen = ref(false);

  const sizes = ref<ImageSize[]>([])

/*  const originalSize = ref("e5793e78-6362-43ce-9373-b76913e34b8a")*/

/*  const originalFile = files.value?.files.find(f => f.sizeId === originalSize.value)

  const sizeId = originalFile?.sizeId*/

  const files = ref<ImageModel | null>(null)

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
    files.value = await GetImageFilesAsync(imageId);
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
          v-if="files"
          v-for="imageFile in files.files" :key="imageFile.id"
          :src="apiBaseUrl + '/Files/' + files?.id"
          alt="Preview"/>


    </div>

    <div
        class="info-section">

      Info 1

      <div v-if="files">
        <div v-for="imageFile in files.files" :key="imageFile.id">
          {{ files.name }}
          {{ files.description }}
          {{ imageFile.id }}
        </div>
      </div>


    </div>

  </div>

</template>
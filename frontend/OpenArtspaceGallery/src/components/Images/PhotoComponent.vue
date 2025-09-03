<script setup lang="ts">

import {onMounted, PropType, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";
import {DecodeImageResponse, ImageModel} from "../../ts/Images/libImageFiles.ts";

const props = defineProps({
  imageId: {
    type: String as PropType<string>,
    required: true
  }
})

  const isMenuOpen = ref(false);

  const sizes = ref<ImageSize[]>([])

  const files = ref<ImageModel>()

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
    files.value = await GetImageFilesAsync(props.imageId);
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
      Photo
    </div>

    <div
        class="info-section">

      <div
          v-for="imageFile in files?.files"
          :key="imageFile.id">

        {{ files?.name }}1
        {{ files?.description }}2
        {{ imageFile.id }}3

      </div>


      Info 1
    </div>

  </div>

</template>
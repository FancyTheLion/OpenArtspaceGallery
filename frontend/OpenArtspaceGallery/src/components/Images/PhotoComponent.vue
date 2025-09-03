<script setup lang="ts">

import {onMounted, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";

  const props = defineProps({
    imageId: String
  })

  const isMenuOpen = ref(false);

  const sizes = ref<ImageSize[]>([])

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
  }

  async function GetImagesSizesListAsync(): Promise<ImageSize[]>
  {
    return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetList")).json()))
        .imagesSizes
        .map(DecodeImageSizeDto)
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))
  }

/*  async function test(): Promise<void>
  {
    imagesSizes.value =  await GetImagesSizesListAsync();

    alert(imagesSizes.value)
  }*/

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
      Info
    </div>

  </div>

</template>
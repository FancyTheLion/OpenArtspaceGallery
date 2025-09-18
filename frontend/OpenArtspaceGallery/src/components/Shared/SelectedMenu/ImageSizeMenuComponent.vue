<script setup lang="ts">

import {PropType, ref} from "vue";
  import {ImageSize} from "../../../ts/imagesSizes/libImagesSizes.ts";

  const props = defineProps({
    imageSizes: {
      type: Array as PropType<ImageSize[]>,
      required: true
    }
  });

  const isMenuOpen = ref(false);

  const emit = defineEmits([ "select" ])

  /*const closeMenu = (e: MouseEvent) => {
    const target = e.target as HTMLElement;

    if (!target.closest(".menu-container"))
    {
      isMenuOpen.value = false;
    }
  };*/

  /*onMounted(() => {
    document.addEventListener("click", closeMenu);
  });

  /!* Optimization to eliminate bugs if the handler breaks *!/
  onUnmounted(() => {
    document.removeEventListener("click", closeMenu);
  });*/

  async function OnSizeSelected(size: ImageSize): Promise<void>
  {
    emit("select", size);
  }

  function ToggleMenu(): void
  {
    isMenuOpen.value = !isMenuOpen.value;
  }

  /*function CloseMenu(): void
  {
    isMenuOpen.value = false;
  }*/

</script>

<template>

    <img
        class="icon-button"
        src="/images/icons/imageMenuSize.webp"
        alt="Select image size"
        title="Select image size"
        @click="ToggleMenu"/>

    <div
        class="menu"
        :class="{ active: isMenuOpen }">

      <div>Image sizes:</div>

      <div
          v-for="size in props.imageSizes"
          :key="size.id"
          class="menu-item"
          @click="OnSizeSelected(size)">

        {{ size.name }} {{ size.width }} {{ size.height }}

      </div>

    </div>


</template>
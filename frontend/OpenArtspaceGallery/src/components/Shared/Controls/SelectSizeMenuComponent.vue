<script setup lang="ts">

import {onMounted, onUnmounted, PropType, ref} from "vue";
  import {ImageSize} from "../../../ts/imagesSizes/libImagesSizes.ts";

  const props = defineProps({
    imageSizes: {
      type: Array as PropType<ImageSize[]>,
      required: true
    }
  })

  const isMenuOpen = ref(false)
  const elementToDetectOutsideClick = ref<HTMLElement | null>(null)

  function ToggleMenu(): void
  {
    isMenuOpen.value = !isMenuOpen.value
  }

  function handleClickOutside(event: MouseEvent)
  {
    const target = event.target as Node

    if (elementToDetectOutsideClick.value && !elementToDetectOutsideClick.value.contains(target))
    {
      isMenuOpen.value = false
    }
  }

  onMounted(() => {
    document.addEventListener("click", handleClickOutside)
  })

  onUnmounted(() => {
    document.removeEventListener("click", handleClickOutside)
  })

</script>

<template>

  <div ref="elementToDetectOutsideClick">

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
          class="menu-item">

        {{ size.name }} {{ size.width }} {{ size.height }}

      </div>

    </div>

  </div>

</template>
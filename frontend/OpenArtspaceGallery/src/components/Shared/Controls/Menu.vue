<script setup lang="ts">

import {onMounted, onUnmounted, PropType, ref} from "vue";
import { MenuItemsExtended } from "../../../ts/Shared/Controls/libMenu.ts";

  const props = defineProps({
    menuItemsExtended: {
      type: Array as PropType<MenuItemsExtended[]>,
      required: true
    }
  })

  const isMenuOpen = ref(false)

  const elementToDetectOutsideClick = ref<HTMLElement | null>(null)

  const emit = defineEmits([ "stringSelected"])

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

  function SizeSelected(currentSizeId: string): void
    {
      emit("stringSelected", currentSizeId)
    }

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
          v-for="size in props.menuItemsExtended"
          :key="size.id"
          class="menu-item"
          @click="SizeSelected(size.id)">

        {{ size.name }} {{ size.width }} {{ size.height }}

      </div>

    </div>

  </div>

</template>
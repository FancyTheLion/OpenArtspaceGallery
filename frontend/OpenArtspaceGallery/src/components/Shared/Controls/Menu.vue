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

  function StringSelected(currentSizeId: string): void
    {
      emit("stringSelected", currentSizeId)
    }

</script>

<template>

  <div ref="elementToDetectOutsideClick">

    <img
        class="icon-button"
        src="/images/icons/imageMenu.webp"
        alt="Select string"
        title="Select string"
        @click="ToggleMenu"/>

    <div
        class="menu"
        :class="{ active: isMenuOpen }">

      <div
          v-for="string in props.menuItemsExtended"
          :key="string.id"
          class="menu-item"
          @click="StringSelected(string.id)">

        {{ string.name }} {{ string.width }} {{ string.height }}

      </div>

    </div>

  </div>

</template>
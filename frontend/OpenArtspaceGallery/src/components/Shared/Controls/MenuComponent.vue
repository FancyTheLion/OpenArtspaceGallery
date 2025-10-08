<script setup lang="ts">

import {onMounted, onUnmounted, PropType, ref} from "vue";
import { MenuItem } from "../../../ts/Shared/Controls/libMenu.ts";

  const props = defineProps({
    title: {
      type: String,
      required: true
    },
    items: {
      type: Array as PropType<MenuItem[]>,
      required: true
    }
  })

  const isMenuOpen = ref(false)

  const elementToDetectOutsideClick = ref<HTMLElement | null>(null)

  const emit = defineEmits([ "itemSelected"])

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

  function ItemSelected(currentId: string): void
  {
    emit("itemSelected", currentId)
  }

</script>

<template>

  <div ref="elementToDetectOutsideClick">

    <img
        class="icon-button"
        src="/images/icons/imageMenu.webp"
        alt="Select item"
        title="Select item"
        @click="ToggleMenu"/>

    <div
        class="menu"
        :class="{ active: isMenuOpen }">

      <div
        class="block">

        {{ props.title }}

      </div>

      <div
          v-for="item in props.items"
          :key="item.id"
          class="menu-item"
          @click="ItemSelected(item.id)">

        {{ item.name }}

      </div>

    </div>

  </div>

</template>
<script setup lang="ts">

import {onMounted, onUnmounted, PropType, ref} from "vue";
  import {ImageSize} from "../../../ts/imagesSizes/libImagesSizes.ts";

  const props = defineProps({
    photoSizes: {
      type: Array as PropType<ImageSize[]>,
      required: true
    }
  });

  const isMenuOpen = ref(false);

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

  onMounted(() => {
    document.addEventListener("click", closeMenu);
  });

  /* Optimization to eliminate bugs if the handler breaks */
  onUnmounted(() => {
    document.removeEventListener("click", closeMenu);
  });

</script>

<template>

    <img
        class="icon-button"
        src="/images/icons/photoMenuSize.webp"
        alt="Select photo size"
        title="Select photo size"
        @click="toggleMenu"/>

    <div
        class="menu"
        :class="{ active: isMenuOpen }">

      <div>Photo sizes:</div>

      <div v-for="size in props.photoSizes" :key="size.id">

          <div>
            {{size.name}} {{size.width}} {{size.height}}
          </div>

      </div>

    </div>


</template>
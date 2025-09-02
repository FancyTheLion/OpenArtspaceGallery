<script setup lang="ts">

  import {ref} from "vue";

  const props = defineProps({
    imageId: String
  })

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

  document.addEventListener("click", closeMenu);

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

        <a href="#">Link 1</a>
        <a href="#">Link 2</a>
        <a href="#">Link 3</a>
        <a href="#">Link 4</a>
        <a href="#">Link 5</a>

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
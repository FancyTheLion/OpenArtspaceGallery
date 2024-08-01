<script setup lang="ts">
  import {ref} from "vue";

  defineExpose({
    Show: ShowPopup
  })

  const props = defineProps({
    title: String,
    text: String
  })

  const isDisplayed = ref<boolean>(false)

  const emit = defineEmits([ "noPressed", "yesPressed" ])

  async function OnNoPressed()
  {
    emit('noPressed')

    await HidePopup()
  }

  async function OnYesPressed()
  {
    emit('yesPressed')
  }

  async function ShowPopup()
  {
    isDisplayed.value = true
  }

  async function HidePopup()
  {

    isDisplayed.value = false
  }

</script>

<template>

<!-- TODO: РАССКАЗАТЬ ПРО эмиты ЗДЕСЬ. Я ОЧЕНЬ ДОЛГО ПЫТАЛСЯ ВНИКНУТЬ И КАК ТО ВНИК, НО ХОЧУ услышать как оно есть на самом деле -->

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="popup-delete-album">
          {{props.title}} Deleting an album <!-- TODO: понять чем заполнять -->
          {{props.text}}

          <button
            @click ="async () => await OnYesPressed()">
            Yes
          </button>

          <button
            @click="async () => await OnNoPressed()">
            No
          </button>

        </div>

      </div>

    </div>

  </div>



</template>
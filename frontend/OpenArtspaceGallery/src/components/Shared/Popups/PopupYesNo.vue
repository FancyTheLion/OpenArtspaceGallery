<script setup lang="ts">
  import {ref} from "vue";

  defineExpose({
    Show: ShowPopup
  })

  const props = defineProps({
    title: {
      type: String,
      default: "",
      required: true
    },
    text: {
      type: String,
      default: "",
      required: true
    }
  })

  const isDisplayed = ref<boolean>(false)

  const emit = defineEmits([ "no", "yes" ])

  function OnNo()
  {
    HidePopup()

    emit("no")
  }

  function OnYes()
  {
    HidePopup()

    emit("yes")
  }

  function ShowPopup()
  {
    isDisplayed.value = true
  }

  function HidePopup()
  {
    isDisplayed.value = false
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

      <div class="popup-upper-layer">

        <div class="popup">

          <div class="popup-yes-no-content">

            <h1 class="popup-title">
              {{props.title}}
            </h1>

            <div class="popup-text-info-padding">
              {{props.text}}
            </div>

            <div class="popup-button-separation-container">

              <button
                  @click="OnNo()">
                No
              </button>

              <button
                  @click ="OnYes()">
                Yes
              </button>

            </div>

          </div>

        </div>

      </div>

  </div>

</template>
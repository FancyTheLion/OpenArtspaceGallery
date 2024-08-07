<script setup lang="ts">
import { reactive, ref} from "vue";

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
    },
    defaultValue: {
      type: String,
      default: "",
      required: true
  }
  })

  const isDisplayed = ref<boolean>(false)

  const emit = defineEmits([ "cancel", "ok" ])

  const valueFormData = reactive({
        value: ""
  })

  async function OnOk()
  {
    await HidePopup()

    emit("cancel")
  }

  async function OnCancel()
  {
    emit("ok", valueFormData.value)
  }

  async function ShowPopup()
  {
    valueFormData.value = props.defaultValue

    isDisplayed.value = true
  }

  async function HidePopup()
  {
    isDisplayed.value = false
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="popup-text-input-content">

          <div>

            <h1
              class="popup-title">

              {{props.title}}
            </h1>

            <div class="popup-text-info-padding">

              {{props.text}}
            </div>

            <input
                class="popup-input"
                v-model="valueFormData.value">

          </div>

          <div class="popup-button-separation-container">

            <button
                @click="async () => await OnCancel()">
              Cancel
            </button>

            <button
                @click ="async () => await OnOk()">
              Ok
            </button>

          </div>



        </div>

      </div>

    </div>

  </div>

</template>
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

  async function OnOk(): Promise<void>
  {
    await HidePopup()

    emit("ok")
  }

  function OnCancel(): Promise<void>
  {
    emit("ok", valueFormData.value)
  }

  function ShowPopup(): Promise<void>
  {
    valueFormData.value = props.defaultValue

    isDisplayed.value = true
  }

  function HidePopup(): Promise<void>
  {
    isDisplayed.value = false
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"/>

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

          <div class="popup-actions-buttons-container">

            <button
                @click="OnCancel()">
              Cancel
            </button>

            <button
                @click ="OnOk()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
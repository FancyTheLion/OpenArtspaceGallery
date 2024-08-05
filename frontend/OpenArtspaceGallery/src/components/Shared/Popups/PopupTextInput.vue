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

  const emit = defineEmits([ "noPressed", "yesPressed" ])

  const valueFormData = reactive({
        value: ""
  })

  async function OnNoPressed()
  {
    emit("noPressed")

    await HidePopup()
  }

  async function OnYesPressed()
  {
    emit("yesPressed", valueFormData.value)
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

        <div class="popup-rename-album">

          <div>

            <div>
              {{props.title}}
            </div>

            <div>
              {{props.text}}
            </div>

            <input
                v-model="valueFormData.value">

          </div>

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
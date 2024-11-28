<script setup lang="ts">

import {onMounted, reactive, ref} from "vue";
import useVuelidate from "@vuelidate/core";
import {maxLength, maxValue, minLength, minValue, required} from "@vuelidate/validators";

  defineExpose({
    Show: ShowPopup
  })

  const isDisplayed = ref<boolean>(false)

  const newImageSizeFormData = reactive({
    name: "",
    width: 0,
    height: 0
  })

  const newImageSizeRules = {
    name: {
      $autoDirty: true,
      required,
      minLength: minLength(5),
      maxLength: maxLength(50)
    },
    width: {
      $autoDirty: true,
      required,
      minValue: minValue(100),
      maxValue: maxValue(4000)
    },
    height: {
      $autoDirty: true,
      required,
      minValue: minValue(100),
      maxValue: maxValue(4000)
    }
  }

  const newImageSizeFormValidator = useVuelidate(newImageSizeRules, newImageSizeFormData)

  const emit = defineEmits([ "cancel", "ok" ])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await newImageSizeFormValidator.value.$validate()
  }

  function ClearInputField()
  {
    newImageSizeFormData.name = "";
    newImageSizeFormData.width = 0;
    newImageSizeFormData.height = 0;
  }

  function OnOk()
  {
    HidePopup()

    emit("ok", newImageSizeFormData)
  }

  function OnCancel()
  {
    HidePopup()

    emit("cancel")
  }

  function ShowPopup()
  {
    ClearInputField()

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

        <div class="popup-text-input-content">

          <div>
            Add new image size
          </div>

          Size name
          <input
            :class="(newImageSizeFormValidator.name.$error) ? 'new-album-invalid-field' : 'new-album-valid-field'"
            class="popup-input"
            v-model="newImageSizeFormData.name">

          Width
          <input
            :class="(newImageSizeFormValidator.width.$error) ? 'new-album-invalid-field' : 'new-album-valid-field'"
            class="popup-input"
            v-model="newImageSizeFormData.width">

          Height
          <input
            :class="(newImageSizeFormValidator.height.$error) ? 'new-album-invalid-field' : 'new-album-valid-field'"
            class="popup-input"
            v-model="newImageSizeFormData.height">

          <div class="popup-button-separation-container">

            <button
                @click="OnCancel()">
              Cancel
            </button>

            <button
                :disabled="newImageSizeFormValidator.$errors.length > 0"
                @click ="OnOk()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
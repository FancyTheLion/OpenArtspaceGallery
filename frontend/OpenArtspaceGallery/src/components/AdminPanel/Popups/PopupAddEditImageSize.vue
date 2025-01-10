<script setup lang="ts">

import {computed, onMounted, reactive, ref} from "vue";
import useVuelidate from "@vuelidate/core";
import {helpers, maxLength, maxValue, minLength, minValue, required} from "@vuelidate/validators";
import {WebClientSendPostRequest} from "../../../ts/libWebClient.ts";
import {
  DecodeImageSizeDimensionsExistenceResponse,
  DecodeImageSizeNameExistenceResponse
} from "../../../ts/imagesSizes/libImagesSizes.ts";

  defineExpose({
    ShowAsync: ShowPopupAsync
  })

  const props = defineProps({
    isNewImageSize: {
      type: Boolean,
      required: true
    }
  })

  const isDisplayed = ref<boolean>(false)

  const newImageSizeFormData = reactive({
    name: "",
    width: 0,
    height: 0
  })

// Adding the originalData variable to keep the original values while editing
  const originalData = reactive({
    name: "",
    width: 0,
    height: 0
  });

  // I supplement the validation rules taking into account the editing modes and adding the image size
  const newImageSizeRules = {
    name: {
      $autoDirty: true,
      required,
      minLength: minLength(1),
      maxLength: maxLength(50),
      isNameTaken: helpers.withAsync(async (name: string) => {
        if (!props.isNewImageSize) return true
        return await ValidateNameAsync(name)
      })
    },
    width: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isWidthTaken: helpers.withAsync(async (width: number) => {
        if (!props.isNewImageSize) return true
        return await ValidateDimensionsAsync(width, newImageSizeFormData.height)
      })
    },
    height: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isHeightTaken: helpers.withAsync(async (height: number) => {
        if (!props.isNewImageSize) return true
        return await ValidateDimensionsAsync(newImageSizeFormData.width, height);
      })
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
  }

  // Adding the originalData variable to keep the original values while editing
  async function InitFormAsync(name: string, width: number, height: number)
  {
    newImageSizeFormData.name = name;
    newImageSizeFormData.width = width;
    newImageSizeFormData.height = height;

    originalData.name = name;
    originalData.width = width;
    originalData.height = height;


    await newImageSizeFormValidator.value.$validate()
  }

  // Method that checks if changes have occurred
  function IsEdited() {
    return (
        newImageSizeFormData.name !== originalData.name ||
        newImageSizeFormData.width !== originalData.width ||
        newImageSizeFormData.height !== originalData.height
    );
  }

  // A computed property that will return true if the form data is different from the original.
  const isFormChanged = computed(() => {
    return (
        newImageSizeFormData.name !== originalData.name ||
        newImageSizeFormData.width !== originalData.width ||
        newImageSizeFormData.height !== originalData.height
    );
  });

  // Adding verification logic
  // Now the validator recalculates itself better
  async function OnOkAsync()
  {
    if (!props.isNewImageSize && !IsEdited()) {
      alert("No changes made. Cannot save.");
      return;
    }

    // Replaced, now reset is not needed
/*    const tmpWidth = newImageSizeFormValidator.value.width.$model
    newImageSizeFormValidator.value.width.$model = 0
    newImageSizeFormValidator.value.width.$model = tmpWidth

    const tmpHeight = newImageSizeFormValidator.value.height.$model
    newImageSizeFormValidator.value.height.$model = 0
    newImageSizeFormValidator.value.height.$model = tmpHeight*/

    await newImageSizeFormValidator.value.$validate()

    if (newImageSizeFormValidator.value.$errors.length > 0) {
      alert("Some fields aren't valid!")
      return
    }

    HidePopup()

    emit("ok", newImageSizeFormData)
  }

  function OnCancel()
  {
    HidePopup()

    emit("cancel")
  }

  async function ShowPopupAsync(name: string, width: number, height: number)
  {
    await InitFormAsync(name, width, height)

    isDisplayed.value = true
  }

  function HidePopup()
  {
    isDisplayed.value = false
  }

  async function ValidateNameAsync(name: string) : Promise<boolean>
  {
    if (!isDisplayed.value)
    {
      return false
    }

    return !await IsNameExistAsync(name)
  }

  async function IsNameExistAsync(name: string): Promise<boolean>
  {
    const response = await (await WebClientSendPostRequest("/ImagesSizes/IsExistByName",
        {
          "name": {
            "name": name
          }
        }))
        .json()

      return DecodeImageSizeNameExistenceResponse(response)
  }

  async function ValidateDimensionsAsync(width: number, height: number): Promise<boolean>
  {
    if (!isDisplayed.value)
    {
      return false
    }

    return !await IsDimensionsExistAsync(width, height)
  }

  async function IsDimensionsExistAsync(width: number, height: number): Promise<boolean>
  {
    const response = await (await WebClientSendPostRequest("/ImagesSizes/IsExistByDimensions",
        {
          "dimensions": {
            "width": width,
            "height": height
          }
        }))
        .json()

    return DecodeImageSizeDimensionsExistenceResponse(response)
  }

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="popup-images-sizes-text-input">

          <div
              v-if="props.isNewImageSize"
              class="popup-title">
            Add new image size
          </div>
          <div
              v-else
              class="popup-title">
            Edit image size
          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Size name
            </label>

            <input
                :class="(newImageSizeFormValidator.name.$error && !newImageSizeFormValidator.name.$pending) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-images-sizes-form-input"
                v-model="newImageSizeFormData.name"/>

          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Width
            </label>

            <input
                :class="((newImageSizeFormValidator.width.$error || newImageSizeFormValidator.height.$error) && !(newImageSizeFormValidator.width.$pending || newImageSizeFormValidator.height.$pending)) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-images-sizes-form-input"
                v-model="newImageSizeFormData.width"
                type="number">
          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Height
            </label>

            <input
                :class="((newImageSizeFormValidator.width.$error || newImageSizeFormValidator.height.$error) && !(newImageSizeFormValidator.width.$pending || newImageSizeFormValidator.height.$pending)) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-images-sizes-form-input"
                v-model="newImageSizeFormData.height"
                type="number">
          </div>

          <div class="popup-actions-buttons-container">

            <button
                @click="OnCancel()">
              Cancel
            </button>

          <!-- If the form has not changed or there is a mistake, the button becomes unavailable. -->
            <button
                :disabled="newImageSizeFormValidator.$errors.length > 0 || !isFormChanged"
                @click="async() => await OnOkAsync()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
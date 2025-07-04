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

  const isDisplayed = ref<boolean>(false)

  const addEditFormData = reactive({
    name: "",
    width: 0,
    height: 0
  })

  const originalData = {
    name: "",
    width: 0,
    height: 0
  }

  const addEditRules = {
    name: {
      $autoDirty: true,
      required,
      minLength: minLength(1),
      maxLength: maxLength(50),
      isNameTaken: helpers.withAsync(async (name: string) => await ValidateNameAsync(name))
    },
    width: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isWidthTaken: helpers.withAsync(async (width: number) => await ValidateDimensionsAsync(width, addEditFormData.height))
    },
    height: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isHeightTaken: helpers.withAsync(async (height: number) => await ValidateDimensionsAsync(addEditFormData.width, height))
    }
  }

  const isFormChanged = computed(() =>
  {
    return (
        addEditFormData.name !== originalData.name
        ||
        addEditFormData.width !== originalData.width
        ||
        addEditFormData.height !== originalData.height
    );
  });

  const addEditFormValidator = useVuelidate(addEditRules, addEditFormData)

  const isAddMode = ref<boolean>(false)

  const emit = defineEmits([ "cancel", "ok" ])

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
  }

  function InitForm(name: string, width: number, height: number): void
  {
    addEditFormData.name = name;
    addEditFormData.width = width;
    addEditFormData.height = height;

    originalData.name = name;
    originalData.width = width;
    originalData.height = height;
  }

  async function OnOkAsync(): Promise<void>
  {
    if (!isAddMode.value && !isFormChanged)
    {
      alert("No changes made. Cannot save.")
      return
    }

    await addEditFormValidator.value.$validate()

    if (addEditFormValidator.value.$errors.length > 0)
    {
      alert("Some fields aren't valid!")
      return
    }

    HidePopup()

    emit("ok", isAddMode.value, addEditFormData)
  }

  function OnCancel(): void
  {
    HidePopup()

    emit("cancel")
  }

  async function ShowPopupAsync(isAdd: boolean, name: string, width: number, height: number): Promise<void>
  {
    isAddMode.value = isAdd
    isDisplayed.value = true

    InitForm(name, width, height)
  }

  function HidePopup(): void
  {
    isDisplayed.value = false
  }

  async function ValidateNameAsync(name: string) : Promise<boolean>
  {
    if (!isDisplayed.value)
    {
      return false
    }

    // TODO: Add universal checks here (like check for "$@#" in name)

    if (!isAddMode.value && name === originalData.name)
    {
      return true
    }

    return !await IsExistAsync(name)
  }

  async function IsExistAsync(name: string): Promise<boolean>
  {
    const response = await (await WebClientSendPostRequest("/ImagesSizes/IsExistByName",
        {
          "imageSize": {
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

    // TODO: Add universal checks here (like check for negative numbers in dimensions)

    if (!isAddMode.value && width === originalData.width && height === originalData.height)
    {
      return true
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

        <div class="popup-text-input">

          <div
              v-if="isAddMode"
              class="popup-title">
            Add new image size
          </div>

          <div
              v-else
              class="popup-title">
            Edit image size
          </div>

          <div class="popup-form-row">

            <label class="popup-form-label">
              Size name
            </label>

            <input
                :class="(addEditFormValidator.name.$error && !addEditFormValidator.name.$pending) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-form-input"
                v-model="addEditFormData.name"/>

          </div>

          <div class="popup-form-row">

            <label class="popup-form-label">
              Width
            </label>

            <input
                :class="((addEditFormValidator.width.$error || addEditFormValidator.height.$error) && !(addEditFormValidator.width.$pending || addEditFormValidator.height.$pending)) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-form-input"
                v-model="addEditFormData.width"
                type="number">
          </div>

          <div class="popup-form-row">

            <label class="popup-form-label">
              Height
            </label>

            <input
                :class="((addEditFormValidator.width.$error || addEditFormValidator.height.$error) && !(addEditFormValidator.width.$pending || addEditFormValidator.height.$pending)) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-form-input"
                v-model="addEditFormData.height"
                type="number">
          </div>

          <div class="popup-actions-buttons-container">

            <button
                @click="OnCancel()">
              Cancel
            </button>

            <button
                :disabled="addEditFormValidator.$errors.length > 0 || (!isAddMode && !isFormChanged)"
                @click="async() => await OnOkAsync()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
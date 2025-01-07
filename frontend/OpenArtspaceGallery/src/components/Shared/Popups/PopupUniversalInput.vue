<script setup lang="ts">

  import {onMounted, reactive, ref} from "vue";
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
    title: {
      type: String,
      default: "",
      required: true
    }
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
      minLength: minLength(1),
      maxLength: maxLength(50),
      isNameTaken: helpers.withAsync(async (name: string) => await ValidateNameAsync(name))
    },
    width: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isWidthTaken: helpers.withAsync(async (width: number) => await ValidateDimensionsAsync(width, newImageSizeFormData.height))
    },
    height: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000),
      isHeightTaken: helpers.withAsync(async (height: number) => await ValidateDimensionsAsync(newImageSizeFormData.width, height))
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

  async function ClearFormAsync()
  {
    newImageSizeFormData.name = "";
    newImageSizeFormData.width = 0;
    newImageSizeFormData.height = 0;

    await newImageSizeFormValidator.value.$validate()
  }

  async function OnOkAsync()
  {

    const tmpWidth = newImageSizeFormValidator.value.width.$model
    newImageSizeFormValidator.value.width.$model = 0
    newImageSizeFormValidator.value.width.$model = tmpWidth

    const tmpHeight = newImageSizeFormValidator.value.height.$model
    newImageSizeFormValidator.value.height.$model = 0
    newImageSizeFormValidator.value.height.$model = tmpHeight

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

  async function ShowPopupAsync()
  {
    await ClearFormAsync()

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

          <div class="popup-title">
            {{props.title}}
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

            <button
                @click ="async() => await OnOkAsync()">
              Ok
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
<script setup lang="ts">

import {onMounted, reactive, ref} from "vue";
import useVuelidate from "@vuelidate/core";
import {maxLength, maxValue, minLength, minValue, required} from "@vuelidate/validators";
import {WebClientSendPostRequest} from "../../../ts/libWebClient.ts";
import {DecodeImageSizeNameExistenceResponse} from "../../../ts/imagesSizes/libImagesSizes.ts";

  defineExpose({
    Show: ShowPopup
  })

  const isDisplayed = ref<boolean>(false)

  const lastNameExistenceRequestId = ref<any>(null)

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
      maxLength: maxLength(50)
    },
    width: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
      maxValue: maxValue(4000)
    },
    height: {
      $autoDirty: true,
      required,
      minValue: minValue(10),
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

  function ClearForm()
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
    ClearForm()

    isDisplayed.value = true
  }

  function HidePopup()
  {
    isDisplayed.value = false
  }

  async function OnTestAsync()
  {
    alert("Result: " + await IsNameExistAsync(newImageSizeFormData.name))
  }

  async function OnNameChangedAsync(name: string): boolean
  {
    lastNameExistenceRequestId.value = crypto.randomUUID();

    QueryIsNameExistAsync(name, lastNameExistenceRequestId.value)
    .then(result => {
      if (result.requestId === lastNameExistenceRequestId)
      {

      }
    })
  }

  type IsNameExistQueryResult =
      {
        isExist: boolean,
        requestId: any
      }

  async function QueryIsNameExistAsync(name: string, requestId: any): Promise<IsNameExistQueryResult>
  {
    return {
      isExist: await IsNameExistAsync(name),
      requestId: requestId
    }
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

</script>

<template>

  <div v-if="isDisplayed">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="popup-images-sizes-text-input">

          <div class="popup-title">
            Add new image size
          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Size name
            </label>

            <input
                :class="(newImageSizeFormValidator.name.$error) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-images-sizes-form-input"
                v-model="newImageSizeFormData.name"/>

          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Width
            </label>

            <input
                :class="(newImageSizeFormValidator.width.$error) ? 'form-invalid-field' : 'form-valid-field'"
                class="popup-images-sizes-form-input"
                v-model="newImageSizeFormData.width"
                type="number">
          </div>

          <div class="popup-images-sizes-form-row">

            <label class="popup-images-sizes-form-label">
              Height
            </label>

            <input
                :class="(newImageSizeFormValidator.height.$error) ? 'form-invalid-field' : 'form-valid-field'"
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
                :disabled="newImageSizeFormValidator.$errors.length > 0"
                @click ="OnOk()">
              Ok
            </button>

            <div @click="async () => await OnTestAsync()">
              Test button
            </div>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
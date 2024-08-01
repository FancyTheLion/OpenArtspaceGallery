<script setup lang="ts">

import {onMounted, PropType, reactive, ref} from "vue";
import {maxLength, required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {WebClientSendPostRequest} from "../../../ts/libWebClient.ts";

const props = defineProps({
  currentAlbumId: {
    type: String as PropType<string | null>,
    required: false
  }
})

  const newAlbumFormData = reactive({
    name: ""
  })

  const newAlbumFormRules = {
    name: {
      $autoDirty: true,
      required,
      maxLength: maxLength(50)
    }
  }

  const emit = defineEmits(["newAlbumCreated"])

  const isNewAlbumPopupVisible = ref<boolean>(false)

  const newAlbumFormValidator = useVuelidate(newAlbumFormRules, newAlbumFormData)

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await newAlbumFormValidator.value.$validate()
  }

  async function ShowNewAlbumPopup()
  {
    isNewAlbumPopupVisible.value = true
  }

  async function HideNewAlbumPopup()
  {
    ClearInputField()

    isNewAlbumPopupVisible.value = false
  }

  async function ClearInputField()
  {
    newAlbumFormData.name = "";
  }

  async function SendAlbumToBackend()
  {
    const response = await WebClientSendPostRequest(
      "/Albums/New",
        {
          "albumToAdd": {
            "name": newAlbumFormData.name,
            "parentId": props.currentAlbumId
          }
        }
    )

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    await HideNewAlbumPopup()
    emit("newAlbumCreated", props.currentAlbumId)
  }

</script>

<template>

  <!-- New album button -->
  <div class="new-album-button-container">

    <div
        class="new-album-button"
        @click="async () => await ShowNewAlbumPopup()">

        <img class="new-album-button-image" src="/images/AddNewAlbum.webp" alt="Create new album" title="Create new album"/>
    </div>

  </div>

  <!-- New album form popup -->
  <div v-if="isNewAlbumPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

          <div>
            Album name:
          </div>

          <div>
            <input
                :class="(newAlbumFormValidator.name.$error) ? 'new-album-invalid-field' : 'new-album-valid-field'"
                type="text"
                v-model="newAlbumFormData.name"/>
          </div>

          <div class="new-album-form-button-container">

            <button
              class="test3"
              type="button"
              @click="async() => await HideNewAlbumPopup()">
              Cancel
            </button>

            <button
              class="new-album-form-buttons"
              type="button"
              :disabled="newAlbumFormValidator.$errors.length > 0"
              @click="async() => await SendAlbumToBackend()">
              Download
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
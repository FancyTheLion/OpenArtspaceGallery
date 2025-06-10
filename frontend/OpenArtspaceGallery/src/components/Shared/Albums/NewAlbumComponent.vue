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

  const isAddActionPopupVisible = ref<boolean>(false)

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

  function ShowAddMenuPopup()
  {
    isAddActionPopupVisible.value = true
  }

  function HideAddMenuPopup()
  {
    isAddActionPopupVisible.value = false
  }

  function ShowNewAlbumPopup()
  {
    isNewAlbumPopupVisible.value = true
  }

  function HideNewAlbumPopup()
  {
    ClearInputField()

    isNewAlbumPopupVisible.value = false
  }

  function ClearInputField()
  {
    newAlbumFormData.name = "";
  }

  async function CreateAlbumAsync()
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

  <!-- Add action button -->
  <div class="add-action-button-container">

    <div
        class="add-action-button"
        @click="ShowAddMenuPopup()">

        <img class="new-album-button-image" src="/images/icons/addNewAlbum.webp" alt="Create new album" title="Create new album"/>
    </div>

  </div>

  <!-- Add menu form popup -->
  <div v-if="isAddActionPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

          <button
              type="button"
              @click="HideAddMenuPopup()">
            Cancel
          </button>

          <button
              class="new-album-form-buttons"
              type="button"
              @click="async() => await ShowNewAlbumPopup()">
            Create
          </button>

        </div>

      </div>

    </div>

  </div>

  <!-- New album form popup -->
  <div v-if="isNewAlbumPopupVisible">

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

          <div>
            Album name:
          </div>

          <div>
            <input
                :class="(newAlbumFormValidator.name.$error) ? 'form-invalid-field' : 'form-valid-field'"
                type="text"
                v-model="newAlbumFormData.name"/>
          </div>

          <div class="new-album-form-button-container">

            <button
              type="button"
              @click="HideNewAlbumPopup()">
              Cancel
            </button>

            <button
              class="new-album-form-buttons"
              type="button"
              :disabled="newAlbumFormValidator.$errors.length > 0"
              @click="async() => await CreateAlbumAsync()">
              Create
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

</template>
<script setup lang="ts">

import {onMounted, PropType, reactive} from "vue";
import {maxLength, required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {WebClientSendPostRequest} from "../../../ts/libWebClient.ts";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
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

  const emit = defineEmits(["newAlbumCreated", "close"])

  const newAlbumFormValidator = useVuelidate(newAlbumFormRules, newAlbumFormData)

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await newAlbumFormValidator.value.$validate()
  }

  function HideNewAlbumPopup()
  {
    ClearInputField()

    emit("close");
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

  <!-- New album form popup -->
    <div class="popup-lower-layer"/>

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

</template>
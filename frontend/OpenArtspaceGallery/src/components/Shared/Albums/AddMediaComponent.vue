<script setup lang="ts">

import {onMounted, PropType, reactive, ref} from "vue";
import {maxLength, required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {WebClientPostForm, WebClientSendPostRequest} from "../../../ts/libWebClient.ts";

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

  const addImageFileForm = reactive({
    name: "",
    description: "",
    file: null as File | null
  });

  const emit = defineEmits(["newAlbumCreated", "newImageAdded"])

  const isAddMenuPopupVisible = ref<boolean>(false)

  const isAddImagePopupVisible = ref<boolean>(false)

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
    isAddMenuPopupVisible.value = true
  }

  function ShowAddImagePopup()
  {
    isAddImagePopupVisible.value = true
  }

  function HidePopup()
  {
    isAddMenuPopupVisible.value = false
    isAddImagePopupVisible.value = false
  }

  function ShowNewAlbumPopup()
  {
    isNewAlbumPopupVisible.value = true
  }

  function HideNewAlbumPopup()
  {
    ClearInputField()

    isAddMenuPopupVisible.value = false
    isNewAlbumPopupVisible.value = false
  }

  function NewAlbumStepBack()
  {
    ClearInputField()

    isNewAlbumPopupVisible.value = false
  }

  function AddImageStepBack()
  {
    isAddImagePopupVisible.value = false
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

    HideNewAlbumPopup()
    emit("newAlbumCreated", props.currentAlbumId)
  }

  async function AddImageAsync()
  {
    if (!addImageFileForm.file)
    {
      alert("Need a file!");
      return;
    }

    const fileToUpload: File = addImageFileForm.file;

    const uploadFormData = new FormData();
    uploadFormData.append("file", fileToUpload);

    const fileUploadResponse = await WebClientPostForm("/Files/Upload", uploadFormData);

    if (!fileUploadResponse.ok)
    {
      alert("Failed to upload file!");
      return;
    }

    const uploadedFileId: string = (await fileUploadResponse.json()).fileInfo.id

    const response = await WebClientSendPostRequest(
        "/Images/Add",
        {
          image: {
            id: String,
            name: addImageFileForm.name,
            description: addImageFileForm.description,
            albumId: props.currentAlbumId,
            creationTime: new Date().toISOString(),
            sourceFileId: uploadedFileId
          }
        }
    );

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    HidePopup()

    emit("newImageAdded", props.currentAlbumId)
  }

  function handleFileChange(event: Event)
  {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0)
    {
      addImageFileForm.file = target.files[0];
    }
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
  <div v-if="isAddMenuPopupVisible">

    <div class="popup-lower-layer"></div>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="add-menu-form">

          <button
              class="add-media-form-buttons"
              type="button"
              @click="async() => await ShowNewAlbumPopup()">
            Create album
          </button>

          <button
              class="add-media-form-buttons"
              type="button"
              @click="async() => await ShowAddImagePopup()">
            Add image
          </button>

          <button
              type="button"
              @click="HidePopup()">
            Cancel
          </button>

        </div>

      </div>

    </div>

  </div>

  <!-- Add image form popup -->
  <div v-if="isAddImagePopupVisible">

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="add-media-form">

          <div>
            Image name
          </div>

          <div>
            <input
                type="text"
                v-model="addImageFileForm.name"/>
          </div>

          <div>
            Image description
          </div>

          <div>
            <input
                type="text"
                v-model="addImageFileForm.description"/>
          </div>

          <input
              ref="addImageFileInput"
              type="file"
              accept="image/png, image/jpeg, image/gif, image/webp"
              @change="handleFileChange"/>

          <div class="add-media-form-button-container">

            <button
                class="add-media-form-buttons"
                type="button"
                @click="AddImageStepBack()">
              Back
            </button>

            <button
                class="add-media-form-buttons"
                type="button"
                @click="HidePopup()">
              Cancel
            </button>

            <button
                class="add-media-form-buttons"
                type="button"
                @click="async() => await AddImageAsync()">
              Add
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>

  <!-- New album form popup -->
  <div v-if="isNewAlbumPopupVisible">

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="add-media-form">

          <div>
            Album name:
          </div>

          <div>
            <input
                :class="(newAlbumFormValidator.name.$error) ? 'form-invalid-field' : 'form-valid-field'"
                type="text"
                v-model="newAlbumFormData.name"/>
          </div>

          <div class="add-media-form-button-container">

            <button
                class="add-media-form-buttons"
                type="button"
                @click="NewAlbumStepBack()">
              Back
            </button>

            <button
              class="add-media-form-buttons"
              type="button"
              @click="HideNewAlbumPopup()">
              Cancel
            </button>

            <button
              class="add-media-form-buttons"
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
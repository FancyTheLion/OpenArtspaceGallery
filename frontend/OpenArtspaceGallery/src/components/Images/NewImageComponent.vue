<script setup lang="ts">

import {PropType, reactive} from "vue";
import {WebClientPostForm, WebClientSendPostRequest} from "../../ts/libWebClient.ts";

  const props = defineProps({
    currentAlbumId: {
      type: String as PropType<string>,
      required: true
    }
  })

  const addImageFileForm = reactive({
    name: "",
    description: "",
    file: null as File | null
  });

  const emit = defineEmits(["imageUploaded", "cancelled"])

  async function OnAddImageAsync()
  {
    const uploadedFileId = await UploadImageAsync()

    if (!uploadedFileId)
    {
      alert("No uploaded image id.")
      return;
    }

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

    emit("imageUploaded")
  }

  function HandleFileChange(event: Event)
  {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0)
    {
      addImageFileForm.file = target.files[0];
    }
  }

  async function UploadImageAsync()
  {
    if (!addImageFileForm.file)
    {
      alert("Need a file!");
      return;
    }

    const fileToUpload: File = addImageFileForm.file;

    const uploadFormData = new FormData();
    uploadFormData.append("file", fileToUpload);

    const response = await WebClientPostForm("/Files/Upload", uploadFormData);

    if (!response.ok)
    {
      alert("Failed to upload file!");
      return;
    }

    const responseBody = await response.json()
    const uploadedFileId: string = responseBody.fileInfo.id;

    return uploadedFileId
  }

  function OnCancel()
  {
    emit("cancelled")
  }

</script>

<template>

  <!-- Add image form popup -->
  <div class="popup-lower-layer"/>

    <div class="popup-upper-layer">

      <div class="popup">

        <div class="new-album-add-new-album-form">

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
              @change="HandleFileChange"/>

          <div class="add-album-form-button-container">

            <button
                class="add-album-form-buttons"
                type="button"
                @click="OnCancel()">
              Cancel
            </button>

            <button
                class="add-album-form-buttons"
                type="button"
                @click="async() => await OnAddImageAsync()">
              Add
            </button>

          </div>

        </div>

      </div>

    </div>

</template>
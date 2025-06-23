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

/*  const isAddMenuPopupVisible = ref<boolean>(false)

  const isAddImagePopupVisible = ref<boolean>(false)*/

  const emit = defineEmits(["newImageAdded"])




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


/*  function NewAlbumStepBack()
  {
    ClearInputField()

    isNewAlbumPopupVisible.value = false
  }*/

/*  function AddImageStepBack()
  {
    isAddImagePopupVisible.value = false
  }*/

/*
  function ShowAddMenuPopup()
  {
    isAddMenuPopupVisible.value = true
  }*/

/*  function ShowAddImagePopup()
  {
    isAddImagePopupVisible.value = true
  }*/

/*  function HidePopup()
  {
    isAddMenuPopupVisible.value = false
    isAddImagePopupVisible.value = false
  }*/

</script>

<template>

  <!-- Add image form popup -->
<!--  <div v-if="isAddImagePopupVisible">-->

<!--    <div class="popup-upper-layer">

      <div class="popup">-->

        <div class="add-album-form">

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

          <div class="add-album-form-button-container">

<!--            <button
                class="add-album-form-buttons"
                type="button">
              Back
            </button>-->

            <button
                class="add-album-form-buttons"
                type="button">
              Cancel
            </button>

            <button
                class="add-album-form-buttons"
                type="button"
                @click="async() => await AddImageAsync()">
              Add
            </button>

          </div>

        </div>

<!--      </div>

    </div>-->

<!--  </div>-->

</template>
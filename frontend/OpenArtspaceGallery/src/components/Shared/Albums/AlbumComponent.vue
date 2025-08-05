<script setup lang="ts">
import {onMounted, PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";
  import {
    WebClientSendDeleteRequest,
    WebClientSendGetRequest,
    WebClientSendPostRequest
  } from "../../../ts/libWebClient.ts";
  import PopupYesNo from "../Popups/PopupYesNo.vue";
  import PopupTextInput from "../Popups/PopupTextInput.vue";
import {DecodeLastImagesResponse, LastImage} from "../../../ts/Images/libLastImages.ts";

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const emit = defineEmits(["albumDeleted", "albumRenamed"])

  const isButtonsToolbarVisible = ref<boolean>(false)

  const deleteAlbumPopupRef = ref<InstanceType<typeof PopupYesNo>>()

  const renameAlbumPopupRef = ref<InstanceType<typeof PopupTextInput>>()

  const lastImagesInAlbum = ref<LastImage[]>([])

  const lastImageCount: number = 4;

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    lastImagesInAlbum.value = await GetLastImagesAsync(props.info?.id)
  }

  function ShowAlbumToolbar(): void
  {
    isButtonsToolbarVisible.value = true
  }

  function HideAlbumToolbar(): void
  {
    isButtonsToolbarVisible.value = false
  }

  function ShowAlbumDeleteConfirmation(): void
  {
    if (deleteAlbumPopupRef.value === undefined)
    {
      alert("Bug in code! Variable deleteAlbumPopupRef is not initialized!")
      return
    }

    deleteAlbumPopupRef.value!.Show()
  }

  function ShowAlbumRenameConfirmation(): void
  {
    if (renameAlbumPopupRef.value === undefined)
    {
      alert("Bug in code! Variable renameAlbumPopupRef is not initialized!")
      return
    }

    renameAlbumPopupRef.value!.Show()
  }

  async function DeleteAlbumAsync(): Promise<void>
  {
    const response = await WebClientSendDeleteRequest("/Albums/"  + props.info.id)

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    emit("albumDeleted", props.info.id)
  }

  async function RenameAlbumAsync(newName: string): Promise<void>
  {
    const response = await WebClientSendPostRequest("/Albums/" + props.info.id + "/Rename",
        {
            "renameAlbumInfo": {
              "newName": newName
            }
        })

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    emit("albumRenamed", props.info.id)
  }

  async function GetLastImagesAsync(currentAlbumId: String): Promise<LastImage[]>
  {
    return DecodeLastImagesResponse((await (await WebClientSendGetRequest("/Images/ByAlbum/" + currentAlbumId + "/lastImages/" + lastImageCount)).json()))
        .lastImages
        .sort((a: LastImage, b: LastImage) => a.creationTime.getTime() - b.creationTime.getTime())
  }

</script>

<template>

  <div class="album-container"
       @mouseenter="ShowAlbumToolbar()"
       @mouseleave="HideAlbumToolbar()">

    <div v-if="lastImagesInAlbum.length === 0" class="album-upper-part">No images</div>

    <!-- Lower layer, album content -->
    <a class="album-link-full" :href="'/albums/' + props.info.id">

      <div class="album-content-layer">

        <div class="album-upper-part">

          <!-- Last images thumbnails -->
          <div
            class="album-thumbnails-container">

              <img
                  v-for="lastImage in lastImagesInAlbum" :key="lastImage.id"
                  class="album-thumbnail-image"
                  :src="apiBaseUrl + '/Files/' + lastImage.thumbnailId"
                  alt="Preview" />

          </div>

        </div>

        <div class="album-lower-part">

            <div class="album-name">
              {{ props.info.name }}
            </div>

            <div class="album-creation-date">
              {{ moment(props.info?.creationTime).format("DD.MM.YYYY HH:mm:ss") }}
            </div>

        </div>

      </div>

    </a>

    <!-- Upper layer, toolbar -->
    <div class="album-toolbar-layer" v-if="isButtonsToolbarVisible">

      <div class="album-toolbar">

        <img
            class="album-toolbar-rename-button"
            src="/images/icons/rename.webp"
            alt="Rename album"
            title="Rename album"
            @click="ShowAlbumRenameConfirmation()" />

        <img
            class="album-toolbar-delete-button"
            src="/images/icons/delete.webp"
            alt="Delete album"
            title="Delete album"
            @click="ShowAlbumDeleteConfirmation()" />

      </div>

    </div>

  </div>


  <PopupYesNo
      title="Confirmation"
      text="Are you sure you want to delete the album?"
      ref="deleteAlbumPopupRef"
      @yes="async () => await DeleteAlbumAsync()" />

  <PopupTextInput
      title="Rename album"
      text="Please provide the new album name:"
      :defaultValue="props.info.name"
      ref="renameAlbumPopupRef"
      @ok="async (nn) => await RenameAlbumAsync(nn)" />

</template>

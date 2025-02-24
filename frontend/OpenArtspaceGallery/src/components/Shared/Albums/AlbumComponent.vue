<script setup lang="ts">
  import {PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";
  import {WebClientSendDeleteRequest, WebClientSendPostRequest} from "../../../ts/libWebClient.ts";
  import PopupYesNo from "../Popups/PopupYesNo.vue";
  import PopupTextInput from "../Popups/PopupTextInput.vue";

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

  const emit = defineEmits(["albumDeleted", "albumRenamed"])

  const isButtonsToolbarVisible = ref<boolean>(false)

  const deleteAlbumPopupRef = ref<InstanceType<typeof PopupYesNo>>()

  const renameAlbumPopupRef = ref<InstanceType<typeof PopupTextInput>>()

  function ShowAlbumToolbar()
  {
    isButtonsToolbarVisible.value = true
  }

  function HideAlbumToolbar()
  {
    isButtonsToolbarVisible.value = false
  }

  function ShowAlbumDeleteConfirmation()
  {
    if (deleteAlbumPopupRef.value === undefined)
    {
      alert("Bug in code! Variable deleteAlbumPopupRef is not initialized!")
      return
    }

    deleteAlbumPopupRef.value!.Show()
  }

  function ShowAlbumRenameConfirmation()
  {
    if (renameAlbumPopupRef.value === undefined)
    {
      alert("Bug in code! Variable renameAlbumPopupRef is not initialized!")
      return
    }

    renameAlbumPopupRef.value!.Show()
  }

  async function DeleteAlbumAsync()
  {
    const response = await WebClientSendDeleteRequest("/Albums/"  + props.info.id)

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    emit("albumDeleted", props.info.id)
  }

  async function RenameAlbumAsync(newName: string)
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

</script>

<template>

  <div class="album-container"
       @mouseenter="ShowAlbumToolbar()"
       @mouseleave="HideAlbumToolbar()">

    <!-- Lower layer, album content -->
    <a class="album-link-full" :href="'/albums/' + props.info.id">

      <div class="album-content-layer">

          <div class="album-upper-part">
              Photos will be here
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

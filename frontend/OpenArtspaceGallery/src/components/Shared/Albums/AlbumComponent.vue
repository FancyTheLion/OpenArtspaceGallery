<script setup lang="ts">
  import {PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";
  import {WebClientSendDeletRequest} from "../../../ts/libWebClient.ts";
  import PopupYesNo from "../Popups/PopupYesNo.vue";

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

  const IsButtonsToolbarVisible = ref<boolean>(false)
  const deleteAlbumPopup = ref(null)

  const emit = defineEmits(["albumDeleted"])


  async function ShowAlbumToolbar()
  {
    IsButtonsToolbarVisible.value = true
  }

  async function HideAlbumToolbar()
  {
    IsButtonsToolbarVisible.value = false
  }

  async function ShowAlbumDeletionConfirmationAsync()
  {
    await deleteAlbumPopup.value.Show()
  }

  async function DeleteAlbumAsync()
  {
    const request = await WebClientSendDeletRequest("/Albums/"  + props.info.id,
        {
          "albumId": props.info.id
        })

    if (!request.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    emit("albumDeleted", props.info?.id)
  }

</script>

<template>

  <div class="album-container"
       @mouseover="async () => await ShowAlbumToolbar()"
       @mouseout="async () => await HideAlbumToolbar()">

    <!-- Lower layer, album content -->
    <div class="album-content-layer">

        <div class="album-upper-part">

          <a class="album-link-full" :href="'/albums/' + props.info.id">
            Top part
          </a>

        </div>

        <div class="album-lower-part">

          <a class="album-link-full" :href="'/albums/' + props.info.id">
            <div class="album-name">{{ props.info.name }}</div>
            <div class="album-creation-date">{{ moment(props.info?.creationTime).format("DD.MM.YYYY HH:mm:ss") }}</div>
          </a>

        </div>

    </div>

    <!-- Upper layer, toolbar -->
    <div>

      <div class="album-toolbar-layer" v-if="IsButtonsToolbarVisible">

        <img
            class="album-delete-button"
            src="/public/images/close.webp"
            @click="async () => await ShowAlbumDeletionConfirmationAsync()">

      </div>

    </div>

  </div>

  <PopupYesNo
      ref="deleteAlbumPopup"
      @yesPressed="async () => await DeleteAlbumAsync()"/>

</template>

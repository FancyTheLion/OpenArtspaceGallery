<script setup lang="ts">
import {PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";
import {WebClientSendDeletRequest} from "../../../ts/libWebClient.ts";

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

  const IsButtonsToolbarVisible = ref<boolean>(false)

 const emit = defineEmits(["albumDeleted"])


  async function ShowAlbumToolbar()
  {
    IsButtonsToolbarVisible.value = true
  }

  async function HideAlbumToolbar()
  {
    IsButtonsToolbarVisible.value = false
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
            @click="async () => await DeleteAlbumAsync()">

      </div>

    </div>



  </div>

<!--  <a class="album-link-full" :href="'/albums/' + props.info.id">

    <div class="album-container">

      &lt;!&ndash; Upper part for photos &ndash;&gt;
      <div class="album-upper-part">
        Top part
      </div>

      &lt;!&ndash; Lower part for albums name and so on &ndash;&gt;
      <div class="album-lower-part">

        <div class="album-name">{{ props.info.name }}</div>
        <div class="album-creation-date">{{ moment(props.info?.creationTime).format("DD.MM.YYYY HH:mm:ss") }}</div>

      </div>

    </div>

  </a>-->

</template>

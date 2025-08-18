<script setup lang="ts">

import {Image} from "../../ts/Images/libImages.ts";
  import {PropType, ref} from "vue";
import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";

  const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

  const props = defineProps({
    image: {
      type: Object as PropType<Image>,
      required: true
    },
    isShowImageName: {
      type: Boolean,
      required: true
    }
  })

  const isVisible = ref<boolean>(false)

  const brokenThumbnail = '/images/icons/brokenImage.webp';

  const fileId = ref<String>();

  function OnPreviewImageError(event: Event): void
  {
    const target = event.target as HTMLImageElement;
    target.src = brokenThumbnail;
  }

  async function ShowFullSizePhoto()
  {
    isVisible.value = true

    fileId.value = await GetFileIdAsync(props.image?.id)
  }



  async function HideFullSizePhoto()
  {
    isVisible.value = false
  }

  async function DoNothing()
  {

  }

  async function GetFileIdAsync(imageFile: String): Promise<String>
  {
    return (await WebClientSendGetRequest("/Images/GetOriginalId/" + imageFile)).json()
  }

</script>

<template>

  <div class="thumbnail-container">

    <img
        :src="apiBaseUrl + '/Files/' + image.thumbnailId"
        alt="Preview"
        class="thumbnail-image"
        @error="OnPreviewImageError"
        @click="ShowFullSizePhoto()"/>

    <div
        v-if="props.isShowImageName"
        class="thumbnail-name">
      {{ image.name }}
    </div>

  </div>

  <div v-if="isVisible">

    <!-- Popup lower layer -->
    <div class="popup-lower-layer">

    </div>

    <!-- Popup upper layer -->
    <div class="popup-upper-layer">

      <div class="popup-main-image-section" @click.stop="async () => await DoNothing()">

        <img
            class="popup-close-button"
            src="/images/icons/delete.webp"
            alt="Closed full size photo"
            @click="async() => await HideFullSizePhoto()" />

        <img
            class="popup-image"
            :src="apiBaseUrl + '/Files/' + fileId"
            alt="Full size photo"/>

      </div>

    </div>

  </div>
</template>
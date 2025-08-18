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



/*  async function HideFullSizePhoto()
  {
    isVisible.value = false
  }*/

/*  async function DoNothing()
  {

  }*/

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

    <img class="popup-image" :src="apiBaseUrl + '/Files/' + fileId"  alt="Photo"/>

<!--    &lt;!&ndash; Popup upper layer &ndash;&gt;
    <div class="popup-upper-layer"
         @click="async() => await HideFullSizePhoto()">-->

<!--
      <div class="popup-main-image-section" @click.stop="async () => await DoNothing()">
-->

<!--        <img
            class="popup-close-button"
            src="/images/close.webp"
            alt="Закрыть полноразмерное фото"
            @click="async() => await HideFullSizePhoto()" />-->

<!--        <img
            class="popup-image"
            :src="apiBaseUrl + '/Files/' + GetFileIdAsync(props.image?.id)"
            alt="Full screen photo"/>-->

<!--      </div>-->

<!--    </div>-->

  </div>
</template>
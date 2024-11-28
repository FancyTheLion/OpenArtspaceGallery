<script setup lang="ts">
import {onMounted, ref} from "vue";
import {
  DecodeImageSizeDto,
  DecodeImagesSizesResponse,
  ImageSize, NewImageSize
} from "../../ts/imagesSizes/libImagesSizes.ts";
import {
  WebClientSendDeleteRequest,
  WebClientSendGetRequest, WebClientSendPostRequest,
} from "../../ts/libWebClient.ts";
  import PopupYesNo from "../Shared/Popups/PopupYesNo.vue";
  import PopupInputImageSize from "./Popups/PopupInputImageSize.vue";

  const imagesSizes = ref<ImageSize[]>([])

  const deleteImageSizePopupRef = ref<InstanceType<typeof PopupYesNo>>()

  const addImageSizePopupRef = ref<InstanceType<typeof PopupInputImageSize>>()

  const imageSizeToDelete = ref<string>("")

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await RefreshImageSizesList()
  }

  async function GetImagesSizesList()
  {
      return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetImagesSizesList")).json()))
        .imagesSizes
        .map(DecodeImageSizeDto)
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))
  }

  async function DeleteImageSizeAsync()
  {
    const response = await WebClientSendDeleteRequest("/ImagesSizes/" + imageSizeToDelete.value)

    await RefreshImageSizesList()

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }
  }

  async function CreateImageSizeAsync(newImageSize: NewImageSize)
  {
    const request = await WebClientSendPostRequest("/ImagesSizes/AddImageSize",
        {
          "imageSize": {
            "name": newImageSize.name,
            "width": newImageSize.width,
            "height": newImageSize.height
          }
        })

    await RefreshImageSizesList()

    if (!request.ok)
    {
      alert("An error happened. Try again later.")
      return
    }
  }

  async function RefreshImageSizesList()
  {
    imagesSizes.value = await GetImagesSizesList()
  }

  async function ShowImageSizeDeleteConfirmationAsync(id: string)
  {
    imageSizeToDelete.value = id;

    await deleteImageSizePopupRef.value!.Show()
  }

  function ShowNewImageSizePopupAsync()
  {
    addImageSizePopupRef.value!.Show()
  }

</script>

<template>

  <div class="admin-panel-images-sizes-container">

    <div class="table-name-header">Sizes</div>

    <div class="pseudo-link"
         @click="ShowNewImageSizePopupAsync()">

      Add new image size

    </div>

    <table
      class="table">

      <thead>
        <tr>
          <th class="table-cells-header">Size name</th>
          <th class="table-cells-header">Width</th>
          <th class="table-cells-header">Height</th>
          <th class="table-cells-header">Actions</th>
        </tr>
      </thead>

      <tbody>

        <tr
          v-for="(image) in imagesSizes"
          :key="image.id"
          class="table-row">

          <td class="table-cells">{{ image.name }}</td>
          <td class="table-cells">{{ image.width }}</td>
          <td class="table-cells">{{ image.height }}</td>
          <td class="table-actions-cells">

            <img
              class="table-close-button"
              src="/public/images/icons/delete.webp"
              alt="Delete image size"
              title="Delete image size"
              @click="async () => await ShowImageSizeDeleteConfirmationAsync(image.id)"
            />

          </td>

        </tr>

      </tbody>

    </table>

    <div class="pseudo-link"
      @click="ShowNewImageSizePopupAsync()">

      Add new image size

    </div>

    <PopupYesNo
        title="Confirmation"
        text="Are you sure you want to delete the image size?"
        ref="deleteImageSizePopupRef"
        @yes="async () => await DeleteImageSizeAsync()" />

    <PopupInputImageSize
        ref="addImageSizePopupRef"
        @ok="async (nIS) => await CreateImageSizeAsync(nIS)"/>

  </div>

</template>
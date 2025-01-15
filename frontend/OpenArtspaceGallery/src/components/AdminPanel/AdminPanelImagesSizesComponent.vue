<script setup lang="ts">
import {onMounted, ref} from "vue";
import {
  DecodeImageSizeDto,
  DecodeImagesSizesResponse,
  ImageSize, NewImageSize,
} from "../../ts/imagesSizes/libImagesSizes.ts";
import {
  WebClientSendDeleteRequest,
  WebClientSendGetRequest, WebClientSendPostRequest,
} from "../../ts/libWebClient.ts";
  import PopupYesNo from "../Shared/Popups/PopupYesNo.vue";
  import PopupAddEditImageSize from "./Popups/PopupAddEditImageSize.vue";

  const imagesSizes = ref<ImageSize[]>([])

  const deleteImageSizePopupRef = ref<InstanceType<typeof PopupYesNo>>()

  const addEditImageSizePopupRef = ref<InstanceType<typeof PopupAddEditImageSize>>()

  const imageSizeToDeleteId = ref<string>("")

  const imageSizeToEditId = ref<string>("")

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await RefreshImageSizesListAsync()
  }

  async function GetImagesSizesListAsync()
  {
      return DecodeImagesSizesResponse((await (await WebClientSendGetRequest("/ImagesSizes/GetImagesSizesList")).json()))
        .imagesSizes
        .map(DecodeImageSizeDto)
        .sort((a: ImageSize, b: ImageSize) => a.name.localeCompare(b.name))
  }

  async function DeleteImageSizeAsync()
  {
    const response = await WebClientSendDeleteRequest("/ImagesSizes/" + imageSizeToDeleteId.value)

    await RefreshImageSizesListAsync()

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

      if (!request.ok)
      {
        alert("An error happened. Try again later.")
        return
      }

    await RefreshImageSizesListAsync()
  }

  async function EditImageSizeAsync(newImageSize: NewImageSize)
  {
    const request = await WebClientSendPostRequest("/ImagesSizes/UpdateImageSizeById",
        {
          "imageSize": {
            "id": imageSizeToEditId.value,
            "name": newImageSize.name,
            "width": newImageSize.width,
            "height": newImageSize.height
          }
        })

    if (!request.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    await RefreshImageSizesListAsync()
  }

  async function RefreshImageSizesListAsync()
  {
    imagesSizes.value = await GetImagesSizesListAsync()
  }

  function ShowDeleteImageSizeConfirmation(id: string)
  {
    imageSizeToDeleteId.value = id;

    deleteImageSizePopupRef.value!.Show()
  }

  async function ShowNewImageSizePopupAsync()
  {
    await addEditImageSizePopupRef.value!.ShowAsync(true, "00000000-0000-0000-0000-000000000000", "", 0, 0)
  }

  async function ShowEditImageSizeAsync(id: string, imageData: NewImageSize)
  {
    imageSizeToEditId.value = id;

    await addEditImageSizePopupRef.value!.ShowAsync(false, imageSizeToEditId.value, imageData.name, imageData.width, imageData.height)
  }

</script>

<template>

  <div class="admin-panel-images-sizes-container">

    <div class="table-name-header">Sizes</div>

    <div class="pseudo-link"
         @click="async () => await ShowNewImageSizePopupAsync()">
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
                class="table-update-button"
                src="/public/images/icons/update.webp"
                alt="Update image size"
                title="Update image size"
                @click="async () => await ShowEditImageSizeAsync(image.id, { name: image.name, width: image.width, height: image.height })"
            />

            <img
              class="table-close-button"
              src="/public/images/icons/delete.webp"
              alt="Delete image size"
              title="Delete image size"
              @click="ShowDeleteImageSizeConfirmation(image.id)"
            />

          </td>

        </tr>

      </tbody>

    </table>

    <div class="pseudo-link"
      @click="async () => await ShowNewImageSizePopupAsync()">
      Add new image size
    </div>

    <PopupYesNo
        title="Confirmation"
        text="Are you sure you want to delete the image size?"
        ref="deleteImageSizePopupRef"
        @yes="async () => await DeleteImageSizeAsync()" />

    <PopupAddEditImageSize
        ref="addEditImageSizePopupRef"

        @ok="async (isAdd, nIS) =>
        {
          if (isAdd)
          {
            await CreateImageSizeAsync(nIS)
          }
          else
          {
            await EditImageSizeAsync(nIS)
          }
        }"/>

  </div>

</template>
<script setup lang="ts">

  import {onMounted, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendDeleteRequest, WebClientSendGetRequest} from "../../ts/libWebClient.ts";
  import PopupYesNo from "../Shared/Popups/PopupYesNo.vue";

  const imagesSizes = ref<ImageSize[]>([])

  const deleteImageSizePopupRef = ref<InstanceType<typeof PopupYesNo>>()

  const imageSizeToDelete = ref<string>("")

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    imagesSizes.value = await GetImagesSizesList()
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

    if (!response.ok)
    {
      alert("An error happened. Try again later.")
      return
    }

    await RefreshImageSizesList()
  }

  async function RefreshImageSizesList()
  {
    imagesSizes.value = await GetImagesSizesList()
  }

  async function ShowImageSizeDeletionConfirmationAsync(id: string)
  {
    imageSizeToDelete.value = id;

    await deleteImageSizePopupRef.value!.Show()
  }

</script>

<template>

  <div class="admin-panel-images-sizes-conteiner">

    <div class="admin-panel-images-sizes-header">Sizes</div>

    <table
    class="admin-panel-images-sizes-table">

      <thead>
        <tr>
          <th class="admin-panel-images-sizes-table-header">Size name</th>
          <th class="admin-panel-images-sizes-table-header">Width</th>
          <th class="admin-panel-images-sizes-table-header">Height</th>
          <th class="admin-panel-images-sizes-table-header">Actions</th>
        </tr>
      </thead>

      <tbody>

        <tr
          v-for="(image) in imagesSizes"
          :key="image.id"
          class="admin-panel-images-sizes-table-row">

          <td class="admin-panel-images-sizes-table-cells">{{ image.name }}</td>
          <td class="admin-panel-images-sizes-table-cells">{{ image.width }}</td>
          <td class="admin-panel-images-sizes-table-cells">{{ image.height }}</td>
          <td class="admin-panel-images-sizes-table-actions-cells">

            <div>
              <img
                class="admin-panel-images-sizes-close-button"
                src="/public/images/icons/delete.webp"
                alt="Delete image size"
                @click="async () => await ShowImageSizeDeletionConfirmationAsync(image.id)"
              />

            </div>

          </td>

        </tr>

      </tbody>

    </table>

    <PopupYesNo
        title="Confirmation"
        text="Are you sure you want to delete the image size?"
        ref="deleteImageSizePopupRef"
        @yes="async () => await DeleteImageSizeAsync()" />

  </div>

</template>
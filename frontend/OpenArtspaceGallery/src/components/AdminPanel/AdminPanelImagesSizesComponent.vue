<script setup lang="ts">

  import {onMounted, ref} from "vue";
  import {DecodeImageSizeDto, DecodeImagesSizesResponse, ImageSize} from "../../ts/imagesSizes/libImagesSizes.ts";
  import {WebClientSendGetRequest} from "../../ts/libWebClient.ts";

  const imagesSizes = ref<ImageSize[]>([])

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
  }

</script>

<template>

  <div class="admin-panel-images-sizes-conteiner">

    <div class="admin-panel-images-sizes-header">
      Images sizes
    </div>

    <table>

      <thead>
        <tr>
          <th>Size name</th>
          <th>Width</th>
          <th>Height</th>
          <th>Custom</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="(image) in imagesSizes" :key="image.id">
          <td>{{ image.name }}</td>
          <td>{{ image.width }}</td>
          <td>{{ image.height }}</td>
          <td>Empty</td>
        </tr>
      </tbody>

    </table>

  </div>

</template>

<style src="public/css/adminPanel/adminPanelImagesSizes.less"/>
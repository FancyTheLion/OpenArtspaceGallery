<script setup lang="ts">
  import { PropType } from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

</script>

<template>

  <a class="album-link-full" :href="'/albums/' + props.info.id">

    <div class="album-container">

      <!-- Upper part for photos -->
      <div class="album-upper-part">
        Я - верхняя часть
      </div>

      <!-- Lower part for album name and so on -->
      <div class="album-lower-part">

        <div class="album-name">{{ props.info.name }}</div>
        <div class="album-creation-date">{{ moment(props.info?.creationTime).format("DD.MM.YYYY HH:mm:ss") }}</div>

      </div>

    </div>

  </a>

</template>

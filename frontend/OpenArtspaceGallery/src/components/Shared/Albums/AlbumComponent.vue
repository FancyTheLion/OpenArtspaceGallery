<script setup lang="ts">

  import {PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
  import moment from "moment";

  const albumCreationTime = ref<String>("")

  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

  albumCreationTime.value = moment(props.info?.creationTime).format('DD.MM.YYYY HH:mm:ss');

</script>

<template>

  <div class="album-container">

    <div>Name: {{ props.info.name }}</div>
    <div>Creation Time: {{ albumCreationTime }}</div>

  </div>

</template>

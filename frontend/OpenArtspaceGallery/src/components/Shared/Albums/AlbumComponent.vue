<script setup lang="ts">

  import {PropType, ref} from "vue";
  import {Album} from "../../../ts/Albums/libAlbums.ts";
/*  import moment = require("moment");*/


  const props = defineProps({
    info: {
      type: Object as PropType<Album>,
      required: true
    }
  })

/*
  let time = ref<Number>() // TODO: String?????

  time = moment(props.info.creationTime).format('DD.MM.YYYY HH:mm:ss')

*/

</script>

<template>

  <div class="album-container">

    <div>Name: {{ props.info.name }}</div>
    <div>Creation Time: {{ props.info.creationTime }}</div>

  </div>

</template>

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

  <div class="album-container">

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
    <div class="album-toolbar-layer">
      <div>K1</div>
      <div>K2</div>
      <div>K3</div>
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

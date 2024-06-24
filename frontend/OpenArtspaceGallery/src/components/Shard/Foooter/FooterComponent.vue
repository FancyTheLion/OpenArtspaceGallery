<script setup lang="ts">
import {onMounted, ref} from 'vue'
import {WebClientSendGetRequest} from "../../../ts/RequestToBackend.ts";

const isLoading = ref(true)
const backendVersion = ref(null)

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
  backendVersion.value = await GetBackendVersion()

  isLoading.value = false
}

async function GetBackendVersion()
{
  return (await (await WebClientSendGetRequest("/api/GetBackendVersion")).json())
      .backendVersion
      .backendVersion
}

</script>

<template>

  <LoadingSymbol v-if="isLoading" />

  <div v-if="!isLoading">

    <div class="test-background-color-n-super-mega-gigachad">
      {{ backendVersion }}
    </div>

  </div>

</template>

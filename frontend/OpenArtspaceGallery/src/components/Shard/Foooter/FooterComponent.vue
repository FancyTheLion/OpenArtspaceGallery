<script setup lang="ts">
import {onMounted, ref} from 'vue'
import {WebClientSendGetRequest} from "../../../ts/RequestToBackend.ts";

const isLoading = ref(true)
const backendVersion = ref(null)
const sourcesLink = ref<string>("")

onMounted(async () =>
{
  await OnLoad();
})

async function OnLoad()
{
  backendVersion.value = await GetBackendVersion()

  sourcesLink.value = await GetSourcesLink()

  isLoading.value = false
}

async function GetBackendVersion()
{
  return (await (await WebClientSendGetRequest("/api/GetBackendVersion")).json())
      .backendVersion
      .backendVersion
}

async function GetSourcesLink()
{
  return (await (await WebClientSendGetRequest("/api/GetSourcesLink")).json())
      .sourcesLink
      .sourcesLink
}

</script>

<template>

  <LoadingSymbol v-if="isLoading" />

  <div v-if="!isLoading">

    <div class="flex-container-info">

      <div class="info-version">

        <div>
          <a :href="sourcesLink" title="Лицензировано под AGPLv3 или более поздней версией">Исходный код</a>
        </div>

        <div class="license">
          <img src="/Images/AGPLv3_Logo.webp" alt="AGPLv3 logo" />
        </div>

        <div>
          Бэкенд: {{ backendVersion }}
        </div>

        <div>
          Фронтенд: Version 0.0.1
          
        </div>

      </div>

    </div>

  </div>

</template>

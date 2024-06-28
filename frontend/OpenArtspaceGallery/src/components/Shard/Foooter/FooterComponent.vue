<script setup lang="ts">
  import {onMounted, ref} from 'vue'
  import {WebClientSendGetRequest} from "../../../ts/RequestToBackend.ts";

  const isLoading = ref<boolean>(true)
  const backendVersion = ref<string>("")
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
    return (await (await WebClientSendGetRequest("/SiteInfo/GetBackendVersion")).json())
        .backendVersion
        .backendVersion
  }

  async function GetSourcesLink()
  {
    return (await (await WebClientSendGetRequest("/SiteInfo/GetSourcesLink")).json())
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
          <a :href="sourcesLink" title="Лицензировано под AGPLv3 или более поздней версией">Исходные коды</a>
        </div>

        <div class="license">
          <img src="/images/AGPLv3_Logo.webp" alt="AGPLv3 logo" />
        </div>

        <div>
          Версия бэкенда: {{ backendVersion }}
        </div>

        <div>
          Версия фронтенда: 0.0.1

        </div>

      </div>

    </div>

  </div>

</template>

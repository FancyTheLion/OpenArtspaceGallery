<script setup lang="ts">
  import {onMounted, ref} from 'vue'
  import {WebClientSendGetRequest} from "../../../ts/libWebClient.ts";
  import LoadingSymbol from "../LoadyngSymbol/LoadingSymbolComponent.vue";

  const isLoading = ref<boolean>(true)
  const backendVersion = ref<string>("")
  const sourcesLink = ref<string>("")

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    backendVersion.value = await GetBackendVersionAsync()
    sourcesLink.value = await GetSourcesLinkAsync()

    isLoading.value = false
  }

  async function GetBackendVersionAsync()
  {
    return (await (await WebClientSendGetRequest("/SiteInfo/GetBackendVersion")).json())
        .backendVersion
        .backendVersion
  }

  async function GetSourcesLinkAsync()
  {
    return (await (await WebClientSendGetRequest("/SiteInfo/GetSourcesLink")).json())
        .sourcesLink
        .sourcesLink
  }

</script>

<template>

  <div class="footer-container">
    <LoadingSymbol v-if="isLoading" />

    <div v-if="!isLoading">

      <div class="footer-version-container">

        <div>
          <a class="link-not-important" :href="sourcesLink" title="Application sources link">Sources link</a>
        </div>

        <img src="/images/AGPLv3_Logo.webp" alt="AGPLv3 logo" />

        <div class="footer-version-info">
          Backend version: {{ backendVersion }}
        </div>

        <div class="footer-version-info">
          Frontend version: 0.0.1
        </div>

      </div>

    </div>

  </div>

</template>

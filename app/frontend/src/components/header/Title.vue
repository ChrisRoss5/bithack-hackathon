<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <RouterLink to="/" class="flex items-center gap-3 font-bold" id="title">
    <img src="/favicon.png" alt="" class="h-8 drop-shadow-lg lg:h-14" />
    <div>{{ t("appName") }}</div>
  </RouterLink>
</template>

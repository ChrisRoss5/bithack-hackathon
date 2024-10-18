<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <RouterLink to="/kontakt" class="underline underline-offset-4">{{
    t("header.contact")
  }}</RouterLink>
  <RouterLink to="/o-nama" class="underline underline-offset-4">
    {{ t("header.aboutUs") }}</RouterLink
  >
</template>

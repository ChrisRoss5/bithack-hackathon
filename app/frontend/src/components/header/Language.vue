<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const changeLocale = (locale: string) => {
  localStorage.setItem("locale", locale);
  document.title = t("appName");
};

document.title = t("appName");
</script>

<template>
  <select
    class="dsy-select hover:bg-base-content/20 bg-transparent text-xl *:text-base-content"
    v-model="$i18n.locale"
    @change="changeLocale(($event!.target as HTMLSelectElement).value)"
  >
    <option value="hr" class="bg-base-100">HR</option>
    <option value="en" class="bg-base-100">EN</option>
  </select>
</template>

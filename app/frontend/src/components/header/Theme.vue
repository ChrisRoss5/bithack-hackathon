<script setup lang="ts">
import { ref } from "vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const theme = ref(
  localStorage.getItem("theme") ||
    (window.matchMedia("(prefers-color-scheme: dark)").matches
      ? "dim"
      : "light"),
);

const changeTheme = () => {
  theme.value = theme.value == "dim" ? "light" : "dim";
  localStorage.setItem("theme", theme.value);
  document.documentElement.dataset.theme = theme.value;
};

document.documentElement.dataset.theme = theme.value;
</script>

<template>
  <button
    class="dsy-btn dsy-btn-ghost"
    @click="changeTheme"
    :title="theme == 'dim' ? t('header.darkMode') : t('header.lightMode')"
  >
    <span class="material-symbols-outlined align-middle" v-if="theme == 'dim'">
      dark_mode
    </span>
    <span class="material-symbols-outlined align-middle" v-else>
      light_mode
    </span>
  </button>
</template>

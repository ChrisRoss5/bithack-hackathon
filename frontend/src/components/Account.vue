<script setup lang="ts">
import { useAuthStore } from '@/stores/auth';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const authStore = useAuthStore();
</script>

<template>
  <div>
    <div class="font-bold"> {{ t("header.userAccount") }} </div>
    <div> {{ authStore.userInfo?.displayName }} </div>
    <div> {{ authStore.userInfo?.email }} </div>
    <div> {{ authStore.userInfo?.role }} </div>
  </div>
</template>

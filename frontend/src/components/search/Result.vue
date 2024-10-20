<script setup lang="ts">
import { useI18n } from "vue-i18n";

defineProps<{
  home: Home;
}>();

const emits = defineEmits<{
  (e: "reserve", home: Home): void;
}>();

const { t } = useI18n();
</script>

<template>
  <div :id="home.id">
    <figure>
      <img v-if="home.pictureUrl" :src="home.pictureUrl" alt="" />
      <img v-else src="@/assets/images/no-image-en.svg" alt="" />
    </figure>
    <div class="dsy-card-body">
      <h2 class="dsy-card-title flex-wrap">
        {{ home.name }}
        <div
          class="dsy-badge h-auto px-2 py-1"
          :class="{
            'dsy-badge-success text-base-100': !home.contracts.length,
            'dsy-badge-warning': home.contracts.length,
          }"
        >
          {{ home.contracts.length ? t("home.partlyFree") : t("home.free") }}
        </div>
      </h2>
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined"> location_on </span
        >{{ home.address }}
      </div>
      <div class="dsy-card-actions mt-3 justify-end">
        <div class="dsy-badge dsy-badge-outline h-auto px-2 py-1">
          <span class="material-symbols-outlined pr-1">
            responsive_layout
          </span>
          {{ home.area }} m<sup>2</sup>
        </div>
        <div class="dsy-badge dsy-badge-outline h-auto px-2 py-1">
          <span class="material-symbols-outlined pr-1"> group </span>
          {{ home.capacity }}
        </div>
        <div class="dsy-badge dsy-badge-outline h-auto px-2 py-1">
          <span class="material-symbols-outlined pr-1"> euro </span>
          {{ home.leaseAmount }}€ / {{ t("home.hour") }}
        </div>
      </div>
      <div class="dsy-divider"></div>
      <div class="dsy-btn dsy-btn-outline" @click="emits('reserve', home)">
        {{ t("home.reserve") }}
      </div>
    </div>
  </div>
</template>

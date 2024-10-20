<script setup lang="ts">
import { useHomesStore } from "@/stores/homes";
import { formatDate } from "@/utils/format";
import { nextTick, onBeforeUnmount, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import Reservation from "./Reservation.vue";
import Results from "./search/Results.vue";
import Search from "./search/Search.vue";

const { t } = useI18n();
const homesStore = useHomesStore();

const resultsComponent = ref<InstanceType<typeof Results> | null>(null);
const landingImg = ref<HTMLElement | null>(null);
const landingImgHeight = ref(0);
const homes = ref<Home[]>([]);
const selectedHome = ref<Home | null>(null);
const resultsScrolledIn = ref(false);

onMounted(() => {
  if (!landingImg.value) return;
  const resizeObserver = new ResizeObserver((entries) => {
    for (let entry of entries) {
      if (entry.target === landingImg.value) {
        landingImgHeight.value = entry.contentRect.height * 1.5 - 30;
      }
    }
  });
  resizeObserver.observe(landingImg.value!);
  onBeforeUnmount(() => {
    resizeObserver.unobserve(landingImg.value!);
    resizeObserver.disconnect();
  });
});

const handleSearch = async (from?: Date, to?: Date) => {
  console.log("SEARCHING");

  homes.value = await homesStore.getHomes(
    formatDate(from),
    formatDate(to),
  );

  console.log(homes.value);
  resultsComponent.value?.$el.scrollIntoView({
    behavior: "smooth",
    block: "start",
  });
};

const handleReservationCancel = async () => {
  const { id } = selectedHome.value!;
  selectedHome.value = null;
  await nextTick();
  document.querySelector(`[id="${id}"]`)?.scrollIntoView({
    behavior: "smooth",
    block: "start",
  });
};
</script>

<template>
  <div class="!p-0">
    <div
      class="relative lg:!h-auto"
      :style="{ height: landingImgHeight + 'px' }"
      :class="{ 'aspect-video': !landingImgHeight }"
    >
      <img
        id="landing-img"
        src="/pictures/landing.webp?url"
        alt=""
        class="landing-img opacity-75 shadow-xl 2xl:rounded-3xl"
        ref="landingImg"
      />
      <div
        class="absolute top-16 w-full py-6 text-center text-white backdrop-blur-sm [text-shadow:0_1px_2px_black] lg:top-24 lg:bg-transparent lg:px-8 lg:text-left lg:backdrop-blur-none lg:[text-shadow:0_0_3px_black]"
      >
        <div class="pb-5 text-4xl font-extrabold lg:text-6xl">
          {{ t("home.title") }}
        </div>
        <div class="text-lg italic">{{ t("home.subtitle") }}</div>
      </div>
    </div>
    <Search
      :isCollapsed="
        (resultsScrolledIn || !!selectedHome) && $breakpoints.lgAndUp
      "
      @change="handleSearch"
    />
    <Reservation
      v-if="selectedHome"
      :home="selectedHome"
      @cancel="handleReservationCancel"
    />
    <Results
      v-else
      :homes="homes"
      ref="resultsComponent"
      @scrolled-in="(scrolledIn) => (resultsScrolledIn = scrolledIn)"
      @reserve="(home) => (selectedHome = home)"
    />
  </div>
</template>

<style scoped>
.landing-img {
  transform: scale(1.5) translateY(30%);
  transform-origin: bottom;
  transition: transform 0.5s;
}
@screen lg {
  .landing-img {
    transform: none;
  }
}
</style>

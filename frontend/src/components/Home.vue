<script setup lang="ts">
import { useHomesStore } from "@/stores/homes";
import { formatDate } from "@/utils/format";
import { onBeforeUnmount, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import Results from "./Results.vue";
import Search from "./Search.vue";

const { t } = useI18n();

const homesStore = useHomesStore();

const resultsComponent = ref<InstanceType<typeof Results> | null>(null);
const landingImg = ref<HTMLElement | null>(null);
const landingImgHeight = ref(0);
const homes = ref<Home[]>([]);

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

const handleSearch = async (startDate?: Date, endDate?: Date) => {
  homes.value = await homesStore.getHomes(
    formatDate(startDate),
    formatDate(endDate),
  );
  console.log(homes.value);
  resultsComponent.value?.$el.scrollIntoView({
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
        class="absolute top-16 w-full bg-white/50 px-8 py-6 text-center text-white backdrop-blur-sm [text-shadow:0_0_3px_black] lg:top-24 lg:bg-transparent lg:text-left lg:backdrop-blur-none"
      >
        <div class="pb-5 text-4xl font-extrabold lg:text-6xl">
          {{ t("home.title") }}
        </div>
        <div class="text-lg italic">{{ t("home.subtitle") }}</div>
      </div>
    </div>
    <Search :isCollapsed="resultsScrolledIn" @change="handleSearch" />
    <Results
      :homes="homes"
      ref="resultsComponent"
      @scrolled-in="(scrolledIn) => (resultsScrolledIn = scrolledIn)"
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

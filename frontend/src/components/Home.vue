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
  homes.value = await homesStore.getHomes(
    formatDate(from),
    formatDate(to),
  );
  /*   homes.value = [
    {
      id: "home1",
      name: "Sunny Villa",
      rentPrice: 1500,
      address: "123 Sunshine Blvd, Miami, FL",
      leaseAmount: 4500,
      bailAmount: 1500,
      homeBills: 200,
      vat: 10,
      area: 1500,
      cutleryPrice: 150,
      capacity: 4,
      createdAt: new Date(2023, 0, 15, 10, 30),
      contracts: [
        {
          userId: "user1",
          contractRanges: [
            {
              id: "range1",
              from: new Date(2024, 9, 28, 9),
              to: new Date(2023, 9, 28, 16),
            },
            {
              id: "range2",
              from: new Date(2023, 8, 1, 9),
              to: new Date(2023, 8, 1, 17),
            },
          ],
        },
        {
          userId: "user2",
          contractRanges: [
            {
              id: "range3",
              from: new Date(2024, 2, 1, 10),
              to: new Date(2024, 2, 1, 18),
            },
          ],
        },
      ],
    },
    {
      id: "home2",
      name: "Cozy Cottage",
      rentPrice: 1200,
      address: "456 Maple Street, Portland, OR",
      leaseAmount: 3600,
      bailAmount: 1200,
      homeBills: 180,
      vat: 8,
      area: 1200,
      capacity: 3,
      createdAt: new Date(2022, 8, 12, 9),
      contracts: [
        {
          userId: "user3",
          contractRanges: [
            {
              id: "range4",
              from: new Date(2022, 9, 1, 7),
              to: new Date(2022, 9, 1, 15),
            },
            {
              id: "range5",
              from: new Date(2023, 4, 1, 10),
              to: new Date(2023, 4, 1, 18),
            },
          ],
        },
        {
          userId: "user4",
          contractRanges: [
            {
              id: "range6",
              from: new Date(2024, 0, 1, 9),
              to: new Date(2024, 0, 1, 17),
            },
            {
              id: "range7",
              from: new Date(2024, 7, 1, 8),
              to: new Date(2024, 7, 1, 16),
            },
          ],
        },
      ],
    },
    {
      id: "home3",
      name: "Urban Loft",
      rentPrice: 2000,
      address: "789 Downtown Ave, New York, NY",
      leaseAmount: 6000,
      bailAmount: 2000,
      homeBills: 250,
      vat: 12,
      area: 1800,
      capacity: 5,
      createdAt: new Date(2021, 4, 10, 8, 45),
      contracts: [
        {
          userId: "user5",
          contractRanges: [
            {
              id: "range8",
              from: new Date(2022, 0, 1, 6),
              to: new Date(2022, 0, 1, 14),
            },
          ],
        },
        {
          userId: "user6",
          contractRanges: [
            {
              id: "range9",
              from: new Date(2023, 0, 1, 8),
              to: new Date(2023, 0, 1, 16),
            },
            {
              id: "range10",
              from: new Date(2023, 7, 1, 9),
              to: new Date(2023, 7, 1, 17),
            },
          ],
        },
      ],
    },
    {
      id: "home4",
      name: "Lakeside House",
      rentPrice: 1700,
      address: "101 Lakeview Dr, Chicago, IL",
      leaseAmount: 5100,
      bailAmount: 1700,
      homeBills: 220,
      vat: 9,
      area: 1600,
      cutleryPrice: 120,
      capacity: 4,
      createdAt: new Date(2023, 2, 20, 11, 15),
      contracts: [
        {
          userId: "user7",
          contractRanges: [
            {
              id: "range11",
              from: new Date(2023, 3, 1, 7),
              to: new Date(2023, 3, 1, 15),
            },
            {
              id: "range12",
              from: new Date(2023, 10, 1, 8),
              to: new Date(2023, 10, 1, 16),
            },
          ],
        },
        {
          userId: "user8",
          contractRanges: [
            {
              id: "range13",
              from: new Date(2024, 5, 1, 9),
              to: new Date(2024, 5, 1, 17),
            },
          ],
        },
      ],
    },
    {
      id: "home5",
      name: "Mountain Cabin",
      rentPrice: 1400,
      address: "202 Peak Trail, Denver, CO",
      leaseAmount: 4200,
      bailAmount: 1400,
      homeBills: 150,
      vat: 7,
      area: 1400,
      capacity: 3,
      createdAt: new Date(2023, 6, 5, 7, 30),
      contracts: [
        {
          userId: "user9",
          contractRanges: [
            {
              id: "range14",
              from: new Date(2023, 7, 1, 8),
              to: new Date(2023, 7, 1, 16),
            },
            {
              id: "range15",
              from: new Date(2024, 2, 1, 7),
              to: new Date(2024, 2, 1, 15),
            },
          ],
        },
        {
          userId: "user10",
          contractRanges: [
            {
              id: "range16",
              from: new Date(2024, 9, 1, 6),
              to: new Date(2024, 9, 1, 14),
            },
          ],
        },
      ],
    },
  ]; */

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

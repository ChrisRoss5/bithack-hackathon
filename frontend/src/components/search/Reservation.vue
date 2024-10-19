<!-- https://github.com/vueform/slider?tab=readme-ov-file#installation -->

<script setup lang="ts">
import { useSearchStore } from "@/stores/search";
import Slider from "@vueform/slider";
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";

const props = defineProps<{ home: Home }>();
const emits = defineEmits<{ (e: "cancel"): void }>();

const { t } = useI18n();
const searchStore = useSearchStore();

const reservationDiv = ref<HTMLElement | null>(null);
const submitReservation = () => {
  console.log("SUBMITTING RESERVATION");
};

onMounted(() => {
  reservationDiv!.value?.scrollIntoView({ behavior: "smooth" });
});

const date1 = new Date(searchStore.dateFrom);
const date2 = new Date(searchStore.dateTo);
const days = ref<
  { date: Date; dateName: string; slider: any; occupiedSliders: any[] }[]
>([]);

const sliderDefaultParams = {
  value: [0, 24],
  max: 24,
  min: 0,
  step: 1,
  lazy: false,
  tooltips: true,
  format: {
    suffix: ":00",
    decimals: 0,
  },
  options: {
    margin: 8,
    behaviour: "drag",
  },
};

for (let d = date1; d <= date2; d.setDate(d.getDate() + 1)) {
  const occupiedRanges = props.home.contracts.flatMap((contract) => {
    const ranges = contract.contractRanges;
    if (ranges) {
      return ranges.filter((range) => {
        return range.from.toLocaleDateString() == d.toLocaleDateString();
      });
    }
    return [];
  });
  days.value.push({
    date: new Date(d),
    dateName: d.toLocaleDateString("hr-HR", { weekday: "long" }),
    slider: structuredClone(sliderDefaultParams),
    occupiedSliders: occupiedRanges.map((range) => {
      return {
        ...structuredClone(sliderDefaultParams),
        value: [range.from.getHours(), range.to.getHours()],
        options: {
          disabled: true,
        },
      };
    }),
  });
}

const applySliderToAll = ref(true);
const areDaysValid = computed(() => {
  for (const day of days.value) {
    const occupiedRanges = day.occupiedSliders.map((slider) => slider.value);
    const chosenRange = day.slider.value;
    for (const range of occupiedRanges) {
      // overlapping: x_start <= y_end && y_start <= x_end
      if (chosenRange[0] < range[1] && range[0] < chosenRange[1]) {
        return false;
      }
    }
  }
  return true;
});

const handleSliderUpdate = (e: [number, number]) => {
  console.log(e);
  if (applySliderToAll.value) {
    days.value.forEach((day) => {
      day.slider.value = e;
    });
  }
};
</script>

<template>
  <div ref="reservationDiv" class="mt-6">
    <figure>
      <img src="@/assets/images/no-image-en.svg" alt="" />
    </figure>
    <div class="dsy-card-body">
      <h2 class="dsy-card-title mb-4 flex-wrap text-3xl">
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
      <div class="dsy-collapse dsy-collapse-arrow bg-base-200">
        <input type="checkbox" />
        <div class="dsy-collapse-title text-xl font-medium">
          <span class="material-symbols-outlined align-middle">
            location_on
          </span>
          Lokacija:
          {{ home.address }}
        </div>
        <div class="dsy-collapse-content">
          <iframe
            title="map"
            class="h-96 w-full"
            style="border: 0"
            loading="lazy"
            allowfullscreen
            referrerpolicy="no-referrer-when-downgrade"
            :src="`https://www.google.com/maps/embed/v1/place?key=${'AIzaSyA5_RRB5bfL6UadURJHQ65qGs8BL_xdEJU'}&q=${home.address}&language=${$i18n.locale}`"
            ><!-- todo move key + language -->
          </iframe>
        </div>
      </div>
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined pr-1"> group </span>
        Kapacitet: {{ home.capacity }}
      </div>
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined pr-1"> euro </span>
        Cijena zakupa: {{ home.leaseAmount }} / {{ t("home.hour") }}
      </div>
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined pr-1"> euro </span>
        Cijena jamčevine: {{ home.bailAmount }}
      </div>
      <div class="flex items-center gap-3">
        <span class="material-symbols-outlined"> restaurant </span>Cijena zakupa
        pribora: {{ home.cutleryPrice || "—" }}
      </div>
      <div class="dsy-divider"></div>
      <label class="flex-center mb-10 gap-3">
        Primijeni raspon na sve dane
        <input
          type="checkbox"
          class="dsy-toggle dsy-toggle-primary"
          v-model="applySliderToAll"
        />
      </label>
      <div class="mr-6 grid gap-10 lg:grid-cols-[auto_1fr]">
        <template v-for="day in days" :key="+day.date">
          <div class="-translate-y-1 text-center capitalize lg:text-right">
            {{ day.dateName }} {{ day.date.toLocaleDateString($i18n.locale) }}
          </div>
          <div class="relative">
            <div
              v-for="(occupiedSlider, i) in day.occupiedSliders"
              :key="i"
              class="pointer-events-none absolute inset-0 z-10"
            >
              <Slider
                class="slider slider-occupied"
                v-model="occupiedSlider.value"
                v-bind="occupiedSlider"
              />
            </div>
            <Slider
              class="slider"
              v-model="day.slider.value"
              v-bind="day.slider"
              @set="handleSliderUpdate"
            />
          </div>
        </template>
      </div>
      <div
        role="alert"
        class="dsy-alert dsy-alert-warning mx-auto mt-3 w-fit"
        v-show="!areDaysValid"
      >
        <span class="material-symbols-outlined"> warning </span>
        Molimo, ispravite mjesta na kojima se odabrani raspon preklapa sa
        zauzetim.
      </div>
      <div class="dsy-divider"></div>

      <div class="dsy-divider"></div>
      <div class="flex">
        <div class="dsy dsy-btn flex-1" @click="emits('cancel')">
          {{ t("home.cancel") }}
        </div>
        <div
          class="dsy-btn dsy-btn-outline flex-1"
          :class="{ 'dsy-btn-disabled': !areDaysValid }"
          @click="submitReservation"
        >
          {{ t("home.reserve") }}
        </div>
      </div>
    </div>
  </div>
</template>

<style src="@vueform/slider/themes/default.css"></style>
<style>
.slider {
  --slider-height: 1rem;
  --slider-handle-width: 1.5rem;
  --slider-handle-height: 1.5rem;
}
.slider-occupied {
  --slider-height: 1rem;
  --slider-handle-width: 0rem;
  --slider-bg: transparent;
  --slider-handle-bg: transparent;
  --slider-connect-bg: #ff5858;
  --slider-tooltip-bg: #ff5858;
}
</style>

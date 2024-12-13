<!-- https://github.com/vueform/slider?tab=readme-ov-file#installation -->

<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";
import { useContractsStore } from "@/stores/contracts";
import { useSearchStore } from "@/stores/search";
import Slider from "@vueform/slider";
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRouter } from "vue-router";
import ContractPDF from "./ContractPDF.vue";

const props = defineProps<{ home: Home; contract?: Contract }>();
const emits = defineEmits<{ (e: "cancel"): void }>();

const { t, locale } = useI18n();
const searchStore = useSearchStore();
const authStore = useAuthStore();
const contractsStore = useContractsStore();
const router = useRouter();

const reservationDiv = ref<HTMLElement | null>(null);

onMounted(async () => {
  reservationDiv!.value?.scrollIntoView({ behavior: "smooth" });
});

const dateFrom = new Date(searchStore.dateFrom);
const dateTo = new Date(searchStore.dateTo);
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

if (!props.contract) loadDays(false);

async function loadDays(fetch = true) {
  if (days.value.length) return;
  const contractRanges = fetch
    ? await contractsStore.getContractRanges(props.contract!.id)
    : null;
  for (let d = dateFrom; d <= dateTo; d.setDate(d.getDate() + 1)) {
    const occupiedRanges: ContractRange[] = contractRanges
      ? contractRanges.filter((range) => {
          return range.from.toLocaleDateString() == d.toLocaleDateString();
        })
      : props.home.contracts.flatMap((contract) => {
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
      dateName: d.toLocaleDateString(locale.value, { weekday: "long" }), // todo make dynamic
      slider: structuredClone(sliderDefaultParams),
      occupiedSliders: occupiedRanges.map((range) => {
        return {
          ...structuredClone(sliderDefaultParams),
          value: [range.from.getHours(), range.to.getHours()],
        };
      }),
    });
  }
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

const leasePurpose = ref(props.contract ? props.contract.leasePurpose : "");
const isContractFree = ref(props.contract ? props.contract.isFree : false);
const usingCutlery = ref(props.contract ? props.contract.usingCutlery : false);
const contractForm = computed<ContractForm>(() => ({
  contractRequest: {
    userId: authStore.userInfo!.id!,
    communityHomeId: props.home.id,
    leasePurpose: leasePurpose.value,
    isfree: isContractFree.value,
    usingCutlery: usingCutlery.value,
  },
  contractRanges: days.value.map((day) => {
    const from = new Date(day.date);
    const to = new Date(day.date);
    from.setHours(day.slider.value[0]);
    to.setHours(day.slider.value[1]);
    return { from, to };
  }),
}));
const totalHours = computed(() => {
  return days.value.reduce((acc, day) => {
    return acc + day.slider.value[1] - day.slider.value[0];
  }, 0);
});
const totalLeasePrice = computed(() => {
  return isContractFree
    ? 0
    : totalHours.value * props.home.leaseAmount + props.home.bailAmount;
});
const totalPrice = computed(() => {
  return (
    (totalLeasePrice.value + props.home.homeBills) * totalHours.value +
    props.home.bailAmount +
    (usingCutlery.value ? props.home.cutleryPrice! : 0)
  );
});

const submitReservation = async () => {
  await contractsStore.submitContract(contractForm.value);
  router.push("/ugovori");
};
const handleSliderUpdate = (e: [number, number]) => {
  if (applySliderToAll.value)
    days.value.forEach((day) => (day.slider.value = e));
};
const print = () => {
  window.print();
};
const confirmPayment = async () => {
  console.log("CONFIRMING PAYMENT");
  console.log(props.contract);
};
</script>

<template>
  <div
    ref="reservationDiv"
    class="dsy-card"
    :class="{ 'mt-6 md:mt-0': !contract }"
  >
    <figure v-if="!contract">
      <img
        v-if="home.pictureUrl"
        :src="home.pictureUrl"
        class="w-full"
        alt=""
      />
      <img v-else src="@/assets/images/no-image-en.svg" alt="" />
    </figure>
    <div class="dsy-card-body">
      <h2 v-if="!contract" class="dsy-card-title mb-4 flex-wrap text-3xl">
        {{ home.name }}
        <div
          class="dsy-badge ml-3 h-auto px-2 py-1 text-2xl"
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
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> group </span>
        Površina: {{ home.area }}
      </div>
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> group </span>
        Kapacitet: {{ home.capacity }}
      </div>
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> euro </span>
        Cijena najma: {{ home.leaseAmount }}€ / {{ t("home.hour") }}
      </div>
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> euro </span>
        Cijena režija: {{ home.homeBills }}€ / dan
      </div>
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> euro </span>
        Cijena jamčevine: {{ home.bailAmount }}€
      </div>
      <div class="flex items-center gap-3 pl-4">
        <span class="material-symbols-outlined pr-1"> restaurant </span>
        Cijena najma pribora:
        {{ home.cutleryPrice ? home.cutleryPrice + "€" : "—" }}
      </div>
      <div class="dsy-collapse dsy-collapse-arrow bg-base-200">
        <input type="checkbox" @change="() => loadDays()" />
        <div class="dsy-collapse-title text-xl font-medium">
          <span class="material-symbols-outlined align-middle">
            arrow_range
          </span>
          Raspon najma prema danima
        </div>
        <div class="dsy-collapse-content">
          <label v-if="!contract" class="flex-center mb-12 gap-3">
            Primijeni raspon na sve dane
            <input
              type="checkbox"
              class="dsy-toggle dsy-toggle-primary"
              v-model="applySliderToAll"
            />
          </label>
          <div
            class="mx-6 grid gap-10 lg:mr-6 lg:grid-cols-[auto_1fr]"
            :class="{ 'mt-12 cursor-not-allowed': !!contract }"
          >
            <template v-for="day in days" :key="+day.date">
              <div class="-translate-y-1 text-center capitalize lg:text-right">
                {{ day.dateName }}
                {{ day.date.toLocaleDateString($i18n.locale) }}
              </div>
              <div
                class="relative"
                :class="{
                  'pointer-events-none': !!contract,
                }"
              >
                <div
                  v-for="(occupiedSlider, i) in day.occupiedSliders"
                  :key="i"
                  class="pointer-events-none absolute inset-0 z-[2]"
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
        </div>
      </div>
      <div
        role="alert"
        class="dsy-alert dsy-alert-warning mx-auto mt-3 w-fit"
        v-show="!areDaysValid"
        v-if="!contract"
      >
        <span class="material-symbols-outlined"> warning </span>
        Molimo, ispravite mjesta na kojima se odabrani raspon preklapa sa
        zauzetim.
      </div>
      <div class="mt-3 flex flex-wrap gap-3">
        <input
          type="checkbox"
          class="dsy-toggle dsy-toggle-primary"
          v-model="isContractFree"
          :disabled="!!props.contract"
        />
        {{ t("freeCondition") }}
      </div>
      <div class="flex flex-wrap gap-3" v-if="home.cutleryPrice">
        <input
          type="checkbox"
          class="dsy-toggle dsy-toggle-primary"
          v-model="usingCutlery"
          :disabled="!!props.contract"
        />
        Najam pribora ({{ home.cutleryPrice }}€)
      </div>
      <div class="flex flex-wrap gap-3 pt-3">
        Razlog najma:
        <textarea
          class="dsy-textarea dsy-textarea-bordered w-full"
          :placeholder="'Unesi rečenicu ili dvije...'"
          v-model="leasePurpose"
          :disabled="!!props.contract"
        ></textarea>
      </div>
      <div class="dsy-divider"></div>
      <div class="text-right text-xl font-bold">
        Ukupno sati: {{ totalHours }}h
        <br />
        Ukupna cijena najma (({{ home.leaseAmount }}€ + {{ home.homeBills }}€) *
        {{ totalHours }}h + {{ home.bailAmount }}€{{
          usingCutlery ? " + " + home.cutleryPrice + "€" : ""
        }}):
        <span :class="{ 'line-through': isContractFree }"
          >{{ totalPrice }}€</span
        ><span v-if="isContractFree"> (BESPLATNO) </span>
      </div>
      <div class="dsy-divider" v-if="!contract"></div>
      <div class="flex" v-if="!contract">
        <div class="dsy dsy-btn flex-1" @click="emits('cancel')">
          {{ t("home.cancel") }}
        </div>
        <div
          class="dsy-btn dsy-btn-outline flex-1"
          :class="{ 'dsy-btn-disabled': !areDaysValid || !leasePurpose }"
          @click="submitReservation"
        >
          {{ t("home.reserve") }}
        </div>
      </div>
      <template v-if="!!contract">
        <div class="dsy-divider"></div>
        <div v-if="contract.status == 1">
          <div class="dsy-collapse dsy-collapse-arrow bg-base-200">
            <input type="checkbox" @change="() => loadDays()" />
            <div class="dsy-collapse-title text-xl font-medium">
              <span class="material-symbols-outlined align-middle">
                picture_as_pdf
              </span>
              PDF dokumenti
            </div>
            <div class="dsy-collapse-content">
              <ContractPDF :contract="contract" />
              <div class="flex flex-wrap">
                <div
                  class="dsy-btn dsy-btn-outline dsy-btn-primary flex-1"
                  @click="print"
                >
                  Print
                </div>
                <div
                  class="dsy-btn dsy-btn-primary flex-1"
                  @click="confirmPayment"
                >
                  Potvrdi plaćanje
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

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

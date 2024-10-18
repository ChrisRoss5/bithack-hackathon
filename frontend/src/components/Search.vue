<script setup lang="ts">
import { formatDate } from "@/utils/format";
import { ref } from "vue";
import { useI18n } from "vue-i18n";

defineProps<{
  isCollapsed?: boolean;
}>();
const emits = defineEmits<{
  (e: "change", startDate?: Date, endDate?: Date): void;
}>();

const { t } = useI18n();

const endDateInput = ref<HTMLInputElement | null>(null);

const startDate = ref("");
const endDate = ref("");

const handleSearch = () => {
  const _startDate = startDate.value ? new Date(startDate.value) : undefined;
  const _endDate = endDate.value ? new Date(endDate.value) : undefined;
  emits("change", _startDate, _endDate);
};

const handleStartDateChange = () => {
  if (!endDate.value) {
    endDateInput.value?.showPicker();
  }
};

const getFirstAvailableDate = () => {
  const days = 8;
  const date = new Date();
  date.setDate(date.getDate() + days);
  return formatDate(date);
};
</script>

<template>
  <div
    class="z-10 mx-auto w-full bg-base-100 px-6 py-3 shadow-xl lg:sticky lg:bottom-40 lg:top-52 lg:w-1/2 lg:-translate-x-1 lg:-translate-y-[7rem] lg:rounded-3xl lg:opacity-95"
    :class="{
      'flex !w-fit justify-center': isCollapsed,
    }"
  >
    <div
      class="relative"
      :class="{
        'flex flex-wrap items-center gap-3': isCollapsed,
        'pb-8': !isCollapsed,
      }"
    >
      <!-- <div class="uppercase">Odaberi željeni period</div> -->
      <div class="relative flex flex-wrap gap-3">
        <div class="flex-1 rounded-md px-8 py-3 shadow-md">
          <label class="flex items-center">
            <div>{{ t("home.searchBox.from") }}</div>
            <input
              type="date"
              class="dsy-input ml-2 w-full"
              :min="getFirstAvailableDate()"
              v-model="startDate"
              @change="handleStartDateChange"
            />
          </label>
          <div class="text-nowrap text-neutral-500" v-show="!isCollapsed">
            {{ t("home.searchBox.fromDescription") }}
          </div>
        </div>
        <div class="flex-1 rounded-md px-8 py-3 shadow-md">
          <label class="flex items-center">
            <div>{{ t("home.searchBox.to") }}</div>
            <input
              type="date"
              class="dsy-input ml-2 w-full"
              :min="startDate"
              v-model="endDate"
              ref="endDateInput"
            />
          </label>
          <div class="text-nowrap text-neutral-500" v-show="!isCollapsed">
            {{ t("home.searchBox.toDescription") }}
          </div>
        </div>
        <div
          class="absolute-center flex-center rounded-full bg-base-200 p-3 shadow-lg"
        >
          <span class="material-symbols-outlined"> swap_horiz </span>
        </div>
      </div>
      <!-- <button class="flex items-center py-2" @click="handleSearch">
        {{ t("home.searchBox.showAll") }}
        <span class="material-symbols-outlined"> arrow_drop_down </span>
      </button> -->
      <div
        class="text-xl uppercase"
        :class="{
          'absolute bottom-0 right-0 w-full translate-y-1/2 lg:right-6 lg:w-1/3':
            !isCollapsed,
          'h-full': isCollapsed,
        }"
      >
        <button
          class="bg-skyBlue hover:bg-skyBlue/50 dsy-btn h-full w-full text-xl uppercase"
          @click="handleSearch"
        >
          {{ t("home.searchBox.search") }}
          <span class="material-symbols-outlined"> search </span>
        </button>
      </div>
    </div>
  </div>
</template>

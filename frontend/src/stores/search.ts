import { defineStore } from "pinia";
import { ref } from "vue";

export const useSearchStore = defineStore("search", () => {
  const dateFrom = ref<string>("11/20/2024");
  const dateTo = ref<string>("11/25/2024");

  return {
    dateFrom,
    dateTo,
  };
});

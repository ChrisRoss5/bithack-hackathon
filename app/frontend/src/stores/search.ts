import { defineStore } from "pinia";
import { ref } from "vue";

export const useSearchStore = defineStore("search", () => {

  // todo: remove defaults
  const dateFrom = ref<string>("2024-10-28");
  const dateTo = ref<string>("2024-11-02");

  return {
    dateFrom,
    dateTo,
  };
});

import { defineStore } from "pinia";
import { ref } from "vue";

export const useSearchStore = defineStore("search", () => {
  const dateFrom = ref<string>("");
  const dateTo = ref<string>("");

  return {
    dateFrom,
    dateTo,
  };
});

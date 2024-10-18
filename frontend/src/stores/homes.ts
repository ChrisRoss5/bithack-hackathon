import { defineStore } from "pinia";
import { URLS, useAuthStore } from "./auth";

export const useHomesStore = defineStore("homes", () => {
  const authStore = useAuthStore();

  const getHomes = async (startDate?: string, endDate?: string) => {
    const response = await authStore.client.get(
      URLS.GET_HOMES_BY_AVAILABILITY,
      {
        params: {
          startDate,
          endDate,
        },
      },
    );
    return response.data as Home[];
  };

  return {
    getHomes,
  };
});

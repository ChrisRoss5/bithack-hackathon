import { defineStore } from "pinia";
import { URLS, useAuthStore } from "./auth";
import { parseDates } from "@/utils/format";

export const useHomesStore = defineStore("homes", () => {
  const authStore = useAuthStore();

  const getHomes = async (from?: string, to?: string) => {
    const response = await authStore.client.get(
      URLS.GET_HOMES_BY_AVAILABILITY,
      {
        params: {
          from,
          to,
        },
      },
    );

    console.log(response.data);
    const parsedData = parseDates(response.data);
    console.log(parsedData);
    return parsedData as Home[];
  };

  return {
    getHomes,
  };
});

import { defineStore } from "pinia";
import { useI18n } from "vue-i18n";
import { useToast } from "vue-toastification";
import { URLS, useAuthStore } from "./auth";
import { parseDates } from "@/utils/format";

export const useContractsStore = defineStore("contracts", () => {
  const authStore = useAuthStore();
  const toast = useToast();
  const { t } = useI18n();

  const submitContract = async (contractForm: ContractForm) => {
    const response = await authStore.client.post(
      URLS.CREATE_CONTRACT,
      contractForm,
    );
    if (response.status === 200) toast.success(t("operationSuccess"));
    else toast.error(t("serverError"));
  };

  const getContracts = async () => {
    const response = await authStore.client.get(URLS.GET_CONTRACTS_BY_USER, {
      params: { userId: authStore.userInfo?.id },
    });
    if (response.status === 200) return response.data;
    else toast.error(t("serverError"));
  };

  const getContractRanges = async (contractId: string) => {
    const response = await authStore.client.get(URLS.GET_CONTRACT_RANGES, {
      params: { contractId },
    });
    if (response.status === 200) {
      const parsedData = parseDates(response.data);
      return parsedData as ContractRange[];
    } else toast.error(t("serverError"));
  };

  return {
    submitContract,
    getContracts,
    getContractRanges,
  };
});

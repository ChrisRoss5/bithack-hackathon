import { defineStore } from "pinia";
import { ref } from "vue";
import { URLS, useAuthStore } from "./auth";
import { useToast } from "vue-toastification";
import { useI18n } from "vue-i18n";

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

  return {
    submitContract,
  };
});

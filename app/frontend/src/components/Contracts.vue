<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";
import { useContractsStore } from "@/stores/contracts";
import { ROLE } from "@/types";
import { onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import Contract from "./Contract.vue";

const { t } = useI18n();
const authStore = useAuthStore();
const contractsStore = useContractsStore();
const contracts = ref<Contract[]>([]);

onMounted(async () => {
  if (authStore.userInfo?.role != ROLE.USER) {
    contracts.value = await contractsStore.getContracts();
  } else {
    contracts.value = await contractsStore.getContracts();
  }
});
</script>

<template>
  <div>
    <div class="mx-auto my-3 w-full rounded-md p-3 shadow-md">
      <div class="py-5 text-center text-2xl font-bold">
        {{ t("header.contracts") }}
      </div>
      <div v-if="!contracts.length">Nema rezultata</div>
      <template v-if="authStore.userInfo?.role == ROLE.CITY_OFFICIAL">
      </template>
      <Contract
        v-for="contract in contracts"
        :key="contract.id"
        :contract="contract"
        class="mb-3"
      >
      </Contract>
    </div>
  </div>
</template>

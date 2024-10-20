<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";
import { useContractsStore } from "@/stores/contracts";
import { onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import Reservation from "./Reservation.vue";

const { t } = useI18n();
const authStore = useAuthStore();
const contractsStore = useContractsStore();
const contracts = ref<Contract[]>([]);

onMounted(async () => {
  contracts.value = await contractsStore.getContracts();
});
</script>

<template>
  <div>
    <div class="mx-auto my-3 w-full rounded-md p-3 shadow-md">
      <div class="py-5 text-center text-2xl font-bold">
        {{ t("header.contracts") }}
      </div>
      <div v-for="contract in contracts" :key="contract.id" class="mb-3">
        <div class="dsy-collapse bg-base-200">
          <input type="radio" name="accordion" checked="true" />
          <div class="dsy-collapse-title p-0 text-xl font-medium">
            <div class="flex flex-wrap items-center gap-x-6">
              <img
                :src="contract.communityHome.pictureUrl"
                alt="Movie Image"
                class="aspect-video lg:w-40 object-cover w-full"
              />
              <div>
                <div class="text-xl font-bold">
                  {{ contract.communityHome.name }}
                </div>
                <div>
                  {{ t("dateCreated") }}:
                  {{
                    new Date(contract.dateOfIssue).toLocaleDateString(
                      $i18n.locale,
                    )
                  }}
                </div>
              </div>
              <div class="ml-auto p-3 text-secondary">
                STATUS:
                <span class="font-bold"
                  >{{ t(`contract.status.${contract.status}`) }}
                </span>
              </div>
            </div>
          </div>
          <div class="dsy-collapse-content">
            <Reservation :home="contract.communityHome" :contract="contract" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

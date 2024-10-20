<script setup lang="ts">
import { useI18n } from "vue-i18n";
import Reservation from "./Reservation.vue";
const { t } = useI18n();

const props = defineProps<{
  contract: Contract;
}>();
</script>

<template>
  <div>

    <div class="dsy-collapse bg-base-200">
      <input type="radio" name="accordion" checked="true" />
      <div class="dsy-collapse-title p-0 text-xl font-medium">
        <div class="flex flex-wrap items-center gap-x-6">
          <img
            v-if="contract.communityHome.pictureUrl"
            :src="contract.communityHome.pictureUrl"
            alt="Movie Image"
            class="aspect-video w-full object-cover lg:w-40"
          />
          <img
            v-else
            src="@/assets/images/no-image-en.svg"
            class="aspect-video w-full object-cover lg:w-40"
            alt=""
          />
          <div>
            <div class="text-xl font-bold">
              {{ contract.communityHome.name }}
            </div>
            <div>
              {{ t("dateCreated") }}:
              {{
                new Date(contract.dateOfIssue).toLocaleDateString($i18n.locale)
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
</template>

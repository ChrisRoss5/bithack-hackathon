<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();
const authStore = useAuthStore();

const form = ref<HTMLFormElement | null>(null);
const email = ref<HTMLInputElement | null>(null);
const phoneNumber = ref<HTMLInputElement | null>(null);

const formValid = computed(() => {
  return {
    username: authStore.userInfo?.username,
    firstName: authStore.userInfo?.firstName,
    lastName: authStore.userInfo?.lastName,
    email: authStore.userInfo?.email && email.value?.checkValidity(),
    address: authStore.userInfo?.address,
    city: authStore.userInfo?.city,
    oib: authStore.userInfo?.oib,
    postalCode: authStore.userInfo?.postalCode,
    phoneNumber:
      authStore.userInfo?.phoneNumber,
  };
});

onMounted(() => {
  authStore.getUserInfo();
});

const handleSubmit = () => {
  authStore.updateUserInfo();
};
</script>

<template>
  <div>
    <div class="mx-auto my-3 w-1/2 rounded-md p-3 shadow-md">
      <div
        role="alert"
        class="dsy-alert dsy-alert-warning mx-auto mt-3 w-fit"
        v-if="!authStore.userInfo?.firstName"
      >
        <span class="material-symbols-outlined"> warning </span>
        {{ t("account.completeProfile") }}
      </div>
      <div class="py-5 text-center text-2xl font-bold">
        {{ t("header.userAccount") }}
      </div>
      <form
        ref="form"
        class="grid items-center gap-3 lg:grid-cols-2 [&>*:nth-child(odd)]:text-right [&>*]:p-3"
      >
        <div>{{ t("account.username") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.username"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.username }"
        />
        <div>{{ t("account.firstName") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.firstName"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.firstName }"
        />
        <div>{{ t("account.lastName") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.lastName"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.lastName }"
        />
        <div>{{ t("account.email") }}</div>
        <input
          type="email"
          required
          v-model="authStore.userInfo!.email"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.email }"
          ref="email"
        />
        <div>{{ t("account.address") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.address"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.address }"
        />
        <div>{{ t("account.city") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.city"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.city }"
        />
        <div>{{ t("account.postalCode") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.postalCode"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.postalCode }"
        />

        <div>{{ t("account.oib") }}</div>
        <input
          type="text"
          required
          v-model="authStore.userInfo!.oib"
          class="dsy-input dsy-input-bordered"
          :class="{ 'dsy-input-error': !formValid.oib }"
        />
        <div>{{ t("account.phoneNumber") }}</div>
        <input
          type="tel"
          required
          v-model="authStore.userInfo!.phoneNumber"
          class="dsy-input dsy-input-bordered"
          ref="phoneNumber"
          :class="{ 'dsy-input-error': !formValid.phoneNumber }"
        />
      </form>
      <div class="flex justify-end pt-3">
        <div
          class="dsy-btn dsy-btn-outline w-full"
          :class="{
            'dsy-btn-disabled': Object.values(formValid).some((v) => !v),
          }"
          @click="handleSubmit"
        >
          {{ t("save") }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { useAuthStore } from "../../stores/auth";

defineProps<{
  inDrawer?: boolean;
}>();

const { t } = useI18n();

const authStore = useAuthStore();

const handleClick = () => {
  (document.activeElement as any).blur(); // Todo?
};

const handleLogout = () => {
  authStore.onLogout();
};
</script>

<template>
  <div
    v-if="authStore.isLoggedIn"
    class="dsy-dropdown dsy-dropdown-end dsy-dropdown-hover"
  >
    <button class="dsy-btn m-1" :class="{ 'bg-base-100': inDrawer }">
      {{ t("header.greeting") }}, {{ authStore.userInfo?.displayName }}
    </button>
    <ul
      tabindex="0"
      class="dsy-menu dsy-dropdown-content z-[1] w-52 rounded-box bg-base-100 p-2 text-base-content shadow"
    >
      <li @click="handleClick">
        <RouterLink to="/korisnik">
          {{ t("header.userAccount") }}
          <span class="material-symbols-outlined ml-auto"> person </span>
        </RouterLink>
      </li>
      <li @click="handleClick">
        <button @click="handleLogout">
          {{ t("header.logout") }}
          <span class="material-symbols-outlined ml-auto"> logout </span>
        </button>
      </li>
    </ul>
  </div>
  <RouterLink
    v-else
    to="/prijava"
    class="dsy-btn"
    :class="{ 'bg-base-100': inDrawer }"
  >
    {{ t("header.login") }}
  </RouterLink>
</template>

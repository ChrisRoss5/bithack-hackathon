<script setup lang="ts">
import { ref } from "vue";
import Language from "./Language.vue";
import Links from "./Links.vue";
import Login from "./Login.vue";
import Theme from "./Theme.vue";
import Title from "./Title.vue";

const isOpen = ref(false);
</script>

<template>
  <div class="dsy-drawer dsy-drawer-end ml-auto w-auto lg:hidden">
    <input
      id="main-drawer"
      type="checkbox"
      class="dsy-drawer-toggle"
      :checked="isOpen"
    />
    <div class="dsy-drawer-content">
      <label for="main-drawer">
        <span class="material-symbols-outlined lg:hidden"> menu </span>
      </label>
    </div>
    <div class="dsy-drawer-side">
      <label
        for="main-drawer"
        aria-label="close sidebar"
        class="dsy-drawer-overlay"
      ></label>
      <div
        class="flex min-h-full w-80 flex-col items-center gap-3 bg-base-200 p-4 text-base-content"
      >
        <Title />
        <div class="dsy-divider"></div>
        <Login :in-drawer="true" />
        <div class="dsy-divider"></div>
        <div class="flex items-center flex-wrap">
          <Theme />
          <Language />
        </div>
        <div class="dsy-divider"></div>
        <Links />
      </div>
    </div>
  </div>
</template>

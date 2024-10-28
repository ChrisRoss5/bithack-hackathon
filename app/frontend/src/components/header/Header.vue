<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import Drawer from "./Drawer.vue";
import Language from "./Language.vue";
import Links from "./Links.vue";
import Login from "./Login.vue";
import Theme from "./Theme.vue";
import Title from "./Title.vue";

const route = useRoute();
const isLandingPage = computed(() => route.path === "/");

const stickyDiv = ref<HTMLElement | null>(null);
const isSticky = ref(false);

onMounted(() => {
  const observer = new IntersectionObserver(
    ([e]) => (isSticky.value = e.intersectionRatio < 1),
    { threshold: [1] },
  );
  observer.observe(stickyDiv.value!);
});
</script>

<template>
  <div class="sticky top-[-1px] z-20 h-0" ref="stickyDiv">
    <div
      class="flex w-full flex-wrap items-center gap-5 p-4 transition-all duration-300 lg:p-8"
      :class="{
        'text-base-content': !isLandingPage,
        'text-white': isLandingPage && !isSticky,
        'bg-base-100/75 !py-3 text-base-content shadow-md backdrop-blur-md':
          isSticky,
      }"
    >
      <Title />
      <div
        class="hidden justify-center gap-6 border-l border-base-content pl-6 lg:flex"
        :class="{
          '!border-white': isLandingPage && !isSticky,
        }"
      >
        <Links />
      </div>
      <div class="ml-auto hidden items-center justify-center gap-3 lg:flex">
        <Login />
        <Theme />
        <Language />
      </div>
      <Drawer />
    </div>
  </div>
</template>

<style>
.router-link-exact-active:not(#title) {
  color: oklch(var(--p));
  text-shadow: 0 0 1px currentColor;
}
</style>
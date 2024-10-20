<script setup lang="ts">
import Header from "./components/header/Header.vue";

const htmlEl = document.documentElement;

window.addEventListener("scroll", throttle(handleScroll, 10));

function handleScroll() {
  const startHeight = document.body.offsetHeight;
  const pixelsScrolled = htmlEl.scrollTop + htmlEl.clientHeight;
  const totalScroll = htmlEl.scrollHeight;
  const percentage =
    (pixelsScrolled - startHeight) / (totalScroll - startHeight);

  if (percentage > 0) {
    htmlEl.style.backgroundColor = `oklch(var(--b3) / ${1 - percentage}) !important`;
  } else {
    htmlEl.style.backgroundColor = "";
  }
}

function throttle<T extends (...args: any[]) => void>(
  func: T,
  delay: number,
): T {
  let lastCall = 0;
  return function (this: any, ...args: Parameters<T>) {
    const now = new Date().getTime();
    if (now - lastCall >= delay) {
      lastCall = now;
      func.apply(this, args);
    }
  } as T;
}
</script>

<template>
  <div
    class="mx-auto mb-[75vh] overflow-clip max-w-screen-2xl bg-base-100/90 shadow-xl backdrop-blur-md 2xl:mt-6 2xl:rounded-3xl"
  >
    <Header />
    <RouterView class="px-8 pb-8 pt-24 lg:pt-32" />
  </div>
</template>

<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref } from "vue";
import Result from "./Result.vue";

defineProps<{
  homes: Home[] | undefined;
}>();
const emits = defineEmits<{
  (e: "scrolledIn", scrolledIn: boolean): void;
  (e: "reserve", home: Home): void;
}>();

const resultsDiv = ref<HTMLElement | null>(null);

onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

onBeforeUnmount(() => {
  window.removeEventListener("scroll", handleScroll);
});

function handleScroll() {
  const resultsBounds = resultsDiv.value?.getBoundingClientRect();
  emits("scrolledIn", !!resultsBounds?.top && resultsBounds.top < 100);
}
</script>

<template>
  <div class="mt-6 scroll-mt-28 lg:mt-0 lg:px-6" ref="resultsDiv">
    <div v-if="homes === undefined" class="pb-20 text-center">
      Rezultati pretrage
    </div>
    <div v-else-if="!homes.length" class="pb-20 text-center">
      Nema rezultata
    </div>
    <div v-else class="grid grid-cols-1 gap-4 lg:grid-cols-2 xl:grid-cols-3 pb-6">
      <Result
        v-for="home in homes"
        :key="home.id"
        :home="home"
        @reserve="emits('reserve', home)"
        class="dsy-card bg-base-100 shadow-xl"
      />
    </div>
  </div>
</template>

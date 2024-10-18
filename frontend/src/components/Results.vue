<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";

const emits = defineEmits<{
  (e: "scrolledIn", scrolledIn: boolean): void;
}>();

const resultsDiv = ref<HTMLElement | null>(null);

const { t } = useI18n();

defineProps<{
  homes: Home[] | undefined;
}>();

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
  <div class="scroll-mt-28 p-6 pt-0" ref="resultsDiv">
    <div v-if="homes === undefined" class="pb-20 text-center">
      Rezultati pretrage
    </div>
    <div v-else-if="!homes.length" class="pb-20 text-center">
      Nema rezultata
    </div>
    <div v-else class="grid grid-cols-1 gap-4 lg:grid-cols-2 xl:grid-cols-3">
      <div v-for="home in homes" :key="home.id" class="dsy-card bg-base-100">
        <figure>
          <img
            src="https://img.daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.webp"
            alt="Shoes"
          />
        </figure>
        <div class="dsy-card-body">
          <h2 class="dsy-card-title">
            {{ home.name }}
            <div class="dsy-badge dsy-badge-primary h-auto px-2 py-1">
              <span class="material-symbols-outlined pr-1"> euro </span>
              {{ home.homeBills }}
            </div>
            <div class="dsy-badge dsy-badge-secondary h-auto px-2 py-1">
              <span class="material-symbols-outlined pr-1"> group </span>
              {{ home.capacity }}
            </div>
          </h2>
          <div>{{ home.address }}</div>
          <div class="separator bg-base-300"></div>
          <div>{{ home.cutleryPrice }}</div>
          <div class="dsy-btn dsy-btn-outline">
            {{ t("home.reserve") }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Slider from "@vueform/slider";
import { ref } from "vue";

const padding = ref(0);
const slider = ref({
  value: [0, 24],
  max: 24,
  min: 0,
  step: 1,
  lazy: false,
  tooltips: true,
  format: {
    suffix: ":00",
    decimals: 0,
  },
  options: {
    margin: 8,
    behaviour: "drag",
  },
});
</script>

<template>
  <div>
    Kontakt
    {{ slider }}
    <Slider class="slider" v-model="slider.value" v-bind="slider" />
    <div @click="padding += 1">PADDING</div>
  </div>
</template>

<!-- https://github.com/vueform/slider?tab=readme-ov-file#installation -->
<style src="@vueform/slider/themes/default.css"></style>
<style>
.slider {
  --slider-height: 1rem;
  --slider-handle-width: 1.5rem;
  --slider-handle-height: 1.5rem;
}
</style>

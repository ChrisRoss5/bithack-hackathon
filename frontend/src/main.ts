import { useMediaQuery } from "@vueuse/core";
import { createPinia } from "pinia";
import VWave from "v-wave";
import { createApp, reactive } from "vue";
import App from "./App.vue";
import router from "./router/router";
import i18n from "./services/i18n";
import "./styles/main.css";

export const $breakpoints = reactive({
  lgAndUp: useMediaQuery("(min-width: 1024px)"),
});

const pinia = createPinia();
const app = createApp(App);
app.config.globalProperties.$breakpoints = $breakpoints;
app.use(pinia).use(router).use(VWave, {}).use(i18n).mount("#app");

declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $breakpoints: typeof $breakpoints;
    $i18n: any; // todo
  }
}

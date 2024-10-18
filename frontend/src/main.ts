import { createPinia } from "pinia";
import VWave from "v-wave";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router/router";
import i18n from "./services/i18n";
import "./styles/main.css";

const pinia = createPinia();

createApp(App).use(pinia).use(router).use(VWave, {}).use(i18n).mount("#app");

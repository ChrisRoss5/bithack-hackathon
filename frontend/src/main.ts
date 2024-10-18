import { createPinia } from "pinia";
import VWave from "v-wave";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router/router";
import "./styles/main.css";

const pinia = createPinia();

createApp(App).use(pinia).use(router).use(VWave, {}).mount("#app");

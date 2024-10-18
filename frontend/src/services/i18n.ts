import en from "@/assets/locales/en.json";
import hr from "@/assets/locales/hr.json";
import { createI18n, type I18nOptions } from "vue-i18n";

const defaultLocale = (getNavigatorLanguage() || "hr").split("-")[0];
const locale = localStorage.getItem("locale") || defaultLocale;

const options: I18nOptions = {
  legacy: false,
  locale,
  messages: { hr, en },
};

const i18n = createI18n<false, typeof options>(options);

function getNavigatorLanguage() {
  if (navigator.languages && navigator.languages.length) {
    return navigator.languages[0];
  } else {
    return (
      (navigator as any).userLanguage ||
      (navigator as any).language ||
      (navigator as any).browserLanguage
    );
  }
}

export default i18n;

import {
  DefineLocaleMessage,
  DefineDateTimeFormat,
  DefineNumberFormat,
} from "vue-i18n";

declare module "vue-i18n" {
  export interface DefineLocaleMessage {
    appName: string;
    header: {
      contact: string;
      aboutUs: string;
      login: string;
      greeting: string;
      userAccount: string;
      logout: string;
      lightMode: string;
      darkMode: string;
    };
    home: {
      title: string;
      subtitle: string;
      searchBox: {
        from: string;
        to: string;
        fromDescription: string;
        toDescription: string;
        showAll: string;
        search: string;
      },
      reserve: string;
      cancel: string;
      hour: string;
      free: string;
      partlyFree: string;
    }
  }

  export interface DefineDateTimeFormat {
    short: {
      hour: "numeric";
      minute: "numeric";
      second: "numeric";
      timeZoneName: "short";
      timezone: string;
    };
  }

  export interface DefineNumberFormat {
    currency: {
      style: "currency";
      currencyDisplay: "symbol";
      currency: string;
    };
  }
}

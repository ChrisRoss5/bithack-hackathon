import { CreateAxiosDefaults } from "axios";
import { jwtDecode } from "jwt-decode";
import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { createAxiosClient } from "../services/createAxiosClient";
import { DecodedJwt, formatDecodedData } from "../utils/jwt";

interface Tokens {
  accessToken: string;
  refreshToken: string;
}

const REST_API_URL = "https://localhost:7044/api"; // Todo: import.meta.env.VITE_REST_API_URL;
export const URLS = {
  REST_API_URL,
  REFRESH_TOKEN_URL: `${REST_API_URL}/auth/refresh-token`,
  FIREBASE_LOGIN_URL: `${REST_API_URL}/auth/authorize-firebase-client`,
  GET_HOMES_BY_AVAILABILITY: `${REST_API_URL}/community-homes/get-by-availability`,
};

const AXIOS_CLIENT_OPTIONS: CreateAxiosDefaults = {
  baseURL: REST_API_URL,
  timeout: 300000,
  headers: {
    "Content-Type": "application/json",
  },
};

function setTokensToLocalStorage({ accessToken, refreshToken }: Tokens) {
  localStorage.setItem("accessToken", accessToken);
  localStorage.setItem("refreshToken", refreshToken);
}

function removeTokensFromLocalStorage() {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
}

export const useAuthStore = defineStore("auth", () => {
  const accessToken = ref(localStorage.getItem("accessToken"));
  const refreshToken = ref(localStorage.getItem("refreshToken"));
  const isLoggedIn = computed(() => !!accessToken.value);
  const userInfo = ref<User | null>(null);

  const initUser = (accessToken: string) => {
    const decoded = jwtDecode(accessToken);
    const formattedData = formatDecodedData(decoded as DecodedJwt);
    userInfo.value = {
      id: formattedData.userId!,
      email: formattedData.email!,
      username: formattedData.name!,
      role: formattedData.role!,
      createdAt: new Date(), //  todo
    };
  };

  if (isLoggedIn.value) initUser(accessToken.value!);

  const onLogin = (tokens: Tokens) => {
    setTokensToLocalStorage(tokens);
    accessToken.value = tokens.accessToken;
    refreshToken.value = tokens.refreshToken;
    initUser(tokens.accessToken);
  };

  const onLogout = () => {
    removeTokensFromLocalStorage();
    accessToken.value = null;
    refreshToken.value = null;
  };

  const axiosClient = ref(
    createAxiosClient({
      options: AXIOS_CLIENT_OPTIONS,
      getCurrentAccessToken: () => accessToken.value,
      getCurrentRefreshToken: () => refreshToken.value,
      refreshTokenUrl: URLS.REFRESH_TOKEN_URL,
      logout: onLogout,
      setRefreshedTokens: onLogin,
    }),
  );

  return {
    userInfo,
    isLoggedIn,
    onLogin,
    onLogout,
    client: axiosClient,
  };
});

<script setup lang="ts">
import { getAuth } from "firebase/auth";
import firebase from "firebase/compat/app";
import "firebaseui/dist/firebaseui.css";
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { useFirebaseUI } from "../firebase";
import { URLS, useAuthStore } from "../stores/auth";

const router = useRouter();
const authStore = useAuthStore();

onMounted(() => {
  const ui = useFirebaseUI();
  ui.reset();
  ui.start("#firebaseui-auth-container", {
    signInOptions: [
      {
        provider: firebase.auth.GoogleAuthProvider.PROVIDER_ID,
      },
      firebase.auth.TwitterAuthProvider.PROVIDER_ID,
      firebase.auth.GithubAuthProvider.PROVIDER_ID,
      "microsoft.com",
      "yahoo.com",
      firebase.auth.EmailAuthProvider.PROVIDER_ID,
      {
        provider: firebase.auth.PhoneAuthProvider.PROVIDER_ID,
        defaultCountry: "HR",
      },
    ],
    signInFlow: "popup",
    callbacks: {
      signInSuccessWithAuthResult: (result) => {
        loginToDOTNET(result);
        return false;
      },
    },
  });
});

async function loginToDOTNET(result: any) {
  console.log(result);
  //const accessToken = result.credential.accessToken;
  //const idToken = result.credential.idToken;
  const idToken2 = await getAuth().currentUser?.getIdToken();
  //console.log(`accessToken: ${accessToken}`);
  //console.log(`idToken: ${idToken}`);
  console.log(`getIdToken: ${idToken2}`);

  const response = await authStore.client.post(URLS.FIREBASE_LOGIN_URL, null, {
    params: {
      firebaseToken: idToken2,
    },
    authorization: false,
  });
  authStore.onLogin({
    accessToken: response.data.accessToken,
    refreshToken: response.data.refreshToken,
  });
  router.replace("/korisnik");
}
</script>

<template>
  <div>
    <div id="firebaseui-auth-container" class="py-10"></div>
  </div>
</template>

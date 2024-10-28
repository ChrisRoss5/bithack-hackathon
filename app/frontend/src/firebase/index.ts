/*
todo
import firebase from "firebase/compat/app";
import { auth } from "firebaseui";
import { getAuth } from "firebase/auth";
//import { setLogLevel } from "firebase/firestore"

//setLogLevel("debug")

export const firebaseApp = firebase.initializeApp({
  apiKey: "AIzaSyBFGSG38oYtYtoYNdnkwdxHPxrvWa-1408",
  authDomain: "bjelovar-drustveni-domovi.firebaseapp.com",
  projectId: "bjelovar-drustveni-domovi",
  storageBucket: "bjelovar-drustveni-domovi.appspot.com",
  messagingSenderId: "106045364405",
  appId: "1:106045364405:web:a410a298f80abec70d46fc",
  measurementId: "G-HMK8RQ6F5L",
});


let ui: auth.AuthUI;

export function useFirebaseUI() {
  ui ??= new (window as any).firebaseui.auth.AuthUI(getAuth());
  return ui;
}
 */

import firebase from "firebase/compat/app";
 import { auth } from "firebaseui";
//import { setLogLevel } from "firebase/firestore"

//setLogLevel("debug")

export const firebaseApp = firebase.initializeApp({
  apiKey: "AIzaSyBFGSG38oYtYtoYNdnkwdxHPxrvWa-1408",
  authDomain: "bjelovar-drustveni-domovi.firebaseapp.com",
  projectId: "bjelovar-drustveni-domovi",
  storageBucket: "bjelovar-drustveni-domovi.appspot.com",
  messagingSenderId: "106045364405",
  appId: "1:106045364405:web:a410a298f80abec70d46fc",
  measurementId: "G-HMK8RQ6F5L"
});

export const firebaseAuth = firebase.auth();

let ui: auth.AuthUI;

export function useFirebaseUI() {
  ui ??= new auth.AuthUI(firebaseAuth);
  return ui;
}

import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import type { RootStackParamList } from "./types";

import Splash from "../screen/Intro/Splash";
import IntroScreen from "../screen/Intro/IntroScreen";
import Home from "../screen/Home";
import Login from "../screen/Auth/Login";
import Register from "../screen/Auth/Register";
import LocationPicker from "../screen/Auth/LocationPicker";
import ForgotPassword from "../screen/Auth/ForgotPassword";
import GymPage from "../screen/GymPage";

const Stack = createNativeStackNavigator<RootStackParamList>();
const isAuthenticated  = false; 
export default function AppNavigator() {
  return (
    <NavigationContainer>
  {/* Change initialRouteName to "GymPage" to skip everything else */}
  <Stack.Navigator 
    initialRouteName="GymPage" 
    screenOptions={{ headerShown: false }}
  >
    {/* 1. Global Screens (Accessible regardless of Auth) */}
    <Stack.Screen name="GymPage" component={GymPage} />

    {isAuthenticated ? (
      <>
        <Stack.Screen name="Home" component={Home} />
        <Stack.Screen name="LocationPicker" component={LocationPicker} />
      </>
    ) : (
      <>
        <Stack.Screen name="Splash" component={Splash} />
        <Stack.Screen name="Intro" component={IntroScreen} />
        <Stack.Screen name="Login" component={Login} />
        <Stack.Screen name="Register" component={Register} />
        <Stack.Screen name="ForgotPassword" component={ForgotPassword} />
      </>
    )}
  </Stack.Navigator>
</NavigationContainer>
  );
}

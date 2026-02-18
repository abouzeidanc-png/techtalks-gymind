import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import type { RootStackParamList } from "./types";

import Splash from "../screen/Intro/Splash";
import IntroScreen from "../screen/Intro/IntroScreen";
import Login from "../screen/Auth/Login";
import Register from "../screen/Auth/Register";
import LocationPicker from "../screen/Auth/LocationPicker";
import ForgotPassword from "../screen/Auth/ForgotPassword";

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="Splash" component={Splash} />
        <Stack.Screen name="Intro" component={IntroScreen} />
        <Stack.Screen name="Login" component={Login} />
        <Stack.Screen name="Register" component={Register} />
        <Stack.Screen name="ForgotPassword" component={ForgotPassword} /> 
        <Stack.Screen
  name="LocationPicker"
  component={LocationPicker}
/>

      </Stack.Navigator>
    </NavigationContainer>
  );
}

import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import type { RootStackParamList } from "./types";

import Splash from "../screen/Splash";
import IntroScreen from "../screen/Intro/IntroScreen";
import Login from "../screen/Login";
import Home from "../screen/Home";

const Stack = createNativeStackNavigator<RootStackParamList>();
const isAuthenticated  = false; // Placeholder for auth state, replace with real logic

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        {isAuthenticated ? (
          <>
            <Stack.Screen name="Login" component={Login} />
          </>
        ) : (
          <>
            <Stack.Screen name="Splash" component={Splash} /> 
            <Stack.Screen name="Intro" component={IntroScreen} />
            
            <Stack.Screen name="Home" component={ Home } />
          </>
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
}

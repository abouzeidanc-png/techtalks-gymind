  import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import type { RootStackParamList } from "./types";

import Splash from "../screens/Splash";
import Intro1 from "../screens/Intro/Intro1";
import Intro2 from "../screens/Intro/Intro2";
import Intro3 from "../screens/Intro/Intro3";
import Login from "../screens/Login"; // make sure you have it

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="Splash" component={Splash} />
        <Stack.Screen name="Intro1" component={Intro1} />
        <Stack.Screen name="Intro2" component={Intro2} />
        <Stack.Screen name="Intro3" component={Intro3} />
        <Stack.Screen name="Login" component={Login} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}

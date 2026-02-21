import React, { useState } from "react";
import {
  View,
  Text,
  StyleSheet,
  TextInput,
  Pressable,
  Image,
  SafeAreaView,
} from "react-native";
import type { NativeStackScreenProps } from "@react-navigation/native-stack";
import type { RootStackParamList } from "../../navigation/types";

type Props = NativeStackScreenProps<RootStackParamList, "Login">;

export default function Login({ navigation }: Props) {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [focused, setFocused] = useState<"email" | "password" | null>(null);

  return (
    <SafeAreaView style={styles.container}>
      {/* Logo */}
      <Image
        source={require("../../../assets/icon.png")} 
        style={styles.logo}
        resizeMode="contain"
      />

      {/* Title */}
      <Text style={styles.title}>
        Welcome <Text style={styles.orange}>Back</Text>
      </Text>
      <Text style={styles.subtitle}>Let's continue your journey</Text>

      {/* Email input */}
      <View
        style={[
          styles.inputContainer,
          focused === "email" && styles.inputFocused,
        ]}
      >
        <Text style={styles.inputIcon}>✉️</Text>
        <TextInput
          placeholder="Email"
          placeholderTextColor="#585757"
          style={styles.input}
          value={email}
          onChangeText={setEmail}
          onFocus={() => setFocused("email")}
          onBlur={() => setFocused(null)}
        />
      </View>

      {/* Password input */}
      <View
        style={[
          styles.inputContainer,
          focused === "password" && styles.inputFocused,
        ]}
      >
        <Text style={styles.inputIcon}>🔑</Text>
        <TextInput
          placeholder="Password"
          placeholderTextColor="#585757"
          style={styles.input}
          secureTextEntry
          value={password}
          onChangeText={setPassword}
          onFocus={() => setFocused("password")}
          onBlur={() => setFocused(null)}
        />
        <Text style={styles.eye}>👁️</Text>
      </View>

      {/* Forgot password */}
      <Pressable onPress={() => navigation.navigate("ForgotPassword")}>
        <Text style={styles.forgot}>Forgot Password</Text>
      </Pressable>


      {/* Continue button */}
      <Pressable style={styles.button}>
        <Text style={styles.buttonText}
          onPress={() => navigation.navigate("Home")}
        >
          Continue
        </Text>
      </Pressable>

<Text style={styles.footer}>
  New here ?{" "}
  <Text
    style={styles.create}
    onPress={() => navigation.navigate("Register")}
  >
    Create an account
  </Text>
</Text>

    </SafeAreaView>
  );
}

const COLORS = {
  dark: "#292929",
  gray: "#585757",
  light: "#F5ECE4",
  orange: "#FC6A0A",
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.light,
    paddingHorizontal: 24,
  },

  logo: {
    width: 150,
    height: 150,
    marginTop: 45,
    alignSelf: "center",
    marginBottom: 28,
  },

  title: {
    fontSize: 35,
    fontWeight: "800",
    color: COLORS.dark,
    textAlign: "center",
  },

  orange: {
    color: COLORS.orange,
  },

  subtitle: {
    color: COLORS.gray,
    marginTop: 6,
    marginBottom: 38,
    fontSize: 14,
    textAlign: "center",
  },

  inputContainer: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    gap: 8,
    backgroundColor: "#EDEDED",
    borderRadius: 14,
    paddingHorizontal: 14,
    marginBottom: 25,
    width: "90%",
    height: 50,
    borderWidth: 2,
    borderColor: "transparent",
    alignSelf: "center",
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.07,
    shadowRadius: 5,
    elevation: 2,
  },

  inputFocused: {
    borderColor: COLORS.orange,
  },

  inputIcon: {
    fontSize: 16,
    marginRight: 8,
  },

  eye: {
    fontSize: 16,
    marginLeft: 8,
    opacity: 0.7,
  },

  input: {
    flex: 1,
    color: COLORS.dark,
    fontSize: 15,
    paddingVertical: 0, 
    textAlign: "left", 
  },

  forgot: {
    color: COLORS.gray,
    fontSize: 13,
    textAlign: "center",
    marginTop: 6,
    marginBottom: 30,
  },

  button: {
    backgroundColor: COLORS.orange,
    width: "90%",
    height: 52,
    borderRadius: 18,
    alignItems: "center",
    justifyContent: "center",
    marginBottom: 26,
    alignSelf: "center",    shadowColor: COLORS.orange,
    shadowOffset: { width: 0, height: 6 },
    shadowOpacity: 0.25,
    shadowRadius: 8,
    elevation: 6,
  },

  buttonText: {
    color: "#1F1F1F",
    fontSize: 18,
    fontWeight: "700",
  },

  footer: {
    fontSize: 14,
    color: COLORS.gray,
    textAlign: "center",
  },

  create: {
    color: COLORS.dark,
    fontWeight: "700",
  },
});

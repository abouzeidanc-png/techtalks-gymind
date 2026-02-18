import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  StyleSheet,
  TextInput,
  Pressable,
  Image,
  SafeAreaView,
  ScrollView,
} from "react-native";
import type { NativeStackScreenProps } from "@react-navigation/native-stack";
import type { RootStackParamList } from "../../navigation/types";
import DateTimePicker from "@react-native-community/datetimepicker";

type Props = NativeStackScreenProps<RootStackParamList, "Register">;

export default function Register({ navigation, route }: Props) {
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [password, setPassword] = useState("");
  const [gender, setGender] = useState("");
  const [location, setLocation] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [focused, setFocused] = useState<string | null>(null);

  const [showDatePicker, setShowDatePicker] = useState(false);

  // ✅ Receive selected location from map screen
  useEffect(() => {
    if (route.params?.selectedLocation) {
      setLocation(route.params.selectedLocation);
    }
  }, [route.params]);

  // ✅ Handle Date Change
  const handleDateChange = (event: any, selectedDate?: Date) => {
    setShowDatePicker(false);

    if (selectedDate) {
      const formatted = selectedDate.toISOString().split("T")[0]; // YYYY-MM-DD
      setDateOfBirth(formatted);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        showsVerticalScrollIndicator={false}
        keyboardShouldPersistTaps="handled"
        contentContainerStyle={styles.scrollContent}
      >
        {/* Logo */}
        <Image
          source={require("../../../assets/icon.png")}
          style={styles.logo}
          resizeMode="contain"
        />

        {/* Title */}
        <Text style={styles.title}>
          Create <Text style={styles.orange}>account</Text>
        </Text>
        <Text style={styles.subtitle}>Let's start your journey</Text>

        {/* Full Name */}
        <View
          style={[
            styles.inputContainer,
            focused === "fullName" && styles.inputFocused,
          ]}
        >
          <Text style={styles.inputIcon}>👤</Text>
          <TextInput
            placeholder="Full name"
            placeholderTextColor="#585757"
            style={styles.input}
            value={fullName}
            onChangeText={setFullName}
            onFocus={() => setFocused("fullName")}
            onBlur={() => setFocused(null)}
          />
        </View>

        {/* Email */}
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

        {/* Phone */}
        <View
          style={[
            styles.inputContainer,
            focused === "phone" && styles.inputFocused,
          ]}
        >
          <Text style={styles.inputIcon}>📞</Text>
          <TextInput
            placeholder="Phone number"
            placeholderTextColor="#585757"
            style={styles.input}
            keyboardType="phone-pad"
            value={phone}
            onChangeText={setPhone}
            onFocus={() => setFocused("phone")}
            onBlur={() => setFocused(null)}
          />
        </View>

        {/* Password */}
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
        </View>

        {/* Gender */}
        <View
          style={[
            styles.inputContainer,
            focused === "gender" && styles.inputFocused,
          ]}
        >
          <Text style={styles.inputIcon}>⚧️</Text>
          <TextInput
            placeholder="Gender"
            placeholderTextColor="#585757"
            style={styles.input}
            value={gender}
            onChangeText={setGender}
            onFocus={() => setFocused("gender")}
            onBlur={() => setFocused(null)}
          />
        </View>

        {/* Location (Open Map) */}
        <View style={styles.inputContainer}>
          <Text style={styles.inputIcon}>📍</Text>

          <Pressable
            style={{ flex: 1 }}
            onPress={() => navigation.navigate("LocationPicker")}
          >
            <Text
              style={{
                color: location ? COLORS.dark : COLORS.gray,
                fontSize: 15,
              }}
            >
              {location || "Select Location"}
            </Text>
          </Pressable>
        </View>

        {/* Date of Birth (Calendar Picker) */}
        <View style={styles.inputContainer}>
          <Text style={styles.inputIcon}>🎂</Text>

          <Pressable
            style={{ flex: 1 }}
            onPress={() => setShowDatePicker(true)}
          >
            <Text
              style={{
                color: dateOfBirth ? COLORS.dark : COLORS.gray,
                fontSize: 15,
              }}
            >
              {dateOfBirth || "Select Date of Birth"}
            </Text>
          </Pressable>
        </View>

        {showDatePicker && (
          <DateTimePicker
            value={dateOfBirth ? new Date(dateOfBirth) : new Date()}
            mode="date"
            display="spinner"

            maximumDate={new Date()}
            onChange={handleDateChange}
          />
        )}

        {/* Continue Button */}
        <Pressable style={styles.button}>
          <Text style={styles.buttonText}>Continue</Text>
        </Pressable>

        {/* Login Redirect */}
        <Text style={styles.footer}>
          Already have account?{" "}
          <Text
            style={styles.create}
            onPress={() => navigation.navigate("Login")}
          >
            Log in
          </Text>
        </Text>
      </ScrollView>
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

  scrollContent: {
    paddingBottom: 40,
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

  input: {
    flex: 1,
    color: COLORS.dark,
    fontSize: 15,
    paddingVertical: 0,
    textAlign: "left",
  },

  button: {
    backgroundColor: COLORS.orange,
    width: "90%",
    height: 52,
    borderRadius: 18,
    alignItems: "center",
    justifyContent: "center",
    marginBottom: 26,
    alignSelf: "center",
    shadowColor: COLORS.orange,
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

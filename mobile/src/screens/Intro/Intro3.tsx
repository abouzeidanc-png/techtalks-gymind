import React from "react";
import {
  View,
  Text,
  StyleSheet,
  ImageBackground,
  Pressable,
  SafeAreaView,
} from "react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../../navigation/types";

type Props = NativeStackScreenProps<RootStackParamList, "Intro3">;

export default function Intro3({ navigation }: Props) {
  const onSkip = () => {
    navigation.replace("Login");
  };

  const onNext = () => {
    navigation.replace("Login");
  };

  return (
    <View style={styles.container}>
      <ImageBackground
        source={require("../../../assets/intro3.png")}
        style={styles.bg}
        resizeMode="cover"
      >
        {/* Top bar */}
        <View style={styles.topBar}>
          <Pressable onPress={onSkip} hitSlop={10}>
            <Text style={styles.skipText}>Skip</Text>
          </Pressable>
        </View>

        {/* Bottom overlay content */}
        <View style={styles.bottomArea}>
          <View style={styles.overlay} />

          <View style={styles.content}>
            <Text style={styles.subtitle}>Track your progress</Text>
            <Text style={styles.title}>Achieve Your Goals</Text>

            {/* Dots */}
            <View style={styles.dots}>
              <View style={styles.dot} />
              <View style={styles.dot} />
              <View style={[styles.dot, styles.dotActive]} />
            </View>

            {/* Get Started button */}
            <Pressable onPress={onNext} style={styles.nextBtn}>
              <Text style={styles.nextText}>Get Started</Text>
            </Pressable>
          </View>
        </View>
      </ImageBackground>
    </View>
  );
}

const COLORS = {
  primary: "#1E3A8A", // Modern blue
  secondary: "#3B82F6", // Lighter blue
  accent: "#F59E0B", // Warm orange
  background: "#0F172A", // Dark navy
  surface: "#1E293B", // Lighter dark
  text: "#F8FAFC", // Off-white
  textSecondary: "#CBD5E1", // Muted text
  dark: "#292929",
  light: "#F5ECE4",
  gray: "#585757",
  orange: "#FC6A0A",
};

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: COLORS.background },
  bg: { flex: 1 },

  topBar: {
    paddingTop: 55,
    paddingHorizontal: 20,
    alignItems: "flex-end",
  },
  skipText: {
    color: COLORS.text,
    opacity: 0.85,
    fontSize: 14,
    fontWeight: "500",
  },

  bottomArea: {
    flex: 1,
    justifyContent: "flex-end",
  },

  overlay: {
    ...StyleSheet.absoluteFillObject,
    top: "45%",
    backgroundColor: COLORS.background,
    opacity: 0.85,
    transform: [{ skewY: "-8deg" }],
  },

  content: {
    paddingHorizontal: 24,
    paddingBottom: 40,
    paddingTop: 200,
  },

  subtitle: {
    color: COLORS.textSecondary,
    fontSize: 18,
    marginBottom: 12,
    fontWeight: "400",
    letterSpacing: 0.5,
  },

  title: {
    color: COLORS.text,
    fontSize: 36,
    fontWeight: "800",
    marginBottom: 32,
    lineHeight: 44,
    letterSpacing: -0.5,
  },

  dots: {
    flexDirection: "row",
    gap: 12,
    alignSelf: "center",
    marginTop: 20,
    marginBottom: 32,
  },
  dot: {
    width: 24,
    height: 6,
    borderRadius: 3,
    backgroundColor: COLORS.surface,
    opacity: 0.6,
  },
  dotActive: {
    backgroundColor: COLORS.accent,
    opacity: 1,
    transform: [{ scaleX: 1.5 }],
  },

  nextBtn: {
    alignSelf: "center",
    backgroundColor: COLORS.secondary,
    paddingVertical: 16,
    paddingHorizontal: 32,
    borderRadius: 25,
    shadowColor: COLORS.secondary,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
    elevation: 8,
  },
  nextText: {
    color: COLORS.text,
    fontSize: 18,
    fontWeight: "600",
    textAlign: "center",
  },
});

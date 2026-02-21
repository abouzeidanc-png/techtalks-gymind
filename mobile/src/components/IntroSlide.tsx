import React from "react";
import {
  View,
  Text,
  StyleSheet,
  ImageBackground,
  Pressable,
} from "react-native";

type Props = {
  image: any;
  subtitle: string;
  title: string;
  totalSlides: number;
  activeIndex: number;
  buttonText: string;
  onSkip: () => void;
  onNext: () => void;
};

export default function IntroSlide({
  image,
  subtitle,
  title,
  totalSlides,
  activeIndex,
  buttonText,
  onSkip,
  onNext,
}: Props) {
  return (
    <View style={styles.container}>
      <ImageBackground source={image} style={styles.bg} resizeMode="cover">
        {/* Skip */}
        <View style={styles.topBar}>
          <Pressable onPress={onSkip}>
            <Text style={styles.skipText}>Skip</Text>
          </Pressable>
        </View>

        {/* Bottom content */}
        <View style={styles.bottomArea}>
          <View style={styles.overlay} />

          <View style={styles.content}>
            <Text style={styles.subtitle}>{subtitle}</Text>
            <Text style={styles.title}>{title}</Text>

            {/* Dots */}
            <View style={styles.dots}>
              {Array.from({ length: totalSlides }).map((_, i) => (
                <View
                  key={i}
                  style={[
                    styles.dot,
                    i === activeIndex && styles.dotActive,
                  ]}
                />
              ))}
            </View>

            {/* Button */}
            <Pressable style={styles.nextBtn} onPress={onNext}>
              <Text style={styles.nextText}>{buttonText}</Text>
            </Pressable>
          </View>
        </View>
      </ImageBackground>
    </View>
  );
}

const COLORS = {
  secondary: "#3B82F6",
  accent: "#F59E0B",
  background: "#0F172A",
  surface: "#1E293B",
  text: "#F8FAFC",
  textSecondary: "#CBD5E1",
};

const styles = StyleSheet.create({
  container: { flex: 1 },
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

  bottomArea: { flex: 1, justifyContent: "flex-end" },

  overlay: {
    ...StyleSheet.absoluteFillObject,
    top: "45%",
    bottom: "5%",
    backgroundColor: COLORS.background,
    opacity: 0.85,
    transform: [{ skewY: "-8deg" }],
  },

  content: {
    paddingHorizontal: 24,
    paddingBottom: 80,
    paddingTop: 100,
    alignItems: "center",
    justifyContent: "center",
  },

  subtitle: {
    color: COLORS.textSecondary,
    fontSize: 18,
    marginBottom: 12,
  },

  title: {
    color: COLORS.text,
    fontSize: 36,
    fontWeight: "800",
    marginBottom: 32,
    lineHeight: 44,
  },

  dots: {
    flexDirection: "row",
    alignSelf: "center",
    gap: 12,
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
  },
  nextText: {
    color: COLORS.text,
    fontSize: 18,
    fontWeight: "600",
  },
});

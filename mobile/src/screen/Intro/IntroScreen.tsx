import React, { useRef, useState } from "react";
import { View, FlatList, Dimensions } from "react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../../navigation/types";
import IntroSlide from "../../components/IntroSlide";

const { width } = Dimensions.get("window");

type Props = NativeStackScreenProps<RootStackParamList, "Intro">;

const SLIDES = [
  {
    image: require("../../../assets/intro1.png"),
    subtitle: "Find the best gyms near you",
    title: "Discover Nearby Gyms",
  },
  {
    image: require("../../../assets/intro2.png"),
    subtitle: "Your fitness, your rules",
    title: "Your Fitness Journey",
  },
  {
    image: require("../../../assets/intro3.png"),
    subtitle: "Track your progress",
    title: "Achieve Your Goals",
  },
];

export default function IntroScreen({ navigation }: Props) {
  const [index, setIndex] = useState(0);
  const listRef = useRef<FlatList>(null);

  const goNext = () => {
    if (index === SLIDES.length - 1) {
      navigation.replace("Login");
    } else {
      listRef.current?.scrollToIndex({
        index: index + 1,
        animated: true,
      });
    }
  };

  return (
    <FlatList
      ref={listRef}
      data={SLIDES}
      horizontal
      pagingEnabled
      showsHorizontalScrollIndicator={false}
      keyExtractor={(_, i) => i.toString()}
      onMomentumScrollEnd={(e) =>
        setIndex(Math.round(e.nativeEvent.contentOffset.x / width))
      }
      renderItem={({ item }) => (
        <View style={{ width }}>
          <IntroSlide
            image={item.image}
            subtitle={item.subtitle}
            title={item.title}
            totalSlides={SLIDES.length}
            activeIndex={index}
            buttonText={index === SLIDES.length - 1 ? "Get Started" : "Next"}
            onSkip={() => navigation.replace("Login")}
            onNext={goNext}
          />
        </View>
      )}
    />
  );
}

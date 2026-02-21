import React, { useEffect, useState } from "react";
import {
  View,
  StyleSheet,
  ActivityIndicator,
  Pressable,
  Text,
} from "react-native";
import MapView, { Marker, MapPressEvent } from "react-native-maps";
import * as Location from "expo-location";
import type { NativeStackScreenProps } from "@react-navigation/native-stack";
import type { RootStackParamList } from "../../navigation/types";

type Props = NativeStackScreenProps<RootStackParamList, "LocationPicker">;

export default function LocationPicker({ navigation }: Props) {
  const [region, setRegion] = useState<any>(null);
  const [marker, setMarker] = useState<any>(null);

  useEffect(() => {
    (async () => {
      const { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== "granted") return;

      const userLocation = await Location.getCurrentPositionAsync({});
      setRegion({
        latitude: userLocation.coords.latitude,
        longitude: userLocation.coords.longitude,
        latitudeDelta: 0.01,
        longitudeDelta: 0.01,
      });
    })();
  }, []);

  const handleMapPress = (event: MapPressEvent) => {
    setMarker(event.nativeEvent.coordinate);
  };

  const handleConfirm = async () => {
    if (!marker) return;

    const address = await Location.reverseGeocodeAsync(marker);

    const formattedAddress = `${address[0]?.city || ""}, ${
      address[0]?.country || ""
    }`;

navigation.navigate("Register", {
  selectedLocation: formattedAddress,
});

  };

  if (!region) return <ActivityIndicator style={{ flex: 1 }} />;

  return (
    <View style={styles.container}>
      <MapView
        style={styles.map}
        initialRegion={region}
        onPress={handleMapPress}
      >
        {marker && <Marker coordinate={marker} />}
      </MapView>

      <Pressable style={styles.button} onPress={handleConfirm}>
        <Text style={styles.buttonText}>Confirm Location</Text>
      </Pressable>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1 },
  map: { flex: 1 },
  button: {
    position: "absolute",
    bottom: 40,
    alignSelf: "center",
    backgroundColor: "#FC6A0A",
    paddingHorizontal: 40,
    paddingVertical: 15,
    borderRadius: 20,
  },
  buttonText: {
    fontWeight: "bold",
  },
});

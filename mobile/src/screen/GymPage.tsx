import React, { useEffect, useState } from 'react';
import { View, Text, Image, ScrollView, TouchableOpacity, StyleSheet, ActivityIndicator, Alert } from 'react-native';
import { spacing } from '../theme/spacing';
import { colors } from '../theme/colors';
import { typography } from '../theme/typography';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

// Standard boilerplate for navigation types
type RootStackParamList = {
  GymPage: { gymId: string };
};

type Props = NativeStackScreenProps<RootStackParamList, 'GymPage'>;

// This should match the DTO 
interface Gym {
  gymID: string;
  name: string;
  description: string;
  address: string;
}

const GymPage = ({ route }: Props) => {
  /* If we're deep-linking or bypassing login, route.params might be empty.
     We use this hardcoded GUID as a safety net so the page doesn't just sit there.
  */
  const gymId = route?.params?.gymId || "88d6270c-56ae-4384-ba06-4532cee0c691";
  
  const [gym, setGym] = useState<Gym | null>(null);
  const [loading, setLoading] = useState(true);

  /* NOTE: We're using HTTP (not HTTPS) because local dev certificates 
     usually play havoc with Android/iOS network security policies. 
  */
  const API_URL = "https://192.168.0.108:7179/api/gyms/{id}"; 

  useEffect(() => {
    fetchGymDetails();
  }, [gymId]);

  const fetchGymDetails = async () => {
    try {
      setLoading(true);
      
      // Standard fetch call. If you hit "Network Request Failed", 
      // check your Windows Firewall and launchSettings.json first.
      const response = await fetch(`${API_URL}/${gymId}`);
      
      if (!response.ok) {
        throw new Error(`Server responded with ${response.status}`);
      }

      const data = await response.json();
      setGym(data);
    } catch (error) {
      console.error("DEBUG -> Gym Fetch Error:", error);
      Alert.alert("Connection Error", "Couldn't reach the backend. Check your IP and Firewall.");
    } finally {
      setLoading(false);
    }
  };

  // Keep the loader centered so it doesn't look janky
  if (loading) {
    return (
      <View style={[styles.container, styles.centered]}>
        <ActivityIndicator size="large" color={colors.primary} />
      </View>
    );
  }

  return (
    <ScrollView style={styles.container} showsVerticalScrollIndicator={false}>
      
      {/* Visual Header: Uses the overlapping logo style from the design spec */}
      <View style={styles.headerContainer}>
        <Image 
          source={{ uri: 'https://via.placeholder.com/400x200' }} 
          style={styles.headerImage} 
        />
        <View style={styles.logoCircle}>
          <Image 
            source={{ uri: 'https://via.placeholder.com/100' }} 
            style={styles.logoImage} 
          />
        </View>
      </View>

      <View style={styles.content}>
        <Text style={styles.gymName}>{gym?.name || "Gym Name"}</Text>
        
        {/* Traffic/Status indicators */}
        <View style={styles.statusRow}>
          <Text style={styles.statusBadge}>
            <Text style={{ color: '#4CAF50' }}>● </Text>Low traffic
          </Text>
          <Text style={styles.openBadge}>Open</Text>
        </View>

        <TouchableOpacity 
          style={styles.enrollButton} 
          activeOpacity={0.8}
          onPress={() => Alert.alert("Coming Soon", "Enrollment feature is in progress.")}
        >
          <Text style={styles.enrollText}>Enroll</Text>
        </TouchableOpacity>

        {/* Info Card: About */}
        <View style={styles.card}>
          <Text style={styles.cardTitle}>About the Gym</Text>
          <Text style={styles.cardBody}>
            {gym?.description || "No description provided by the gym."}
          </Text>
        </View>

        {/* Info Card: Location */}
        <View style={styles.card}>
          <View style={styles.rowBetween}>
            <Text style={styles.cardTitle}>Location & Branches</Text>
            <TouchableOpacity>
               <Text style={styles.viewAll}>View all branches {'>'}</Text>
            </TouchableOpacity>
          </View>
          <Text style={styles.locationText}>📍 {gym?.address || "Location not available"}</Text>
        </View>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    backgroundColor: colors.background // #F5ECE4
  },
  centered: {
    justifyContent: 'center',
    alignItems: 'center'
  },
  headerContainer: {
    height: 200,
    width: '100%',
  },
  headerImage: { 
    width: '100%', 
    height: '100%' 
  },
  logoCircle: {
    position: 'absolute',
    bottom: -35,
    left: spacing.lg,
    width: 80,
    height: 80,
    borderRadius: 40,
    borderWidth: 3,
    borderColor: colors.primary, // #FC6A0A
    backgroundColor: colors.white,
    justifyContent: 'center',
    alignItems: 'center',
    elevation: 5, // Android shadow
    shadowColor: '#000', // iOS shadow
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
  },
  logoImage: { 
    width: 70, 
    height: 70, 
    borderRadius: 35 
  },
  content: { 
    padding: spacing.md, 
    marginTop: 40, // Offsets the overlapping logo
    alignItems: 'center' 
  },
  gymName: { 
    fontSize: typography.h2, 
    fontWeight: 'bold', 
    color: colors.primaryDark, 
    textAlign: 'center' 
  },
  statusRow: { 
    flexDirection: 'row', 
    backgroundColor: '#FAD7BD', 
    borderRadius: 20, 
    paddingHorizontal: spacing.md, 
    paddingVertical: 4,
    marginTop: spacing.sm 
  },
  statusBadge: { 
    fontSize: typography.small, 
    color: colors.secondaryDark, 
    marginRight: spacing.sm 
  },
  openBadge: { 
    fontSize: typography.small, 
    fontWeight: 'bold', 
    color: '#2D6A4F' 
  },
  enrollButton: {
    backgroundColor: colors.primary, 
    width: '100%',
    padding: spacing.md,
    borderRadius: 12,
    alignItems: 'center',
    marginVertical: spacing.lg, 
  },
  enrollText: { 
    color: colors.white, 
    fontSize: typography.h3, 
    fontWeight: 'bold' 
  },
  card: { 
    backgroundColor: colors.white, 
    width: '100%', 
    padding: spacing.md, 
    borderRadius: 15, 
    marginBottom: spacing.md,
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
  },
  cardTitle: { 
    fontSize: typography.body, 
    fontWeight: 'bold', 
    color: colors.primaryDark, 
    marginBottom: spacing.xs 
  },
  cardBody: { 
    fontSize: typography.small, 
    color: colors.secondaryDark, 
    lineHeight: 20 
  },
  rowBetween: { 
    flexDirection: 'row', 
    justifyContent: 'space-between', 
    alignItems: 'center' 
  },
  viewAll: { 
    color: colors.primary, 
    fontSize: 12 
  },
  locationText: { 
    fontSize: typography.small, 
    color: colors.secondaryDark, 
    marginTop: spacing.xs 
  }
});

export default GymPage;
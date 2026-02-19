import React, { useEffect, useState } from 'react';
import { View, Text, Image, ScrollView, TouchableOpacity, StyleSheet, ActivityIndicator } from 'react-native';
import { spacing } from '../theme/spacing'; //
import { colors } from '../theme/colors'; //
import { typography } from '../theme/typography'; //
import { NativeStackScreenProps } from '@react-navigation/native-stack';

// 1. Navigation Types
type RootStackParamList = {
  GymPage: { gymId: string };
};

type Props = NativeStackScreenProps<RootStackParamList, 'GymPage'>;

// 2. Data Interface matching your GymsController/GymDto
interface Gym {
  gymID: string;
  name: string;
  description: string;
  address: string;
}

const GymPage = ({ route }: Props) => {
  const { gymId } = route.params; // Get the ID from navigation
  const [gym, setGym] = useState<Gym | null>(null);
  const [loading, setLoading] = useState(true);

  // 3. Localhost Connection
  // Use 10.0.2.2 for Android Emulator, or your local IP for physical devices
  const API_URL = "https://192.168.0.108:7179/api/gyms"; 

  useEffect(() => {
    fetchGymDetails();
  }, [gymId]);

  const fetchGymDetails = async () => {
    try {
      // Connects to [HttpGet("{id:guid}")] 
      const response = await fetch(`${API_URL}/${gymId}`);
      const data = await response.json();
      setGym(data);
    } catch (error) {
      console.error("Error fetching gym:", error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <View style={[styles.container, { justifyContent: 'center' }]}>
        <ActivityIndicator size="large" color={colors.primary} />
      </View>
    );
  }

  return (
    <ScrollView style={styles.container}>
      {/* Header Image & Logo  */}
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
        {/* Mapping to  GymDto.Name */}
        <Text style={styles.gymName}>{gym?.name || "Gym Name"}</Text>
        
        <View style={styles.statusRow}>
          <Text style={styles.statusBadge}>
            <Text style={{ color: '#4CAF50' }}>● </Text>Low traffic
          </Text>
          <Text style={styles.openBadge}>Open</Text>
        </View>

        <TouchableOpacity style={styles.enrollButton}>
          <Text style={styles.enrollText}>Enroll</Text>
        </TouchableOpacity>

        {/* About Section */}
        <View style={styles.card}>
          <Text style={styles.cardTitle}>About the Gym</Text>
          <Text style={styles.cardBody}>
            {gym?.description || "Welcome to our premium fitness center."}
          </Text>
        </View>

        {/* Location Section mapping to GymDto.Address */}
        <View style={styles.card}>
          <View style={styles.rowBetween}>
            <Text style={styles.cardTitle}>Location & Branches</Text>
            <TouchableOpacity>
               <Text style={styles.viewAll}>View all branches {'>'}</Text>
            </TouchableOpacity>
          </View>
          <Text style={styles.locationText}>📍 {gym?.address || "123 Fitness St."}</Text>
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
    left: spacing.lg, // 24
    width: 80,
    height: 80,
    borderRadius: 40,
    borderWidth: 3,
    borderColor: colors.primary, // #FC6A0A
    backgroundColor: colors.white, // #FFFFFF
    justifyContent: 'center',
    alignItems: 'center',
    elevation: 5,
    shadowColor: '#000',
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
    padding: spacing.md, // 16
    marginTop: 40, // Space for the overlapping logo
    alignItems: 'center' 
  },
  gymName: { 
    fontSize: typography.h2, // 22
    fontWeight: 'bold', 
    color: colors.primaryDark, // #292929
    textAlign: 'center' 
  },
  statusRow: { 
    flexDirection: 'row', 
    backgroundColor: '#FAD7BD', 
    borderRadius: 20, 
    paddingHorizontal: spacing.md, 
    paddingVertical: 4,
    marginTop: spacing.sm // 8
  },
  statusBadge: { 
    fontSize: typography.small, // 14
    color: colors.secondaryDark, // #585757
    marginRight: spacing.sm 
  },
  openBadge: { 
    fontSize: typography.small, 
    fontWeight: 'bold', 
    color: '#2D6A4F' 
  },
  enrollButton: {
    backgroundColor: colors.primary, // #FC6A0A
    width: '100%',
    padding: spacing.md,
    borderRadius: 12,
    alignItems: 'center',
    marginVertical: spacing.lg, // 24
  },
  enrollText: { 
    color: colors.white, 
    fontSize: typography.h3, // 18
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
    fontSize: typography.body, // 16
    fontWeight: 'bold', 
    color: colors.primaryDark, 
    marginBottom: spacing.xs // 4
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
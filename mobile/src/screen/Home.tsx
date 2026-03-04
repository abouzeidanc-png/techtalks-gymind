import { ScrollView, Text } from "react-native";
import MembershipStatusPill from "../components/home/MembershipStatusPill";
import TopGymsCarousel from "../components/home/TopGymsCarousel";
import AppContainer from "../components/ui/AppContainer";
import { colors } from "../theme/colors";

const Home = () => {
    return ( 
        <AppContainer>
            <ScrollView 
                showsVerticalScrollIndicator={ false }
            >
                {/* Greeting */}
                <Text
                    style={{
                        fontSize: 24,
                        fontWeight: "700",
                        color: colors.primaryDark,
                    }}
                >
                    Good Evening 👋
                </Text>

                <Text
                    style={{
                        fontSize: 16,
                        color: colors.secondaryDark,
                        marginBottom: 10,
                    }}
                >
                    Let's train strong today
                </Text>
                
                {/* Membership Test: "active" | "expiring" | "expired"; */}
                <MembershipStatusPill status="expired" daysLeft={12}/>

                {/* Filters + Search will go here */}

                {/* Top Gyms */}
                <TopGymsCarousel />

                {/* Nearby List (next section) */}

            </ScrollView>
        </AppContainer>       
     );
}
 
export default Home;
import { Text } from "react-native";
import MembershipStatusPill from "../components/home/MembershipStatusPill";
import TopGymsCarousel from "../components/home/TopGymsCarousel";
import AppContainer from "../components/ui/AppContainer";

const Home = () => {
    return ( 
        <AppContainer>
            <Text 
                style={{ 
                    fontSize: 24,
                    fontWeight: "bold", 
                    marginBottom: 16,
                    textAlign: "center",
                    color: "#bb3636",
                }}
            >
                Welcome to GymInd! 
            </Text>
            
            <MembershipStatusPill />
            {/* <TopGymsCarousel /> */}

        </AppContainer>       
     );
}
 
export default Home;
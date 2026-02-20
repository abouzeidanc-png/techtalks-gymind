import { ReactNode } from "react";
import { View } from "react-native";

interface AppCardProps {
    children: ReactNode;
}
const AppCard = ({ children }: AppCardProps) => {
    return ( 
        <View
            style={{
                backgroundColor: "white",
                padding: 16,
                borderRadius: 16,
                shadowColor: "#000",
                shadowOpacity: 0.08,
                shadowRadius: 8,
                elevation: 3,
                marginBottom: 16,
            }}
        >
            {children}
        </View>
     );
}
 
export default AppCard;
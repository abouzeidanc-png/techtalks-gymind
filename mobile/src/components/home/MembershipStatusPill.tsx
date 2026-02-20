import { Text, View } from "react-native";

const MembershipStatusPill = () => {
    return ( 
        <View
            style={{
                backgroundColor: "#bb3636",
                paddingVertical: 6,
                paddingHorizontal: 12,
                borderRadius: 20,
                alignSelf: "flex-start",
            }}
        >
            <Text
                style={{
                    color: "#fff",
                    fontSize: 14,
                }}
            >
                Membership Status: Active
            </Text>
        </View>
     );
}
 
export default MembershipStatusPill;
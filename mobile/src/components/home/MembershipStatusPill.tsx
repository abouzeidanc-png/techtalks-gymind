import { Text, View } from "react-native";
import { colors } from "../../theme/colors";

type MembershipStatus = "active" | "expiring" | "expired";

interface MembershipStatusPillProps {
    status: MembershipStatus;
    daysLeft?: number;
}

const MembershipStatusPill = ({ status, daysLeft }: MembershipStatusPillProps) => {
    const config = {
        active: {
            bg: "#E6F6EC",
            text: colors.primaryDark,
            label: `Active . ${daysLeft} days left`,
        },
        expiring: {
            bg: "#FFF4E5",
            text: colors.primaryDark,
            label: `Expires in ${daysLeft} days`,
        },
        expired: {
            bg: "#FFE6E6",
            text: colors.danger,
            label: "Membership expired · Renew now",
        }
    };

    const current = config[status];

    return ( 
        <View
            style={{
                backgroundColor: current.bg,
                paddingVertical: 6,
                paddingHorizontal: 12,
                borderRadius: 20,
                alignSelf: "flex-start",
            }}
        >
            <Text
                style={{
                    color: current.text,
                    fontSize: 14,
                }}
            >
                {current.label}
            </Text>
        </View>
     );
}
 
export default MembershipStatusPill;
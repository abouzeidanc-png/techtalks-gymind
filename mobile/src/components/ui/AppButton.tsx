import { Pressable, Text } from "react-native";
import { colors } from "../../theme/colors";

interface AppButtonProps {
    title: string;
    onPress: () => void;
}
const AppButton = ({ title, onPress }: AppButtonProps ) => {
    return ( 
        <Pressable 
            onPress={onPress}
            style={({ pressed}) => ({
                backgroundColor: colors.primary,
                padding: 16,
                borderRadius: 12,
                opacity: pressed ? 0.8 : 1,
            })}
        >
            <Text
                style={{
                    color: "white",
                    textAlign: "center",
                    fontWeight: 600,
                    fontSize: 16,
                }}
            >
                {title}
            </Text>
        </Pressable>
     );
}
 
export default AppButton;
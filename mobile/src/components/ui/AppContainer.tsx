import { ReactNode } from "react";
import { View } from "react-native";
import { colors } from "../../theme/colors";

interface AppContainerProps {
    children: ReactNode;
}

const AppContainer = ( {children}: AppContainerProps ) => {
    return ( 
        <View
            style={{ 
                flex: 1, 
                backgroundColor: colors.background,
                padding: 16,
            }}
        >
            {children}
        </View>
     );
}
 
export default AppContainer;
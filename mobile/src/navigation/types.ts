export type RootStackParamList = {
  Splash: undefined;
  Intro1: undefined;
  Intro2: undefined;
  Intro3: undefined;
  Intro: undefined;
  ForgotPassword: undefined;
  Login: undefined;
  Main: undefined;

  Register:
    | {
        selectedLocation?: string;
      }
    | undefined;

  LocationPicker: undefined;
  Home: undefined;
};

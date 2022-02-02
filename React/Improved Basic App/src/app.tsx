import React from "react";
import { RouterComponent,ProfileProvider } from "./core";

export const App = () => {
  return (
    <ProfileProvider>
      <RouterComponent />
    </ProfileProvider>
  );
};

import React from "react";
import { useNavigate } from "react-router-dom";
import { routes, ProfileContext } from "../../core";
import { LoginComponent } from "./login.component";

export const LoginContainer: React.FC = () => {
  const navigate = useNavigate();
  const { setUserProfile } = React.useContext(ProfileContext);

  const handleLogin = (username: string, password: string) => {
    if (username === "admin" && password === "test") {
      setUserProfile({ username: username });
      navigate(routes.list);
    } else {
      alert("User / password not valid, psst... admin / test");
    }
  };

  return <LoginComponent onLogin={handleLogin} />;
};
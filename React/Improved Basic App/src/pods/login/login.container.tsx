import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { LoginComponent } from "./login.component";
import { doLogin } from "./login.api";
import { Login } from "./login.vm";
import { ProfileContext, routes } from "../../core";

const useLoginHook = () => {
  const navigate = useNavigate();
  const { setUserProfile } = React.useContext(ProfileContext);

  const handleLogin = (login: Login) => {
    const { username, password } = login;
    doLogin(username, password).then((result) => {
      if (result) {
        setUserProfile({ username: username });
        navigate(routes.list);
      } else {
        alert("User / password not valid, psst... admin / test");
      }
    });
  };

  return { handleLogin };
};

export const LoginContainer: React.FC = () => {
  const { handleLogin } = useLoginHook();

  return <LoginComponent onLogin={handleLogin} />;
};
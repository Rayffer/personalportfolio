import React from "react";
import { useNavigate } from "react-router-dom";
import { routes, ProfileContext } from "../../core";
import { LoginComponent } from "./login.component";
import { doLogin } from "./login.api";

const useLoginHook = () => {
    const navigate = useNavigate();
    const { setUserProfile } = React.useContext(ProfileContext);

    const handleLogin = (username: string, password: string) => {
        doLogin(username, password).then(result => {

            if (result) {
                setUserProfile({ username: username });
                navigate(routes.list);
            } else {
                alert("User / password not valid, psst... admin / test");
            }
        })
    };

    return { handleLogin};
};

export const LoginContainer: React.FC = () => {
    const {handleLogin} = useLoginHook();

    return <LoginComponent onLogin={handleLogin} />;
};
import React from "react";
import { Login, createEmptyLogin } from "./login.vm";

interface Props {
  onLogin: (login: Login) => void;
}

export const LoginComponent: React.FC<Props> = (props) => {
  const { onLogin } = props;
  const [login, setLogin] = React.useState<Login>(createEmptyLogin());

  const handleNavigation = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onLogin(login);
  };

  const updateField = (name: keyof Login) => (e) => {
    setLogin({
      ...login,
      [name]: e.target.value,
    });
  };

  return (
    <>
      <form onSubmit={handleNavigation}>
        <div className="login-container">
          <input
            placeholder="Username"
            value={login.username}
            onChange={updateField("username")}
          />
          <input
            placeholder="Password"
            type="password"
            value={login.password}
            onChange={updateField("password")}
          />
          <button type="submit">Login</button>
        </div>
      </form>
    </>
  );
};

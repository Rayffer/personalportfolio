import React from "react";
import { ProfileContext } from "../core";

export const AppLayout: React.FC = ({ children }) => {
    const {username} = React.useContext(ProfileContext);

    return (
        <div className="layout-app-container">
            <div className="layout-app-header">{username}</div>
            {children}
        </div>
    );
};
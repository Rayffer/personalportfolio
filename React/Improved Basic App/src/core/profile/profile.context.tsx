import React from "react";
import { UserProfile, createEmptyUserProfile } from "./profile.vm";

type UserProfileFn = (userProfile: UserProfile) => void;

interface Context extends UserProfile {
    setUserProfile: UserProfileFn;
}

const noUserLogin = "no user login";

export const ProfileContext = React.createContext<Context>({
    username: noUserLogin,
    setUserProfile: () => console.warn("If you are reading this, I am dead already"),

});

export const ProfileProvider: React.FC = ({ children }) => {
    const [userProfile, setUserProfile] = React.useState<UserProfile>(createEmptyUserProfile());

    return (
        <ProfileContext.Provider value={{ username: userProfile.username, setUserProfile }}>
            {children}
        </ProfileContext.Provider>
    );
};
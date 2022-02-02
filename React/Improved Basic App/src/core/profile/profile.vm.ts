export interface UserProfile {
    username: string;
}

export const createEmptyUserProfile = (): UserProfile => ({
    username: '',
});
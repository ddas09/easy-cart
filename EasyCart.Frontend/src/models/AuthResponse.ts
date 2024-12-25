export interface UserInformation {
    id: number;
    email: string;
}

export interface AuthResponse {
    token: string;
    user: UserInformation;
}
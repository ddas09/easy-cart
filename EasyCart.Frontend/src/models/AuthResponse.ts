import { JwtTokenContainerModel } from "./JwtTokenContainerModel";

export interface UserInformation {
    id: number;
    email: string;
    isAdmin: boolean;
}

export interface AuthResponse {
    tokenContainer: JwtTokenContainerModel;
    user: UserInformation;
}
import { JwtTokenContainerModel } from "./JwtTokenContainerModel";

export interface UserInformation {
    id: number;
    email: string;
}

export interface AuthResponse {
    tokenContainer: JwtTokenContainerModel;
    user: UserInformation;
}
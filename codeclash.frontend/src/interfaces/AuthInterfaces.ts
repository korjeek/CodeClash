export interface RegisterUser {
    username: string;
    email: string;
    password: string;
    passwordConfirm: string;
}

export interface LoginUser {
    email: string;
    password: string;
}

export interface LoginResponse{
    username: string;
    email: string;
    token: string;
    refreshToken: string;
}

export interface Token{
    accessToken: string;
    refreshToken: string;
}
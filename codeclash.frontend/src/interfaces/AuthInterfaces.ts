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
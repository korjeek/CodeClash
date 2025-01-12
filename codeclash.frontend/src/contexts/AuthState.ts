import {createContext, useContext} from "react";
import {LoginResponse} from "../interfaces/AuthInterfaces.ts";

interface AuthContextProps {
    auth: LoginResponse | undefined;
    setAuth: (users: LoginResponse) => void;
}

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const useAuth = (): AuthContextProps => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within a AuthProvider');
    }
    return context;
}
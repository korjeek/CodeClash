import React, {ReactNode, useState} from "react";
import {LoginResponse} from "../interfaces/AuthInterfaces.ts";
import {AuthContext} from "./AuthState.ts"

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [auth, setAuth] = useState<LoginResponse>();

    return (
        <AuthContext.Provider value={{ auth, setAuth }}>
            {children}
        </AuthContext.Provider>
    )
}
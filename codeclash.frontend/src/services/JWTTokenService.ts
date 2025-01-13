import Cookies from 'js-cookie';
import {jwtDecode} from 'jwt-decode';
import {JWTTokenProps} from "../Props/JWTTokenProps.ts";

// Функция для получения токена из куков
export const getTokenFromCookies = (cookieName: string): string | undefined => {
    return Cookies.get(cookieName);
};

// Функция для декодирования JWT-токена и извлечения информации
export const getUserInfoFromToken = (token: string): JWTTokenProps | null => {
    try {
        return jwtDecode(token);
    }
    catch (error) {
        console.error('Error decoding token:', error);
        return null;
    }
};

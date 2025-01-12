import { axiosPrivate } from "../api/axios";
import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import {AxiosInstance} from "axios";
import {useAuth} from "../contexts/AuthState.ts";

const useAxiosPrivate: () => AxiosInstance = () => {
    const refresh = useRefreshToken();
    const auth = useAuth();

    useEffect(() => {
        const responseIntercept = axiosPrivate.interceptors.response.use(
            (response) => {
                console.log('Response:', response);
                return response;
            },
            async (error) => {
                if (error.response && error.response.status === 401) {
                    console.log('Unauthorized response detected.');

                    // Проверяем наличие куки "spooky-cookies"
                    const spookyCookies = document.cookie.split('; ').find((row) => row.startsWith('spooky-cookies='));

                    if (spookyCookies) {
                        console.log('Spooky-cookies found. Attempting token refresh...');
                        try {
                            await refresh();
                            // Повторяем исходный запрос
                            return axiosPrivate(error.config);
                        } catch (refreshError) {
                            console.error('Failed to refresh token:', refreshError);
                            console.log('login') // Перенаправляем на страницу авторизации
                        }
                    } else {
                        console.log('No spooky-cookies found. Redirecting to login...');
                        console.log('login') // Перенаправляем на страницу авторизации
                    }
                }

                return Promise.reject(error);
            }
        );

        return () => {
            axiosPrivate.interceptors.response.eject(responseIntercept);
        }
    }, [auth, refresh])

    return axiosPrivate;
}

export default useAxiosPrivate;
import { axiosPrivate } from "../api/axios";
import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import {AxiosInstance} from "axios";
import Cookies from 'js-cookie';
import {useNavigate} from "react-router-dom";

const useAxiosPrivate: () => AxiosInstance = () => {
    const refresh = useRefreshToken();
    const navigate = useNavigate();

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
                            const accessToken = await refresh();
                            Cookies.set('spooky-cookies', accessToken, { secure: true, sameSite: 'None' });
                            // console.log(auth)
                            // Повторяем исходный запрос
                            return axiosPrivate(error.config);
                        } catch (refreshError) {
                            console.error('Failed to refresh token:', refreshError);
                            navigate('/') // Перенаправляем на страницу авторизации
                        }
                    } else {
                        console.log('No spooky-cookies found. Redirecting to login...');
                        navigate('/') // Перенаправляем на страницу авторизации
                    }
                }

                return Promise.reject(error);
            }
        );

        return () => {
            axiosPrivate.interceptors.response.eject(responseIntercept);
        }
    }, [refresh])

    return axiosPrivate;
}

export default useAxiosPrivate;
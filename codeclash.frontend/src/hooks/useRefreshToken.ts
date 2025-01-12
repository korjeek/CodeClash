import axios from '../api/axios.ts';
import {useAuth} from "../contexts/AuthState.ts";
import {Token} from "../interfaces/AuthInterfaces.ts";

const useRefreshToken = () => {
    const { auth, setAuth } = useAuth();

    return async () => {
        const response = await axios.post<Token>('/auth/refresh-token', undefined, {
            withCredentials: true
        });
        if (auth)
            setAuth(
                {
                    username: auth.username,
                    email: auth.email,
                    token: response.data.accessToken,
                    refreshToken: auth.refreshToken,
                })
        return response.data.accessToken;
    }
};

export default useRefreshToken;
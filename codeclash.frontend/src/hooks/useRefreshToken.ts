import axios from '../api/axios.ts';
import {useAuth} from "../contexts/AuthState.ts";

const useRefreshToken = () => {
    const { setAuth } = useAuth();

    return async () => {
        const response = await axios.post('/auth/refresh-token', undefined, {
            withCredentials: true
        });
    
        setAuth(
            {
                refreshToken: "",
                token: response.data,
                username: "",
                email: ""
            });
        return response.data;
    }
};

export default useRefreshToken;
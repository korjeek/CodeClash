import axios from '../api/axios.ts';
import {useAuth} from "../contexts/AuthState.ts";

const useRefreshToken = () => {
    const { auth, setAuth } = useAuth();

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
        console.log(response, auth);
        return response.data;
    }
};

export default useRefreshToken;
import {  useEffect } from "react";
import useRefreshToken from "../hooks/useRefreshToken.ts";
import {Outlet} from "react-router-dom";
import {useAuth} from "./AuthState.ts";

const PersistLogin = () => {
    const refresh = useRefreshToken();
    const {auth} = useAuth();

    useEffect(() => {
        let isMounted = true;

        const verifyRefreshToken = async () => {
            try {
                await refresh();
            }
            catch (err) {
                console.error(err);
            }
        }

        if (!auth?.token)
        {
            verifyRefreshToken()
            console.log(auth?.token)
        }

        return () => {isMounted = false};
    }, [])

    return (
        <>
            <Outlet/>
        </>
    )
}

export default PersistLogin;
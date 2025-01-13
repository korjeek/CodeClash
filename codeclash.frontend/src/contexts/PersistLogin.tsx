import {  useEffect } from "react";
import useRefreshToken from "../hooks/useRefreshToken.ts";
import {Outlet} from "react-router-dom";
import {useAuth} from "./AuthState.ts";

const PersistLogin = () => {
    const refresh = useRefreshToken();
    const {auth} = useAuth();

    useEffect(() => {
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
    }, [])

    return (
        <>
            <Outlet/>
        </>
    )
}

export default PersistLogin;
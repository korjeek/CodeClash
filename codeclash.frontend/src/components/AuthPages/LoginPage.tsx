import React, {useState} from 'react';
import '../../style/Default/BackGround.css'
import '../../style/AuthPage/AuthField.css'
import '../../style/AuthPage/Input.css'
import '../../style/AuthPage/Button.css'
import {login} from "../../services/AuthService.ts";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../../contexts/AuthState.ts";

export default function RegisterPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [checkState, setCheckState] = useState(false);
    const [wrongLogin, setWrongLogin] = useState('');
    const navigate = useNavigate();
    const { setAuth } = useAuth();

    const handleLogin = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            setCheckState(true);
            const response = await login({ email, password });
            setAuth(response)
            navigate('/competitions')
        }
        catch (error) {
            setCheckState(false);
            setWrongLogin(error.response.data);
        }
    };

    return (
        <form className="auth-field" onSubmit={handleLogin}>
            <h1 className="auth-header">
                <span className="">Log In</span>
            </h1>
            <div className="input-field-container">
                <div className="input-box type-md">
                    <input
                        type="email"
                        required
                        name="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <label htmlFor="email">Email</label>
                    <span className="border"></span>
                </div>
                <div className="input-box type-md">
                    <input
                        type="password"
                        required
                        name="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <label htmlFor="password">Password</label>
                    <span className="border"></span>
                </div>
            </div>
            {wrongLogin && <div className="wrong-login-warning">{wrongLogin}</div>}
            <div className="auth-confirm">
                <button type="submit" className={checkState ? "active-auth-button" : "auth-button"}>
                    {checkState && <div className="loading"></div>}
                    {!checkState && <span className="auth-text">Log In</span>}
                </button>
                <h5>Don't have an account? <a href="/reg" aria-current="page">Sign Up</a></h5>
            </div>
        </form>
    )
};
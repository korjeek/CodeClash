import React, {useState} from 'react';
import '../../style/Default/BackGround.css'
import '../../style/AuthPage/AuthField.css'
import '../../style/AuthPage/Input.css'
import {login} from "../../services/AuthService.ts";
import {useNavigate} from "react-router-dom";

export default function RegisterPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            await login({ email, password });
            navigate('/competitions')
        }
        catch (error) {
            console.error('Login failed:', error);
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
            <div className="auth-confirm">
                <button type="submit" className="auth-button">Log In</button>
                <h5>Don't have an account? <a href="/reg" aria-current="page">Sign Up</a></h5>
            </div>
        </form>
    )
};
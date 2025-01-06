import React, { useState } from 'react';
import '../../style/Default/BackGround.css'
import '../../style/AuthPage/AuthField.css'
import '../../style/AuthPage/Input.css'
import { register } from '../../services/AuthService.ts';
import {useNavigate} from "react-router-dom";

export default function RegisterPage() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [passwordConfirm, setPasswordConfirm] = useState('');
    const navigate = useNavigate();

    const handleRegister = async (event: React.FormEvent) => {
        event.preventDefault()
        try{
            await register({ username, email, password, passwordConfirm })
            navigate('/competitions')
        }
        catch(error) {
            console.error('Register failed:', error);
        }
    }

    return (
        <form className="auth-field" onSubmit={handleRegister}>
            <h1 className="auth-header">
                <span className="">Sign Up</span>
            </h1>
            <div className="input-field-container">
                <div className="input-box type-md">
                    <input
                        type='text'
                        required
                        name="name"
                        id="name"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                    <label htmlFor="name">Username</label>
                    <span className="border"></span>
                </div>
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
                <div className="input-box type-md">
                    <input
                        type="password"
                        required
                        name="confirm-pw"
                        id="confirm-pw"
                        value={passwordConfirm}
                        onChange={(e) => setPasswordConfirm(e.target.value)}
                    />
                    <label htmlFor="confirm-pw">Confirm Password</label>
                    <span className="border"></span>
                </div>
            </div>
            <div className="auth-confirm">
                <button className="auth-button">Sign Up</button>
                <h5>Already have an account? <a href="/login" aria-current="page">Log In</a></h5>
            </div>
        </form>
    )
};
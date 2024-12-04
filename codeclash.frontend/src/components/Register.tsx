// src/components/Register.tsx
import React, { useState } from 'react';
import {Link} from "react-router-dom"
import { register } from '../services/authService';

const Register: React.FC = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirm, setPasswordConfirm] = useState('');

  const handleRegister = async (event: React.FormEvent) => {
    event.preventDefault();
    try {
      const userData = { username, email, password, passwordConfirm };
      const result = await register(userData);
      console.log('Registration successful:', result);
    } catch (error) {
      console.error('Registration failed:', error);
    }
  };

  return (
        <div id="formContent">
            <h2 className="inactive underlineHover"><Link to="/login">Sign In</Link> </h2>
            <h2 className="active"> Sign Up </h2>

            <form onSubmit={handleRegister}>
                <input 
                    type="text"   
                    placeholder='Username'
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    id="login"
                    className="fadeIn"
                    name="login"
                    required
                />
                <input 
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    id="email"
                    className="fadeIn"
                    name="email"
                    required
                />
                <input 
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    id="password"
                    className="fadeIn"
                    name="password"
                    placeholder="Password"
                    required
                />
                <input 
                    type="password"
                    value={passwordConfirm}
                    onChange={(e) => setPasswordConfirm(e.target.value)}
                    id="passwordConfirm"
                    className="fadeIn"
                    name="login"
                    placeholder="Confirm password"
                    required
                />
                <input 
                    type="submit"
                    className="fadeIn"
                    value="Have fun"
                />
            </form>

            <div id="formFooter">
                <Link to="/login">Already have an acount?</Link>
            </div>
        </div>
  );
};

export default Register;

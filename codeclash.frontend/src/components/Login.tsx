// src/components/Login.tsx
import React, { useState } from 'react';
import {Link} from 'react-router-dom'
import { login } from '../services/authService';

const Login: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (event: React.FormEvent) => {
    event.preventDefault();
    try {
      const userData = { email, password };
      const result = await login(userData);
      console.log('Login successful:', result);
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

  return (
  <div id='formContent'>
    <h2 className="active">Sing In</h2>
    <h2 className="inactive underlineHover"><Link to="/register">Sign Up</Link></h2>

    <form onSubmit={handleLogin}>
      <input
        type="text"
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
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        id="password"
        className="fadeIn"
        name="password"
        required
      />
      <input type="submit" className="fadeIn" value="Go For It"/>
    </form>

    <div id="formFooter">
      <a className="underlineHover" href="#">Forgot Password?</a>
    </div>
  </div>
  );
};

export default Login;

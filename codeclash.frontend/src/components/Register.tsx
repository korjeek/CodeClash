// src/components/Register.tsx
import React, { useState } from 'react';
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
    <div className="wrapper fadeInDown">
        <div id="formContent">
            <h2 className="active"> Sign Up </h2>
            <h2 className="inactive underlineHover"><a href="/login">Sign In</a> </h2>

            <form>
                <input 
                    type="text"
                    placeholder='Username'
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    id="login"
                    className="fadeIn second"
                    name="login"
                    required
                />
                <input 
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    id="email"
                    className="fadeIn second"
                    name="email"
                />
                <input 
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    id="password"
                    className="fadeIn third"
                    name="password"
                    placeholder="Password"
                />
                <input 
                    type="password"
                    value={passwordConfirm}
                    onChange={(e) => setPasswordConfirm(e.target.value)}
                    id="passwordConfirm"
                    className="fadeIn third"
                    name="login"
                    placeholder="Confirm password"
                />
                <input 
                    type="submit"
                    className="fadeIn fourth"
                    value="Have fun"
                />
            </form>

            <div id="formFooter">
                <a className="underlineHover" href="#">Forgot Password?</a>
            </div>

        </div>
    </div>
  );
    <form onSubmit={handleRegister}>
      <div >
        <input
          type="text"
          placeholder='Username'
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
      </div>
      <div>
        <input
          type="email"
          placeholder='Email'
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>
      <div>
        <input
          placeholder='Password'
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>
      <div>
        <input
          placeholder='Confirm password'
          type="password"
          value={passwordConfirm}
          onChange={(e) => setPasswordConfirm(e.target.value)}
          required
        />
      </div>
      <button type="submit">Join all the fun</button>
    </form>
//   );
};

export default Register;

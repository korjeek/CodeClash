// src/App.tsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Register from './components/Register';
import Login from './components/Login';

const App: React.FC = () => {
  return (
//     <div>
//             <Register />
//     </div>
//   );
        <Router>
        <nav>
            <ul>
                <li>
                    <Link to="/">App</Link>
                </li>
                <li>
                    <Link to="/register">Register</Link>
                </li>
                <li>
                    <Link to="/login">Log in</Link>
                </li>
            </ul>
        </nav>

        <Routes>
            <Route path="/" element={App} />
            <Route path="/register" element={<Register/>} />
            <Route path="/login" element={<Login/>} />
        </Routes>
        </Router>
    );
};

export default App;

// src/App.tsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Register from './components/Register';
import Home from './components/Home';
import Login from './components/Login';
import Auth from './components/Auth';
import Rooms from './components/Rooms';
import Page from './components/Page';
import HomePage from './components/HomePage';

const App: React.FC = () => {
    const rooms = [
        {key: 0, Name: "ABCD", host: "example@example.com", connected: 1, total: 10},
        {key: 1, Name: "My Room", host: "example@example.com", connected: 3, total: 5},
        {key: 2, Name: "Another Room", host: "example@example.com", connected: 5, total: 5},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2}
    ];
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Page navBarIndex={0}><Home/></Page>} />
                <Route path="/home" element={<Page navBarIndex={0}><Home/></Page>} />
                <Route path="/login" element={<Page><Auth><Login/></Auth></Page>} />
                <Route path="/register" element={<Page><Auth><Register/></Auth></Page>} />
                <Route path="/rooms" element={<Page navBarIndex={1}><Rooms Rooms={rooms}/></Page>} />
                <Route path="/ranks" element={<Page navBarIndex={2}></Page>} />
                <Route path="/test" element={<HomePage />} />
            </Routes>
        </Router>
    );
};

export default App;

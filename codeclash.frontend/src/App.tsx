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
import { CodePallete } from './components/CodePallete';
import RegisterPage from './components/Auth/RegisterPage.tsx';
import LoginPage from './components/Auth/LoginPage.tsx';
import CreateRoomPage from './components/CreateRoomPage';
import AuthPage from "./components/Auth/AuthPage.tsx";
import Menu from "./components/Menu/Menu.tsx";
import Lobby from "./components/Lobby/Lobby.tsx";
import CodeSpace from "./components/CodeSpace/CodeSpace.tsx";

const App: React.FC = () => {
    const rooms = [
        {key: 0, Name: "ABCD", host: "example@example.com", connected: 1, total: 10},
        {key: 1, Name: "My Room", host: "example@example.com", connected: 3, total: 5},
        {key: 2, Name: "Another Room", host: "example@example.com", connected: 5, total: 5},
        {key: 3, Name: "New Room", host: "example@example.com", connected: 1, total: 2},
        {key: 4, Name: "More Room", host: "example@example.com", connected: 5, total: 1},
    ];
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Page navBarIndex={0}><link rel="stylesheet" href="./src/style/Home.css"></link><Home/></Page>} />
                <Route path="/home" element={<Page navBarIndex={0}><link rel="stylesheet" href="./src/style/Home.css"></link><Home/></Page>} />
                <Route path="/login" element={<Page><Auth><Login/></Auth></Page>} />
                <Route path="/register" element={<Page><Auth><Register/></Auth></Page>} />
                <Route path="/rooms" element={<Page navBarIndex={1}><link rel="stylesheet" href="./src/style/Home.css"></link><Rooms/></Page>} />
                <Route path="/ranks" element={<Page navBarIndex={2}></Page>} />
                <Route path="/submit" element={<Page navBarIndex={-1}><CodePallete/></Page>} />
                <Route path="/test" element={<HomePage/>} />
                <Route path="/reg1" element={<AuthPage AuthPageElement={<RegisterPage/>} />} />
                <Route path="/login1" element={<AuthPage AuthPageElement={<LoginPage/>}/>} />
                <Route path="/menu" element={<Menu/>} />
                <Route path="/createRoom" element={<CreateRoomPage/>} />
                <Route path="/lobby" element={<Lobby/>} />
                <Route path="/problem/:param" element={<CodeSpace/>} />
            </Routes>
        </Router>
    );
};

export default App;

import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import StartPage from './components/StartPage/StartPage.tsx';
import RegisterPage from './components/AuthPages/RegisterPage.tsx';
import LoginPage from './components/AuthPages/LoginPage.tsx';
import CreateRoomPage from './components/CreateRoomPage/CreateRoomPage.tsx';
import AuthPage from "./components/AuthPages/AuthPage.tsx";
import HomePage from "./components/HomePage/HomePage.tsx";
import Lobby from "./components/LobbyPage/Lobby.tsx";
import CodeSpace from "./components/CodeSpace/CodeSpace.tsx";
import CompetitionResultPage from "./components/CompetitionResultPage.tsx";
import ProblemsPage from "./components/ProblemsPage/ProblemsPage.tsx";

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<StartPage/>} />
                <Route path="/reg" element={<AuthPage AuthPageElement={<RegisterPage/>} />} />
                <Route path="/login" element={<AuthPage AuthPageElement={<LoginPage/>}/>} />
                <Route path="/competitions" element={<HomePage/>} />
                <Route path="/create-competition" element={<CreateRoomPage/>} />
                <Route path="/lobby" element={<Lobby/>} />
                <Route path="/problem/:param" element={<CodeSpace/>} />
                <Route path="/competition/result" element={<CompetitionResultPage/>} />
                <Route path="/problems" element={<ProblemsPage/>} />
            </Routes>
        </Router>
    );
};

export default App;

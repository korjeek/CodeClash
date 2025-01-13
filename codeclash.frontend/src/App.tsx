import React from 'react';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
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
import PersistLogin from "./contexts/PersistLogin.tsx";

const App: React.FC = () => {
    return (
        <Router future={{
            v7_startTransition: true,
            v7_relativeSplatPath: true,
        }}>
            <Routes>
                {/* public routes */}
                <Route path="/" element={<StartPage/>} />
                <Route path="/reg" element={<AuthPage AuthPageElement={<RegisterPage/>} />} />
                <Route path="/login" element={<AuthPage AuthPageElement={<LoginPage/>}/>} />

                {/* auth routes */}
                <Route element={<PersistLogin />}>
                    <Route path="/competitions" element={<HomePage/>} />
                    <Route path="/create-competition" element={<CreateRoomPage/>} />
                    <Route path="/lobby" element={<Lobby/>} />
                    <Route path="/problem/:param" element={<CodeSpace/>} />
                    <Route path="/competition/result" element={<CompetitionResultPage/>} />
                    <Route path="/problems" element={<ProblemsPage/>} />
                </Route>
            </Routes>
        </Router>
    );
};

export default App;

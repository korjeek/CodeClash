import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {CompetitionDataProvider} from "./contexts/CompetitionContext.tsx";
import {AuthProvider} from "./contexts/AuthProvider.tsx";

createRoot(document.getElementById('root')!).render(
    <CompetitionDataProvider>
        <AuthProvider>
            <App />
        </AuthProvider>
    </CompetitionDataProvider>
)

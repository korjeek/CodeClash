import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {CompetitionDataProvider} from "./contexts/CompetitionContext.tsx";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <CompetitionDataProvider>
        <App />
      </CompetitionDataProvider>
  </StrictMode>,
)

import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {RoomDataProvider} from "./contexts/RoomContext.tsx";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <RoomDataProvider>
        <App />
      </RoomDataProvider>
  </StrictMode>,
)

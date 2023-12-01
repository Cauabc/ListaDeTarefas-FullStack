import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import AppRoutes from './routes/AppRoutes'


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AppRoutes/>
  </React.StrictMode>,
)

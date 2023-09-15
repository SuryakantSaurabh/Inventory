import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './Components/LoginPage';
import Navbar from './Components/Navbar';
import Dashboard from './Components/Dashboard';
import './App.css';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faBars, faTimes } from '@fortawesome/free-solid-svg-icons';
library.add(faBars, faTimes);

function App() {
  return (
    <>
    <Router>
      <Navbar />
      
        <Routes>
          <Route path="/" element={<LoginPage />} /> 
          <Route path="/dashboard/*" element={<Dashboard />} /> 
        </Routes>
      </Router>
    </>
  );
}

export default App;

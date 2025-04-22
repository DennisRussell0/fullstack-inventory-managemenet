import { useState } from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from "react-router-dom";
import './App.css'
import Header from './components/Header'
import Storage from './components/Storage'
import Orders from './components/Orders'

function App() {
  

  return (
    <>
      <Router>
      <Header />
        <Routes>
          <Route path="/" element={<Navigate to="/storage" replace/>}/>
          <Route path="/storage" element={<Storage/>}/>
          <Route path="/orders" element={<Orders/>}/>
        </Routes>
      </Router>
    </>    
   )
}

export default App

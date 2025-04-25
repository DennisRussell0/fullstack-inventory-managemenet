import { useState } from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from "react-router-dom";
import './App.css'
import Header from './components/Header'
import Storage from './components/Storage'
import Orders from './components/Orders'

function App() {
  

  return (
    <>
    <div className='h-screen w-screen flex flex-col bg-gray-100'>
      <Router>
        <Header />
        <div className="flex-1 overflow-y-auto">
          <Routes>
            <Route path="/" element={<Navigate to="/storage" replace/>}/>
            <Route path="/storage" element={<Storage/>}/>
            <Route path="/orders" element={<Orders/>}/>
          </Routes>  
        </div>
      </Router>
    </div>
    </>    

   )
}

export default App

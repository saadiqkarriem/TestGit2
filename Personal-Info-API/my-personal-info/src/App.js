import React from 'react';
import Home from "./components/Home";
import {
    Routes,
    BrowserRouter,
    Route,
    } from "react-router-dom";      
    function App() {  
    return (
        <BrowserRouter>  
                <Routes>
                    <Route exact path="/" Component={Home}/>                    
                </Routes>
        </BrowserRouter>
  );
 }
 export default App;
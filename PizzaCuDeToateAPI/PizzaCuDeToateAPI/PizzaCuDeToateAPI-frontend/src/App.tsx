import { Container } from 'react-bootstrap'
import { Routes, Route } from 'react-router-dom';
import { useState, useEffect } from 'react';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import { ThreeCircles } from 'react-loader-spinner';
import { GoogleOAuthProvider } from '@react-oauth/google';
import Pizza from './pages/Pizza';
import './App.css'
import EmailConfirmed from './pages/EmailConfirmed';
import Customize from './pages/Customize';

function App() {
  const [logged, setLogged] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  
  return (
    <>
      <GoogleOAuthProvider clientId="243357289852-s807hi6ior999l811crverov3lenloi7.apps.googleusercontent.com">
        <ThreeCircles
          height="100%"
          width="20%"
          color="crimson"
          wrapperStyle={{}}
          wrapperClass="threee-circles-wrapper"
          visible={isLoading}
          ariaLabel="three-circles-rotating"
          outerCircleColor=""
          innerCircleColor=""
          middleCircleColor=""
        />
        
        <Container fluid style={{ padding:"0", filter: isLoading ? "blur(10px)" : "none", pointerEvents: isLoading ? "none" : "auto" }}>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/register" element={<Register setSpinner={setIsLoading} loading={isLoading} logged={logged}/>} />
              <Route path="/login" element={<Login setSpinner={setIsLoading} loading={isLoading} logged={logged}/>} />
              <Route path="/emailConfirmed/:email/:username" element={<EmailConfirmed/>} />
              <Route path="/customize" element={<Customize setSpinner={setIsLoading} loading={isLoading} logged={logged}/>}/>
              <Route path="/menu" element={<Pizza/>}/>
            </Routes>
        </Container>
      </GoogleOAuthProvider>
    </>
  )
}

export default App

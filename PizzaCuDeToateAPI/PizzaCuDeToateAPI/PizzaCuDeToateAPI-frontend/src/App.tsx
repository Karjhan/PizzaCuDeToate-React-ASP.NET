import { Container } from 'react-bootstrap'
import Row from 'react-bootstrap/Row';
import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import './App.css'
import NavbarMain from './components/NavbarMain';
import { useState, useEffect } from 'react';
import ParticlesBackground from "./components/ParticlesBackground";

function App() {
  const [logged, setLogged] = useState(false);
  
  return (
    <>
      <Container fluid>
        <NavbarMain logged={logged} />
        {/* <ParticlesBackground/> */}
        <Row style={{ background: "linear-gradient(#E14242, #F7D098)" }}>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/register" element={<Register />} />
              <Route path="/login" element={<Login />} />
            </Routes>
        </Row>
      </Container>
    </>
  )
}

export default App

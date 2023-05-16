import { Container } from 'react-bootstrap'
import Row from 'react-bootstrap/Row';
import Col from "react-bootstrap/Col";
import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import './App.css'
import NavbarMain from './components/NavbarMain';
import { useState, useEffect } from 'react';

function App() {
  const [logged, setLogged] = useState(false);
  
  return (
    <>
      <Container fluid>
        <NavbarMain logged = {logged}/>
        <Row>
          <Col>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/register" element={<Register />} />
              <Route path="/login" element={<Login />} />
            </Routes>
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default App

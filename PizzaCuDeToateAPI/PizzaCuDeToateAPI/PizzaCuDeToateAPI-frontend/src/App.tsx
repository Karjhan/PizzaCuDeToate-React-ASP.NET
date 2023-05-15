import { Container } from 'react-bootstrap'
import Row from 'react-bootstrap/Row';
import Col from "react-bootstrap/Col";
import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import './App.css'

function App() {
  
  return (
    <>
      <Container fluid>
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

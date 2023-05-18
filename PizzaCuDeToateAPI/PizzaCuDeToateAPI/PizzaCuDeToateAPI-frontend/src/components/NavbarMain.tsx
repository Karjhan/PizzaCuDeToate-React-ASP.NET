import React from 'react'
import Row from 'react-bootstrap/Row';
import Col from "react-bootstrap/Col";
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Container } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import Button from 'react-bootstrap/Button';

const NavbarMain = (props: { logged: boolean; }) => {
  return (
    <Row>
      <Col style={{ padding: "0", width: "100%" }}>
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
          <Container>
            <Link to="/">
              <Navbar.Brand className="d-block d-lg-none">Home</Navbar.Brand>
            </Link>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="me-auto gap-1 w-100" id="main-nav">
                {!props.logged && <>
                  <Link to="/register">
                    <Nav.Link eventKey={1} as={Button} size="lg">Sign Up</Nav.Link>{' '}
                  </Link>
                  <Link to="/login">
                    <Nav.Link eventKey={2} as={Button} size="lg">Sign In</Nav.Link>{' '}
                  </Link>
                </>}
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </Col>
    </Row>
  )
}

export default NavbarMain

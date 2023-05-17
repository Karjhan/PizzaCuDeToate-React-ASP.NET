import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import PizzaNavBar2 from './PizzaNavBar2';

function PizzaNavBar(props: {logged:boolean; totalItems: number; totalPrice: number; }) {
  return (
    <>
    <Navbar collapseOnSelect expand="lg" bg="#DC0037" variant="dark" fixed='top'  style={{background:'#DC0037'}}>
      <Container>
        <Navbar.Brand href="#home">PizzaCuDeToate</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav" className="d-flex justify-content-between">
          <Nav className="me-auto">
            <Nav.Link href="#aboutUs">About Us</Nav.Link>
             {!props.logged && <>
                    <Nav.Link href="/register" >Sign Up</Nav.Link>
                    <Nav.Link href="/login" >Login</Nav.Link>
                    </>}
              </Nav>
             <Nav>       
            <NavDropdown  title="Your Cart is Empty" as={Button} variant="success" id="collasible-nav-dropdown">
              <NavDropdown.Item href="#action/3.1" className='text-center'>{props.totalItems} items  {props.totalPrice} RON</NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    </>
  );
}

export default PizzaNavBar;
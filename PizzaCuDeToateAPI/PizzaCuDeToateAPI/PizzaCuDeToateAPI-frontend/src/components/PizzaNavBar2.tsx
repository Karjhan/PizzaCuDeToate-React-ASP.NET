import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { IoPizzaOutline } from "react-icons/io5";
import { GiDonerKebab} from "react-icons/gi";
import { RiCake3Line } from "react-icons/ri";
import { TbBottle } from "react-icons/tb";
import NavDropdown from 'react-bootstrap/NavDropdown';

function PizzaNavBar2(){
return (
  <>
    <hr/>
      <Navbar collapseOnSelect expand="lg" bg="#DC0037" variant="dark" className="d-flex justify-content-between" style={{background:'#DC0037'}}>
      <Container>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav" className="d-flex justify-content-between">
          {/* <Nav className="me-auto">
            <IoPizzaOutline/>
              </Nav> */}
          <Nav className="me-auto">
            <Nav.Link href="#pizza"><IoPizzaOutline/> Pizza</Nav.Link>
              </Nav>
              <Nav className="me-auto">
            <Nav.Link href="#shawarma"><GiDonerKebab/>Shawarma</Nav.Link>
              </Nav>
              <Nav className="me-auto">
            <Nav.Link href="#desert"><RiCake3Line/>Desserts</Nav.Link>
              </Nav>
              <Nav className="me-auto">
            <Nav.Link href="#drink"><TbBottle/>Drinks</Nav.Link>
              </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    </>
    );
}

export default PizzaNavBar2
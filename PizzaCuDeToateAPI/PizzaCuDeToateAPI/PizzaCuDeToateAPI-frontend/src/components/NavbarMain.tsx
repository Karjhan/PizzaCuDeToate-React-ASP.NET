import Col from "react-bootstrap/Col";
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Container } from 'react-bootstrap';
import { motion } from "framer-motion";
import navbarLogo from '../images/navbarLogo.png'
import '../NavbarMain.css'

const NavbarMain = (props: { logged: boolean; }) => {

  return (
      <Col style={{ padding: "0", width: "100%" }}>
        <Navbar collapseOnSelect expand="lg" style={{ backgroundImage:"radial-gradient(circle, rgba(230,0,0,1) 70%, rgba(147,1,1,1) 100%)"}}>
          <Container>
            <Navbar.Brand className='d-flex align-items-center'>
            <img src={navbarLogo} alt="logo" style={{ width: "4.5rem", marginRight:"1rem" }} />
            <h2 style={{ fontFamily: "poppins", fontStyle: "italic", color: "#DFD3C3", textShadow: "0px 0px 8px black" }}>PizzaCuDeToate</h2>
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="me-auto gap-1 w-100" id="main-nav">
                <ul className="d-flex flex-lg-row flex-column ">
                  {(window.location.pathname != "/about") && <>
                    <li className="mt-0">
                      <motion.button style={{ minWidth: "9rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset" }} whileHover={{
                        textShadow: "0px 0px 8px white",
                      }}>
                        <a href="/about" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>About Us</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/menu") && <>
                    <li className="mt-0">
                      <motion.button style={{minWidth:"9rem", backgroundColor:"transparent", border:"0rem",textShadow:"unset"}} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/menu" style={{color: "#DFD3C3", fontFamily:"poppins", fontSize:"1.5rem"}}>Menu</a>
                      </motion.button>
                    </li>
                  </>} 
                  {(window.location.pathname != "/customize") && <>
                    <li className="mt-0">
                      <motion.button style={{ minWidth: "9rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset" }} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/customize" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>Customize</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/register" && !props.logged) && <>
                    <li className="mt-0">
                      <motion.button style={{ minWidth: "9rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset" }} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/register" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>Sign Up</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/login" && !props.logged) && <>
                    <li className="mt-0">
                      <motion.button style={{minWidth:"9rem", backgroundColor:"transparent", border:"0rem", textShadow:"unset"}} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/login" style={{color: "#DFD3C3", fontFamily:"poppins", fontSize:"1.5rem"}}>Sign In</a>
                      </motion.button>
                    </li>
                  </>}   
                </ul>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </Col>
  )
}

export default NavbarMain

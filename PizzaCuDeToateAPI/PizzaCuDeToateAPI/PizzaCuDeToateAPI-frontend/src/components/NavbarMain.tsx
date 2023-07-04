import Col from "react-bootstrap/Col";
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { motion } from "framer-motion";
import navbarLogo from '../images/navbarLogo.png';
import '../NavbarMain.css'
import { Container } from "react-bootstrap";
import { useState, useEffect, useCallback } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import Image from 'react-bootstrap/Image';
import { useNavigate } from "react-router";

const NavbarMain = (props: {setSpinner: (arg0: boolean) => void; loading: boolean; logged: object; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any; setLogged: any; notify: (arg0: string, arg1: boolean) => void;}) => {
  const [show, setShow] = useState(false);

  useEffect(() => {
    if(props.basket.length === 0){
      setShow(false);
    }
  }, [props.basket])
  
  const navigate = useNavigate();
  const handleGoOrder = useCallback(
    () => navigate("/order", { replace: true }),
    [navigate]
  );

  return (
      <Col style={{ padding: "0", width: "100%" }}>
        <Navbar collapseOnSelect expand="lg" style={{ backgroundImage:"radial-gradient(circle, rgba(230,0,0,1) 70%, rgba(147,1,1,1) 100%)" }}>
          <Container fluid className="p-lg-2 p-0">
            <Navbar.Brand className='d-flex align-items-center px-sm-4 px-3 mx-0'>
              <img src={navbarLogo} alt="logo" style={{ width: "4.5rem", marginRight:"1rem" }} />
            <h2 style={{ fontFamily: "poppins", fontStyle: "italic", color: "#DFD3C3", textShadow: "0px 0px 8px black" }}>PizzaCuDeToate</h2>
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" className="mx-sm-4 mx-3"/>
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="gap-1 w-100" id="main-nav">
                <ul className="d-flex flex-lg-row flex-column align-items-center w-100 px-lg-3 px-0" style={{maxWidth:"1000rem"}}>
                  {(window.location.pathname != "/about") && <>
                    <li className="mt-0" style={{flexBasis:"0%"}}>
                      <motion.button style={{ width: "8rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset", padding:"0.5rem" }} whileHover={{
                        textShadow: "0px 0px 8px white",
                      }}>
                        <a href="/about" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>About Us</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/menu") && <>
                    <li className="mt-0">
                      <motion.button style={{width:"8rem", backgroundColor:"transparent", border:"0rem",textShadow:"unset", padding:"0.5rem"}} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/menu" style={{color: "#DFD3C3", fontFamily:"poppins", fontSize:"1.5rem"}}>Menu</a>
                      </motion.button>
                    </li>
                  </>} 
                  {(window.location.pathname != "/customize") && <>
                    <li className="mt-0">
                      <motion.button style={{ width: "8rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset", padding:"0.5rem" }} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/customize" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>Customize</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/register" && (props.logged["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] === undefined)) && <>
                    <li className="mt-0">
                      <motion.button style={{ width: "8rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset", padding:"0.5rem" }} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/register" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem" }}>Sign Up</a>
                      </motion.button>
                    </li>
                  </>}
                  {(window.location.pathname != "/login" && (props.logged["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] === undefined)) && <>
                    <li className="mt-0">
                      <motion.button style={{width:"8rem", backgroundColor:"transparent", border:"0rem", textShadow:"unset", padding:"0.5rem"}} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }}>
                        <a href="/login" style={{color: "#DFD3C3", fontFamily:"poppins", fontSize:"1.5rem"}}>Sign In</a>
                      </motion.button>
                    </li>
                  </>}
                  {!(props.logged["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] === undefined) && <>
                    <li className="mt-0">
                      <motion.button style={{ width: "8rem", backgroundColor: "transparent", border: "0rem", textShadow: "unset", padding:"0.5rem" }} whileHover={{
                        textShadow: "0px 0px 8px white"
                      }} onClick={(e) => {props.setSpinner(true); setTimeout(() => {props.setLogged({}); props.setSpinner(false)},3000)}}>
                        <p style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.5rem", margin:"0px" }}>Logout</p>
                      </motion.button>
                    </li>
                  </>}
                  <li className="mt-lg-0 mt-3 nav-cart-element">
                    <motion.button className="d-flex align-items-center justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 1 }} onClick={(e) => { setShow(true); if(!(props.basket.length > 0)){props.notify("🍕 Oops, basket is empty...", true);}}}>
                      {props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] !== undefined && <span className="px-2" style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.3rem" }}>{`${props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]}`}</span>}
                      <i className="fa badge fa-lg" value={props.basket.length} style={{fontSize:"150%", margin:"0px"}}>&#xf290;</i>
                      <span style={{ color: "#DFD3C3", fontFamily: "poppins", fontSize: "1.3rem" }}>{`${props.basket.reduce((a,b) => {return a + b.amount*b.unitPrice},0)} RON`}</span>
                    </motion.button>
                    <Modal show={show && (props.basket.length > 0)} onHide={() => { setShow(false) }} backdrop="static" centered aria-labelledby="contained-modal-title-vcenter">
                      <Modal.Header closeButton style={{backgroundColor:"rgba(34,34,34,1)", border:"0px"}}>
                        <Modal.Title className="d-flex align-items-center">
                          <img src={navbarLogo} alt="logo" style={{ width: "2.5rem", marginRight:"1rem" }} />
                          <h5 style={{ fontFamily: "poppins", fontStyle: "italic", color: "#DFD3C3", textShadow: "0px 0px 8px red", margin:"0px" }}>YOUR ORDER</h5>
                        </Modal.Title>
                      </Modal.Header>
                      <Modal.Body style={{backgroundColor:"rgba(34,34,34,1)", border:"0px", borderTop:"2px dashed rgba(223,211,195,1)"}}>
                        {props.basket.map((item, index) => 
                          <div key={index} className="w-100 p-0 d-flex flex-row align-items-center my-2" style={{minHeight:"5rem !important"}}>
                            <Image src={item.logo} alt='logo' style={{ borderRadius: "1rem", border: "3px", borderColor: "black", borderStyle: "ridge", width:"25%" }} />
                            <p style={{color: "#DFD3C3", marginBottom:"0px", marginLeft:"1rem", marginRight:"1rem", fontSize:"0.95rem", fontFamily:"Copperplate", width:"40%"}}>{item.name.toUpperCase()}</p>  
                            <div className="d-flex flex-row justify-content-center" style={{ width:"30%", margin:"0px", padding:"0px"}}>
                              <motion.div whileTap={{ scale: 0.9 }} style={{width:"30%", minWidth:"35px"}}>
                                  <Button 
                                    style={{ borderRadius: "0px", fontWeight: "bold", backgroundColor: "#E64848", borderColor: "#E64848", width:"100%" }} 
                                    onClick={(event) => {let tempArray = [...props.basket];tempArray[index].amount -= 1;if(tempArray[index].amount === 0){tempArray.splice(index, 1);}props.setBasket(tempArray);}}
                                  >-</Button>
                              </motion.div>
                              <input
                                  type="text"
                                  value={item.amount}
                                  style={{backgroundColor:"white", color:"black", width:"40%", textAlign:"center", minWidth:"40px"}}
                              ></input>
                              <motion.div whileTap={{ scale: 0.9 }} style={{width:"30%", minWidth:"35px"}}>
                                  <Button 
                                    style={{ borderRadius: "0px", fontWeight: "bold", backgroundColor: "#2EB086", borderColor: "#2EB086", width:"100%" }} 
                                    onClick={(event) => {let tempArray = [...props.basket];tempArray[index].amount += 1;props.setBasket(tempArray);}}
                                  >+</Button>
                              </motion.div>                        
                            </div>
                          </div>
                        )}
                      </Modal.Body>
                      <Modal.Footer className="d-flex justify-content-center" style={{backgroundColor:"rgba(34,34,34,1)", borderTop:"2px dashed rgba(223,211,195,1)"}}>
                        <motion.button
                          whileHover={{ scale: 1.05 }}
                          whileTap={{ scale: 0.9 }}
                          className="modal-checkout-button"
                          onClick={(e) => { if (props.logged["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] === undefined || props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] === undefined){
                              props.notify("🍝 Please sign in for checkout...", true);
                            } else {
                              props.setSpinner(true); 
                              setTimeout(() => {setShow(false); handleGoOrder(); props.setSpinner(false)},3000)
                            }
                          }}
                        >
                          Checkout
                        </motion.button>
                      </Modal.Footer>
                    </Modal>
                  </li>   
                </ul>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </Col>
  )
}

export default NavbarMain
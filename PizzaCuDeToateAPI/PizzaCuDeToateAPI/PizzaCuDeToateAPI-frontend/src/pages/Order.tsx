import ParticlesBackground from "../components/ParticlesBackground";
import Footer from "../components/Footer";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import RestaurantInteriorCanvas from "../components/RestaurantInteriorCanvas";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { motion } from "framer-motion";
import { Image } from "react-bootstrap";
import { useState, useEffect, useCallback } from 'react';
import Carousel from 'react-bootstrap/Carousel';
import navbarLogo from '../images/navbarLogo.png';
import { useNavigate } from "react-router";

const Order = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; logged: object; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any; notify: (arg0: string, arg1: boolean) => void; }) => {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    address: "",
    phoneNumber: "",
    description: "",
    termsAgreed: false
  });

  const [isMobile, setIsMobile] = useState(false);
  useEffect(() => {
    const mediaQuery = window.matchMedia("(max-width: 576px)");
    setIsMobile(mediaQuery.matches);
    const handleMediaQueryChange = (event: { matches: boolean | ((prevState: boolean) => boolean); }) => {
      setIsMobile(event.matches);
    };
    mediaQuery.addEventListener("change", handleMediaQueryChange);
    return () => {
      mediaQuery.removeEventListener("change", handleMediaQueryChange);
    };
  }, []);

  const handleCheckout = async () => {
    props.setSpinner(true);
    let customerId : string;
    let invoiceId : string;
    const findCustomerResponse = await fetch(`https://localhost:44388/api/stripe/customer/name=${formData.firstName + formData.lastName}&email=${formData.email}`);
    const findCustomerData = await findCustomerResponse.json();
    if (findCustomerData.error) {
      const createdCustomerResponse = await fetch("https://localhost:44388/api/stripe/customer", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*"
        },
        body: JSON.stringify({
          name: formData.firstName + formData.lastName,
          email: formData.email,
          creditCard: {
            name: formData.firstName + formData.lastName,
            expirationYear: "2024",
            expirationMonth: "12",
            cvc: "999",
            cardNumber: "4242424242424242"
          }
        }),
      });
      const createdCustomerData = await createdCustomerResponse.json();
      customerId = createdCustomerData.customerId;
    } else {
      customerId = findCustomerData.customerId;
    }
    const createdInvoiceResponse = await fetch("https://localhost:44388/api/stripe/invoice", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      },
      body: JSON.stringify({
        customerId: customerId,
        address: formData.address,
        phone: formData.phoneNumber,
        description: formData.description,
        firstName: formData.firstName,
        lastName: formData.lastName,
        appUserName: props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        appUserEmail: props.logged["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
      }),
    });
    const createdInvoiceData = await createdInvoiceResponse.json();
    invoiceId = createdInvoiceData.invoiceId;
    await Promise.all(props.basket.map((item, index) => fetch("https://localhost:44388/api/stripe/invoiceItem", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      },
      body: JSON.stringify({
        customerId: customerId,
        invoiceId: invoiceId,
        currency: "RON",
        discountable: false,
        foodItemName: item.name,
        quantity: item.amount
      }),
    })));
    const finalizedInvoiceResponse = await fetch(`https://localhost:44388/api/stripe/invoice/finalize/id=${invoiceId}`);
    const finalizedInvoiceData = await finalizedInvoiceResponse.json();
    window.location.replace(finalizedInvoiceData.hostedStringURL);
    props.setSpinner(false);
  };

  const navigate = useNavigate();
  const handleGoMenu = useCallback(
    () => navigate("/menu", { replace: true }),
    [navigate]
  );

  const changeCredential = function (field: string, credential: string): void {
    switch (field) {
      case "firstname":
        setFormData({
          firstName: credential,
          email: formData.email,
          lastName: formData.lastName,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          description: formData.description,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "lastname":
        setFormData({
          firstName: formData.firstName,
          email: formData.email,
          lastName: credential,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          description: formData.description,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "email":
        setFormData({
          firstName: formData.firstName,
          email: credential,
          lastName: formData.lastName,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          description: formData.description,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "address":
        setFormData({
          firstName: formData.firstName,
          email: formData.email,
          lastName: formData.lastName,
          address: credential,
          phoneNumber: formData.phoneNumber,
          description: formData.description,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "phoneNumber":
        setFormData({
          firstName: formData.firstName,
          email: formData.email,
          lastName: formData.lastName,
          address: formData.address,
          phoneNumber: credential,
          description: formData.description,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "description":
        setFormData({
          firstName: formData.firstName,
          email: formData.email,
          lastName: formData.lastName,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          description: credential,
          termsAgreed: formData.termsAgreed
        })
        break;
      case "termsAgreed":
        setFormData({
          firstName: formData.firstName,
          email: formData.email,
          lastName: formData.lastName,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          description: formData.description,
          termsAgreed: !formData.termsAgreed
        })
        break;
    }
  }

  return (
    <>
      <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
      <Row
        style={{ position: "fixed", top: "0", left: "0", width: "100%", padding: "0", margin: "0", boxSizing: "border-box", height:"100%" }}
      >
        <Col className="p-0">
          <RestaurantInteriorCanvas/>
        </Col>
      </Row>
      <Row>
        <Col xl={{ span: 6, offset: 3 }} lg={{ span: 8, offset: 2 }} md={{ span: 10, offset: 1 }} xs={{ span: 10, offset: 1 }} style={{ zIndex:"10", marginTop:"5rem", marginBottom:"5rem", padding:"0px" }}>
          <motion.div animate={{ scale: 1 }} initial={{ scale: 0 }} transition={{ delay: 0.5, duration: 1 }} className="align-self-center" id="col-small-format">
            <div className="d-flex align-items-center justify-content-between p-3" style={{ backgroundImage: "radial-gradient(circle, rgba(230,0,0,0.8) 70%, rgba(147,1,1,0.8) 100%)" }}>
              <div className="d-flex align-items-center">
                <img src={navbarLogo} alt="logo" style={{ width: "3rem", marginRight: "1rem" }} />
                <h5 style={{ fontFamily: "poppins", fontStyle: "italic", color: "#DFD3C3", textShadow: "0px 0px 8px red", margin: "0px" }} className="d-sm-block d-none">YOUR ORDER</h5>
              </div>
              <motion.button whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }} style={{fontSize: `${isMobile ? "0.85rem" : "1rem"}`}} className="go-back-button" onClick={(e) => { props.setSpinner(true); setTimeout(() => { handleGoMenu(); props.setSpinner(false) }, 1500) }}>
                🌯 Hungry? Go back for more 🍕
              </motion.button>
            </div>
            <Card
              border="danger"
              bg="dark"
              text="white"
              style={{
                border: "1rem",
                paddingTop: "1rem",
                paddingBottom: "1rem",
              }}
              className="d-flex flex-md-row flex-column mt-sm-0 mt-0 mb-sm-5 mb-0 p-0"
            >
              <Card.Body className="w-md-50 w-100" >
                <div className="d-flex flex-column p-0 m-0 justify-content-center">
                  {isMobile ?
                    <Carousel style={{ width: "100%" }} indicators={false}>
                      {props.basket.map((item, index) =>
                        <Carousel.Item key={index}>
                          <div className="d-flex flex-column justify-content-center align-items-center">
                            <Image src={item.logo} alt='logo' style={{ borderRadius: "1rem", border: "3px", borderColor: "black", borderStyle: "ridge", width: "70%" }} />
                            <p style={{ color: "#DFD3C3", fontSize: "1rem", fontFamily: "Copperplate", width: "100%", flexShrink: "0" }} className="mx-0 my-3">{item.name.toUpperCase()}</p>
                            <div className="d-flex flex-row justify-content-center" style={{ width: "75%", margin: "0px", padding: "0px" }}>
                              <motion.div
                                whileTap={{ scale: 0.9 }}
                                style={{ width: "30%", borderRadius: "0px", fontWeight: "bold", backgroundColor: "#E64848", borderColor: "#E64848", padding: "0.5rem", cursor: "pointer" }}
                                onClick={(event) => { let tempArray = [...props.basket]; tempArray[index].amount -= 1; if (tempArray[index].amount === 0) { tempArray.splice(index, 1); } props.setBasket(tempArray); }}
                              >
                                -
                              </motion.div>
                              <input
                                type="text"
                                value={item.amount}
                                style={{ backgroundColor: "white", color: "black", width: "40%", textAlign: "center" }}
                              ></input>
                              <motion.div
                                whileTap={{ scale: 0.9 }}
                                style={{ width: "30%", borderRadius: "0px", fontWeight: "bold", backgroundColor: "#2EB086", borderColor: "#2EB086", padding: "0.5rem", cursor: "pointer" }}
                                onClick={(event) => { let tempArray = [...props.basket]; tempArray[index].amount += 1; props.setBasket(tempArray); }}
                              >
                                +
                              </motion.div>
                            </div>
                            <p className="mx-0 my-3" style={{ color: "#DFD3C3", marginBottom: "0px", marginLeft: "0.5rem", marginRight: "0", fontSize: "1rem", fontFamily: "Copperplate", maxWidth: "30%" }}>{`${item.amount * item.unitPrice} RON`}</p>
                          </div>
                        </Carousel.Item>
                      )}
                    </Carousel>
                    :
                    <>
                      {props.basket.map((item, index) =>
                        <div key={index} className="w-100 p-0 d-flex flex-row align-items-center justify-content-between my-2" style={{ minHeight: "5rem !important" }}>
                          <Image src={item.logo} alt='logo' style={{ borderRadius: "1rem", border: "3px", borderColor: "black", borderStyle: "ridge", width: "25%" }} className="smallHidden" />
                          <div style={{ width: "35%", maxWidth: "35%", flexShrink: "0" }}>
                            <p style={{ color: "#DFD3C3", marginBottom: "0px", fontSize: "0.9rem", fontFamily: "Copperplate", width: "100%" }}>{item.name.toUpperCase()}</p>
                          </div>
                          <div className="d-flex flex-row justify-content-center" style={{ width: "30%", margin: "0px", padding: "0px" }}>
                            <motion.div
                              whileTap={{ scale: 0.9 }}
                              style={{ width: "30%", borderRadius: "0px", fontWeight: "bold", backgroundColor: "#E64848", borderColor: "#E64848", padding: "0.5rem", cursor: "pointer" }}
                              onClick={(event) => { let tempArray = [...props.basket]; tempArray[index].amount -= 1; if (tempArray[index].amount === 0) { tempArray.splice(index, 1); } props.setBasket(tempArray); }}
                            >
                              -
                            </motion.div>
                            <input
                              type="text"
                              value={item.amount}
                              style={{ backgroundColor: "white", color: "black", width: "40%", textAlign: "center" }}
                            ></input>
                            <motion.div
                              whileTap={{ scale: 0.9 }}
                              style={{ width: "30%", borderRadius: "0px", fontWeight: "bold", backgroundColor: "#2EB086", borderColor: "#2EB086", padding: "0.5rem", cursor: "pointer" }}
                              onClick={(event) => { let tempArray = [...props.basket]; tempArray[index].amount += 1; props.setBasket(tempArray); }}
                            >
                              +
                            </motion.div>
                          </div>
                          <p style={{ color: "#DFD3C3", marginBottom: "0px", marginLeft: "0.5rem", marginRight: "0", fontSize: "1rem", fontFamily: "Copperplate", width: "15%", maxWidth: "30%" }}>{`${item.amount * item.unitPrice} RON`}</p>
                        </div>
                      )}
                    </>
                  }
                  <div style={{ borderTop: "2px dashed rgba(223,211,195,1)", marginTop: "1rem", paddingTop:"1rem" }} className="d-flex justify-content-between">
                    <p style={{ color: "#DFD3C3", marginBottom: "0px", marginLeft: "0.5rem", marginRight: "0", fontSize: "1rem", fontFamily: "Copperplate", width: "50%", textAlign: "left" }}>TOTAL: </p>
                    <p style={{ color: "#DFD3C3", marginBottom: "0px", marginLeft: "0.5rem", marginRight: "0", fontSize: "1rem", fontFamily: "Copperplate", width: "50%", textAlign: "right" }}>{`${props.basket.reduce((a, b) => { return a + b.amount * b.unitPrice }, 0)} RON`}</p>
                  </div>
                </div>
              </Card.Body>
              <Card.Body className="w-md-50 w-100">
                <Form className='d-flex flex-column' style={{ color: "#DFD3C3" }}>
                  <Form.Group className="mb-3" controlId="formBasicFirstName">
                    <Form.Label>Firstname</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        onChange={(event) =>
                          changeCredential("firstname", event.target.value)
                        }
                        type="text"
                        placeholder="Enter your firstname"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      Only your firstname, please.
                    </Form.Text>
                  </Form.Group>
                  <Form.Group className="mb-3" controlId="formBasicLastName">
                    <Form.Label>Lastname</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        onChange={(event) =>
                          changeCredential("lastname", event.target.value)
                        }
                        type="text"
                        placeholder="Enter your lastname"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      One minimum lastname, please.
                    </Form.Text>
                  </Form.Group>
                  <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Email</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        onChange={(event) =>
                          changeCredential("email", event.target.value)
                        }
                        type="email"
                        placeholder="Enter email"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      Email required for receipt.
                    </Form.Text>
                  </Form.Group>
                  <Form.Group className="mb-3" controlId="formBasicAddress">
                    <Form.Label>Address</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        as="textarea"
                        style={{ resize: "none", height: "9.25em" }}
                        onChange={(event) =>
                          changeCredential("address", event.target.value)
                        }
                        type="text"
                        placeholder="Enter address"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      Town/County, Street, Street Nr, Residence, Postal Code
                    </Form.Text>
                  </Form.Group>
                  <Form.Group className="mb-3" controlId="formBasicPhoneNumber">
                    <Form.Label>Phone Number</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        onChange={(event) =>
                          changeCredential("phoneNumber", event.target.value)
                        }
                        type="text"
                        placeholder="Enter phone number"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      Your phone number for delivery calls
                    </Form.Text>
                  </Form.Group>
                  <Form.Group className="mb-3" controlId="formBasicDescription">
                    <Form.Label>Description</Form.Label>
                    <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Form.Control
                        as="textarea"
                        style={{ resize: "none", height: "9.25em" }}
                        onChange={(event) =>
                          changeCredential("description", event.target.value)
                        }
                        type="text"
                        placeholder="Enter description"
                      />
                    </motion.div>
                    <Form.Text className="text-muted">
                      Mention anything you feel like and we'll get it.
                    </Form.Text>
                  </Form.Group>
                  <div className="d-flex justify-content-center">
                    <Form.Check
                      type="switch"
                      id="custom-switch"
                      style={{marginRight:"0.2rem"}}
                      onChange={(event) => {changeCredential("termsAgreed", "checked")}}
                    />
                    <p style={{textAlign:"left", fontSize:"0.9rem"}}>I agree with the <a style={{textDecoration:"underline", cursor:"pointer"}}>terms and conditions</a></p>
                  </div>
                  <motion.div className="d-flex justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                    <Button
                      variant="primary"
                      type="submit"
                      style={{ marginTop: "1rem", width: "100%", color: "#DFD3C3" }}
                      className="modal-checkout-button"
                      disabled={!formData.termsAgreed}
                      onClick={(event) => {event.preventDefault(); formData.termsAgreed ? handleCheckout() : props.notify("Accept terms and agreement 🚫", true)}}
                    >
                      Checkout
                    </Button>
                  </motion.div>
                </Form>
              </Card.Body>
            </Card>
          </motion.div>
        </Col>
      </Row>
      <Row>
        <Footer />
      </Row>
    </>
  )
}

export default Order;
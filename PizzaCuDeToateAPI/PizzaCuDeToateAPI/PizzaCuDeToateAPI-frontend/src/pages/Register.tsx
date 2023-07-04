import { useState, useEffect, useCallback } from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import formLogo from "../images/registerFormLogo.png"
import googleLogo from "../images/logoGoogle.png"
import registerSuccessLogo from "../images/registerSuccessTick.gif"
import { motion, useAnimationControls } from "framer-motion"
import PizzaCanvas from '../components/PizzaCanvas';
import KebabSaladCanvas from '../components/KebabSaladCanvas';
import HorizLineWithText from '../components/HorizLineWithText';
import { useNavigate } from "react-router-dom";
import { useGoogleLogin } from '@react-oauth/google';
import ParticlesBackground from "../components/ParticlesBackground";
import NavbarMain from "../components/NavbarMain";
import Footer from "../components/Footer";

const Register = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; logged: object; setLogged: any; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any; notify: (arg0: string, arg1: boolean) => void; }) => {
  const [statusMessage, setStatusMessage] = useState("");
  const [successStatus, setSuccessStatus] = useState(false);
  const [formData, setFormData] = useState({
    password: "",
    confirmedPassword: "",
    email: "",
    username: "",
    address: "",
    phoneNumber: ""
  });
  const [logoAngle, setLogoAngle] = useState(0);
  const [timer, setTimer] = useState(10);
  
  const navigate = useNavigate();
  const handleGoHome = useCallback(
    () => navigate("/", { replace: true }),
    [navigate]
  );

  const reset = function () : void {
    setFormData({
      password: "",
      confirmedPassword: "",
      email: "",
      username: "",
      address: "",
      phoneNumber: ""
    });
    setLogoAngle(0);
    setStatusMessage("");
    setTimer(10);
  }

  const changeCredential = function (field: string, credential: string) : void{
    switch (field) {
      case "username":
        setFormData({
          username: credential,
          email: formData.email,
          password: formData.password,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          confirmedPassword: formData.confirmedPassword
        })
        setLogoAngle(logoAngle + 10);
        break;
      case "password":
        setFormData({
          username: formData.username,
          email: formData.email,
          password: credential,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          confirmedPassword: formData.confirmedPassword
        })
        setLogoAngle(logoAngle + 10);
        break;
      case "email":
        setFormData({
          username: formData.username,
          email: credential,
          password: formData.password,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          confirmedPassword: formData.confirmedPassword
        })
        setLogoAngle(logoAngle + 10);
        break;
      case "address":
        setFormData({
          username: formData.username,
          email: formData.email,
          password: formData.password,
          address: credential,
          phoneNumber: formData.phoneNumber,
          confirmedPassword: formData.confirmedPassword
        })
        setLogoAngle(logoAngle + 10);
        break;
      case "phoneNumber":
        setFormData({
          username: formData.username,
          email: formData.email,
          password: formData.password,
          address: formData.address,
          phoneNumber: credential,
          confirmedPassword: formData.confirmedPassword
        })
        setLogoAngle(logoAngle + 10);
        break;
      case "confirmedPassword":
        setFormData({
          username: formData.username,
          email: formData.email,
          password: formData.password,
          address: formData.address,
          phoneNumber: formData.phoneNumber,
          confirmedPassword: credential
        })
        setLogoAngle(logoAngle + 10);
        break;
    }
  }

  const handleRegister = function (event: any): any{
    event.preventDefault();
    props.setSpinner(true);
    if (formData.username.length < 3) {
      setStatusMessage("Username too short: at least 3 characters!");
      props.setSpinner(false);
      return;
    }
    if (formData.password !== formData.confirmedPassword) {
      setStatusMessage("Passwords don't match!");
      props.setSpinner(false);
      return;
    }
    setStatusMessage("");
    fetch("https://localhost:44388/api/auth/register/User", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      },
      body: JSON.stringify({
        username: formData.username,
        email: formData.email,
        password: formData.password,
        address: formData.address,
        phoneNumber: formData.phoneNumber
      }),
    })
    .then((response) => response.json())
      .then((data) => {
        if (Object.prototype.toString.call(data) === '[object Array]') {
          setStatusMessage(data.map((item: { description: any; }) => item.description).join("\r\n"));
        } else if ("errors" in data) {
          setStatusMessage(Object.keys(data.errors).map((key) => data.errors[key][0]).join("\r\n"));
        } else if ("error" in data) {
          setStatusMessage(data.error);
        } else if ("success" in data) {
          setStatusMessage(data.success);
          setSuccessStatus(true);
        }
        props.setSpinner(false);
    })
  }

  const googleRegister = useGoogleLogin({
    onSuccess: tokenResponse => {
      props.setSpinner(true)
      fetch(`https://localhost:44388/api/auth/register/google/${tokenResponse.access_token}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (Object.prototype.toString.call(data) === '[object Array]') {
          setStatusMessage(data.map((item: { description: any; }) => item.description).join("\r\n"));
        } else if ("errors" in data) {
          setStatusMessage(Object.keys(data.errors).map((key) => data.errors[key][0]).join("\r\n"));
        } else if ("error" in data) {
          setStatusMessage(data.error);
        } else if ("success" in data) {
          setStatusMessage(data.success);
          setFormData({
            username: formData.username,
            email: data.email,
            password: formData.password,
            address: formData.address,
            phoneNumber: formData.phoneNumber,
            confirmedPassword: formData.confirmedPassword
          })
          setSuccessStatus(true);
        }
        props.setSpinner(false);
      })
    },
  });

  useEffect(() => {
    if (successStatus && timer > 0) {
      const interval = setInterval(() => setTimer(timer - 1), 1000);

      return () => clearInterval(interval);
    } else if (successStatus && timer === 0) {
      reset();
      handleGoHome();
    }
  }, [successStatus,timer]);

  const controls = useAnimationControls();
  useEffect(() => {
    controls.start({ rotate: logoAngle, transition:{repeat:0} })
  }, [formData])

  return (
    <>
      <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
      <Row>
        <NavbarMain logged={props.logged} basket={props.basket} setBasket={props.setBasket} setLogged={props.setLogged} setSpinner={props.setSpinner} loading={props.loading} notify={props.notify}/>
      </Row>
      <Row>
        <Col className="p-0 d-none d-xl-block" lg={{ span: 4, offset: 4 }} xl={{ span: 3, offset: 0 }} xxl={{ span: 4, offset: 0 }}>
          <PizzaCanvas />
        </Col>
        <Col xxl={{ span: 4, offset: 0 }} xl={{ span: 6, offset: 0 }} lg={{ span: 6, offset: 3 }} xs={{ span: 10, offset: 1 }} className="d-flex justify-content-center px-lg-0 px-4" id="col-small-format">
          <motion.div animate={{ scale: 1 }} initial={{ scale: 0 }} transition={{ delay: 0.5, duration: 1 }} className="align-self-center" id="col-small-format">
            <Card
              border="danger"
              bg="dark"
              text="white"
              style={{
                border: "1rem",
                paddingTop: "1rem",
                paddingBottom: "1rem",
              }}
              className="d-flex flex-column mt-sm-5 mt-0 mb-sm-5 mb-0"
            >
              {successStatus && <>
                <Card.Body>
                  <p>{`Redirecting you to home in ${timer} seconds...`}</p>
                  <Card.Img
                    variant="top"
                    src={registerSuccessLogo}
                    style={{ width: "50%" }}
                  />
                  <Card.Title style={{ textAlign: "center", marginBottom: "2rem", marginTop: "2rem" }}>
                    <h3>User created successfully</h3>
                  </Card.Title>
                  <p>Confirmation email sent to <i>{formData.email}</i></p>
                </Card.Body>
              </>}
              {!successStatus && <>
                <motion.div animate={controls}>
                  <Card.Img
                    variant="top"
                    src={formLogo}
                    style={{ width: "50%" }}
                  />
                </motion.div>
                <Card.Body>
                  <Card.Title style={{ textAlign: "center", marginBottom: "2rem", color: "#DFD3C3" }}>
                    <h2>SignUp Form</h2>
                  </Card.Title>
                  <Form className='d-flex flex-column' style={{ color: "#DFD3C3" }}>

                    <div className='d-flex flex-md-row flex-column justify-content-around'>
                      <div className="px-2">
                        <Form.Group className="mb-3" controlId="formBasicUsername">
                          <Form.Label>Username</Form.Label>
                          <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                            <Form.Control
                              onChange={(event) =>
                                changeCredential("username", event.target.value)
                              }
                              type="text"
                              placeholder="Enter username"
                            />
                          </motion.div>
                          <Form.Text className="text-muted">
                            Choose your prefered username.
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
                            We'll never share your email with anyone else.
                          </Form.Text>
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicPassword">
                          <Form.Label>Password</Form.Label>
                          <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                            <Form.Control
                              onChange={(event) =>
                                changeCredential("password", event.target.value)
                              }
                              type="password"
                              placeholder="Enter password"
                            />
                          </motion.div>
                          <Form.Text className="text-muted">
                            At least: 1 uppercase, 1 alphanumeric and 1 digit.
                          </Form.Text>
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicConfirmPassword">
                          <Form.Label>Confirm Password</Form.Label>
                          <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                            <Form.Control
                              onChange={(event) =>
                                changeCredential("confirmedPassword", event.target.value)
                              }
                              type="password"
                              placeholder="Confirm password"
                            />
                          </motion.div>
                          <Form.Text className="text-muted">
                            Confirm previous password
                          </Form.Text>
                        </Form.Group>
                      </div>

                      <div className="px-2">
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
                      </div>
                    </div>
                    <motion.div className="d-flex justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Button variant="primary" type="submit" style={{ marginTop: "1rem", width: "100%", color: "#DFD3C3" }} onClick={(event) => handleRegister(event)}>
                        Sign Up
                      </Button>
                    </motion.div>
                    <HorizLineWithText text={"Or"} />
                    <motion.div className="d-flex justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Button variant="danger" type="submit" style={{ marginTop: "1rem", width: "100%", color: "#DFD3C3" }} onClick={(event) => { event.preventDefault(); googleRegister(); }}>
                        <img src={googleLogo} alt="" style={{ width: "1.5rem", marginRight: "0.5rem" }}></img>
                        Register with Google
                      </Button>
                    </motion.div>
                  </Form>
                  {statusMessage !== "" && <p className="mt-4" style={{ whiteSpace: "pre-wrap", color: "red" }}>{statusMessage}</p>}
                </Card.Body>
              </>}
            </Card>
          </motion.div>
        </Col>
        <Col className="p-0 d-none d-xl-block" lg={{ span: 4, offset: 4 }} xl={{ span: 3, offset: 0 }} xxl={{ span: 4, offset: 0 }}>
          <KebabSaladCanvas />
        </Col>
      </Row>
      <Row>
        <Footer />                      
      </Row>
    </>
  )
}

export default Register

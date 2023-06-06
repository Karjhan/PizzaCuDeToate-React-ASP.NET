import { useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useGoogleLogin } from '@react-oauth/google';
import { motion } from "framer-motion"
import jwtDecode from "jwt-decode";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import googleLogo from "../images/logoGoogle.png"
import logoList from "../scripts/signin_logo_list";
import CokeDrinkCanvas from '../components/CokeDrinkCanvas';
import DessertCanvas from '../components/DessertCanvas';
import ParticlesBackground from "../components/ParticlesBackground";
import HorizLineWithText from '../components/HorizLineWithText';
import registerSuccessLogo from "../images/registerSuccessTick.gif"
import NavbarMain from "../components/NavbarMain";
import Footer from "../components/Footer";

const Login = (props: { setSpinner: (arg0: boolean) => void; loading: boolean }) => {
  const [statusMessage, setStatusMessage] = useState("");
  const [successStatus, setSuccessStatus] = useState(false);
  const [formData, setFormData] = useState({
    password: "",
    email: "",
  });
  const [logoOrder, setLogoOrder] = useState(-1);
  const [timer, setTimer] = useState(5);

  const navigate = useNavigate();
  const handleGoMenu = useCallback(
    () => navigate("/menu", { replace: true }),
    [navigate]
  );

  const reset = function (): void {
    setFormData({
      password: "",
      email: ""
    });
    setStatusMessage("");
    setTimer(5);
  }

  const changeCredential = function (field: string, credential: string): void{
    switch (field) {
      case "password":
        setFormData({
          email: formData.email,
          password: credential
        })
        break;
      case "email":
        setFormData({
          email: credential,
          password: formData.password
        })
        break;
    }
  }

  const handleLogin = function (event: any): any{
    event.preventDefault();
    props.setSpinner(true);
    setStatusMessage("");
    fetch("https://localhost:44388/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      },
      body: JSON.stringify({
        email: formData.email,
        password: formData.password
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data)
        if ("error" in data) {
          setStatusMessage(data.error);
        } else if ("errors" in data) {
          setStatusMessage("User doesn't exist, or invalid credentials!");
        } else if ("token" in data) {
          setSuccessStatus(true);
          localStorage.setItem("jwt", data.token)
          var decoded = jwtDecode(data.token);
          setStatusMessage(`Greetings ${decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].toLowerCase()} ${decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]} !`)
        }
        props.setSpinner(false);
      })
  }

  const googleLogin = useGoogleLogin({
    onSuccess: tokenResponse => {
      props.setSpinner(true)
      fetch(`https://localhost:44388/api/auth/login/google/${tokenResponse.access_token}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*"
        },
      })
        .then((response) => response.json())
        .then((data) => {
          console.log(data);
          if ("error" in data) {
            setStatusMessage(data.error);
          } else if ("token" in data) {
            setSuccessStatus(true);
            localStorage.setItem("jwt", data.token)
            var decoded = jwtDecode(data.token);
            setStatusMessage(`Greetings ${decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].toLowerCase()} ${decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]} !`)
          }
          props.setSpinner(false);
        })
    }
  });

  useEffect(() => {
    if (successStatus && timer > 0) {
      const interval = setInterval(() => setTimer(timer - 1), 1000);

      return () => clearInterval(interval);
    } else if (successStatus && timer === 0) {
      reset();
      handleGoMenu();
    }
  }, [successStatus, timer]);

  useEffect(() => {
    setLogoOrder((logoOrder + 1)%logoList.length)
  }, [formData])

  return (
    <>
      <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
      <Row>
        <NavbarMain />
      </Row>
      <Row>
        <Col className="p-0 d-none d-xl-block" lg={{ span: 4, offset: 4 }} xl={{ span: 3, offset: 0 }} xxl={{ span: 4, offset: 0 }}>
          <CokeDrinkCanvas />
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
                  <p>{`Redirecting you to menu page in ${timer} seconds...`}</p>
                  <Card.Img
                    variant="top"
                    src={registerSuccessLogo}
                    style={{ width: "50%" }}
                  />
                  <Card.Title style={{ textAlign: "center", marginBottom: "2rem", marginTop: "2rem" }}>
                    <h3>{statusMessage}</h3>
                  </Card.Title>
                </Card.Body>
              </>}
              {!successStatus && <>
                <Card.Img
                  variant="top"
                  src={logoList[logoOrder]}
                  style={{ marginLeft:"25%", width: "50%", transform:"rotate(120deg) scale(1.4)"}}
                />
                <Card.Body>
                  <Card.Title style={{ textAlign: "center", marginBottom: "2rem" }}>
                    <h2>SignIn Form</h2>
                  </Card.Title>
                  <Form className='d-flex flex-column'>
                      <div className="px-2">
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
                      </div>
                    <motion.div className="d-flex justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Button variant="primary" type="submit" style={{ marginTop: "1rem", width: "100%" }} onClick={(event) => handleLogin(event)}>
                        Sign In
                      </Button>
                    </motion.div>
                    <HorizLineWithText text={"Or"} />
                    <motion.div className="d-flex justify-content-center" whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                      <Button variant="danger" type="submit" style={{ marginTop: "1rem", width: "100%" }} onClick={(event) => { event.preventDefault(); googleLogin(); }}>
                        <img src={googleLogo} alt="" style={{ width: "1.5rem", marginRight: "0.5rem" }}></img>
                        Login with Google
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
          <DessertCanvas />
        </Col>
      </Row>
      <Row>
        <Footer />
      </Row>
    </>
  )
}

export default Login

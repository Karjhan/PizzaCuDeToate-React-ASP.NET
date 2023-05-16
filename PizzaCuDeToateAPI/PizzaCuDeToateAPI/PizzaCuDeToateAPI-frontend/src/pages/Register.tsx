import React from 'react'
import { useState, useEffect } from "react";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { Link } from "react-router-dom";
import logo1 from "../images/registerloginLogo1.png"
import { motion, useAnimationControls } from "framer-motion"
import { Container } from 'react-bootstrap';
import PizzaCanvas from '../components/PizzaCanvas';
import KebabSaladCanvas from '../components/KebabSaladCanvas';

const Register = () => {
  const [formData, setFormData] = useState({
    password: "",
    confirmedPassword: "",
    email: "",
    username: "",
    address: "",
    phoneNumber: ""
  });
  const [logoAngle, setLogoAngle] = useState(0);

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
    .then((data) => console.log(data))
  }

  const controls = useAnimationControls();
  useEffect(() => {
    controls.start({ rotate: logoAngle, transition:{repeat:0} })
    console.log("ceva")
  }, [formData])

  return (
    <>
      <Col className="p-0 d-none d-xl-block" lg={{ span: 4, offset: 4 }} xl={{ span: 3, offset: 0 }} xxl={{ span: 4, offset: 0 }}>
        <PizzaCanvas/>
      </Col>
      <Col xxl={{ span: 4, offset: 0 }} xl={{ span: 6, offset: 0 }} lg={{ span: 6, offset: 3 }} xs={{ span: 10, offset: 1 }} className="px-lg-0 px-4">
        <motion.div animate={{ scale: 1 }} initial={{ scale: 0 }} transition={{ delay: 0.5, duration: 1}}>
          <Card
            border="danger"
            bg="dark"
            text="white"
            style={{
              marginTop: "2rem",
              marginBottom: "2rem",
              border: "1rem",
              paddingTop: "1rem",
              paddingBottom: "1rem",
            }}
            className="d-flex flex-column"
          >
            <motion.div animate={controls}>
              <Card.Img
                variant="top"
                src={logo1}
                style={{ width: "50%" }}
              />
            </motion.div>
            <Card.Body>
              <Card.Title style={{ textAlign: "center", marginBottom: "2rem" }}>
                <h2>SignUp Form</h2>
              </Card.Title>
              <Form className='d-flex flex-column'>

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
                            changeCredential("confirmedPassword", event.target.value)
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

                <div className="d-flex justify-content-center">
                  <Button variant="primary" type="submit" style={{ marginTop: "1rem", width: "100%" }} onClick={(event) => handleRegister(event)}>
                    Sign Up
                  </Button>
                </div>
              </Form>
            </Card.Body>
          </Card>
        </motion.div>
      </Col>
      <Col className="p-0 d-none d-xl-block" lg={{ span: 4, offset: 4 }} xl={{ span: 3, offset: 0 }} xxl={{ span: 4, offset: 0 }}>
        <KebabSaladCanvas />                
      </Col>
    </>
  )
}

export default Register

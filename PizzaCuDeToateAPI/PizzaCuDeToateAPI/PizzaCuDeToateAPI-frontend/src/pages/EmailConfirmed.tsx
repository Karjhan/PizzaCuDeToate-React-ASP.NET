import emailConfirmBackground from '../images/emailConfirm_backgroundFull.png'
import mailBoxGif from '../images/mailBoxLogo.gif'
import { Row } from 'react-bootstrap'
import { Col } from 'react-bootstrap'
import { Card } from 'react-bootstrap'
import { useParams } from 'react-router'
import { motion } from "framer-motion"

const EmailConfirmed = () => {
    const { email, username } = useParams();

    return (
        <Row style={{
            maxWidth: "100%",
            minHeight: "100vh",
            margin: "0rem",
            backgroundImage: `url(${emailConfirmBackground})`,
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat'
        }}>
            <Col xs={{span:12, offset:0}} className="d-flex justify-content-center">
                <Card style={{ background: "rgba(0,0,0,0.9)", border: "none" }} className="d-flex flex-lg-row flex-column align-items-center align-self-center w-75" id="emailConfirmWidth">
                    <Card.Img src={mailBoxGif} style={{ width:"50%"}} className='mb-5' id="mailBox"/>
                    <Card.Body style={{ color: "#DFD3C3", padding:"0rem", width:"100%" }}>
                        <h2 style={{ color: "#DFD3C3", textShadow: "0px 0px 8px black" }}>Email confirmed: <span style={{ fontStyle: "italic", fontSize:"1.5rem", textDecoration:"wavy underline" }}>{email}</span></h2>
                        <br></br>
                        <h5>Welcome in our family, <span style={{ fontStyle: "italic", fontSize: "1.7rem", textDecoration: "wavy underline" }}>{username}</span> !</h5>
                        <p>Thank you for choosing our food & delivery services. We hope you'll have a great experience with us.</p>
                        <a href="/login">
                            <motion.button
                                whileHover={{ scale: 1.1 }}
                                whileTap={{ scale: 0.9 }}
                                style={{ width: "50%", color: "#DFD3C3", marginTop: "4.5rem", backgroundColor: "rgba(180,10,10,1)", borderColor: "rgba(232,184,193,1)", borderWidth: "0.15rem", borderRadius: "0.5rem" }}
                                id="emailConfirmWidth"
                            >
                                <span style={{ color: "#DFD3C3", fontFamily: "Segoe UI", fontSize: "1.1rem", textDecoration: "none", fontWeight: "400" }}>Go to Login</span>
                            </motion.button>
                        </a>
                    </Card.Body>
                </Card>
            </Col>
        </Row>
    )
}

export default EmailConfirmed

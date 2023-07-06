import emailConfirmBackground from '../images/emailConfirm_backgroundFull.png'
import mailBoxGif from '../images/foodDeliveryLogo.gif'
import { Row } from 'react-bootstrap'
import { Col } from 'react-bootstrap'
import { Card } from 'react-bootstrap'
import { useParams } from 'react-router'
import { motion } from "framer-motion"

const OrderConfirmed = () => {
    const { orderId, username } = useParams();

    return (
        <Row style={{
            maxWidth: "100%",
            minHeight: "100vh",
            margin: "0rem",
            backgroundImage: `url(${emailConfirmBackground})`,
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat'
        }}>
            <Col xs={{ span: 12, offset: 0 }} className="d-flex justify-content-center">
                <Card style={{ background: "rgba(0,0,0,0.9)", border: "none" }} className="d-flex flex-lg-row flex-column align-items-center align-self-center w-75" id="emailConfirmWidth">
                    <Card.Img src={mailBoxGif} style={{ width: "50%", marginRight: "10%" }} className='mb-5' id="mailBox" />
                    <Card.Body style={{ color: "#DFD3C3", padding: "0rem", width: "100%" }}>
                        <h3 style={{ color: "#DFD3C3", textShadow: "0px 0px 8px black" }}>Thank you, <span style={{ fontStyle: "italic", fontSize: "1.65rem", textDecoration: "wavy underline" }}>{username?.replace("&", " ")}</span>!</h3>
                        <br></br>
                        <h5>Order, <span style={{ fontStyle: "italic", fontSize: "1.25rem", textDecoration: "wavy underline" }}>{orderId}</span> is on the way</h5>
                        <p>Thank you for choosing our food & delivery services. You'll never go hungry with us!</p>
                        <a href="/">
                            <motion.button
                                whileHover={{ scale: 1.1 }}
                                whileTap={{ scale: 0.9 }}
                                style={{ width: "50%", color: "#DFD3C3", marginTop: "4.5rem", backgroundColor: "rgba(180,10,10,1)", borderColor: "rgba(232,184,193,1)", borderWidth: "0.15rem", borderRadius: "0.5rem" }}
                                id="emailConfirmWidth"
                            >
                                <span style={{ color: "#DFD3C3", fontFamily: "Segoe UI", fontSize: "1.1rem", textDecoration: "none", fontWeight: "400" }}>Go to Home</span>
                            </motion.button>
                        </a>
                    </Card.Body>
                </Card>
            </Col>
        </Row>
    )
}

export default OrderConfirmed

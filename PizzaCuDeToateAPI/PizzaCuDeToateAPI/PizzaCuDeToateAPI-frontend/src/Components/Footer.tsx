import Card from 'react-bootstrap/Card';
import { ImFacebook2, ImInstagram, ImGooglePlus2, ImTwitter, ImHome, ImPhone, ImEnvelop } from 'react-icons/im'
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { motion } from "framer-motion"

const Footer = () => {
  return (
    <Card as="div" bg={"dark"} style={{ borderStyle: "none", borderRadius: "0px", backgroundImage:"radial-gradient(circle, rgba(230,0,0,0.85) 70%, rgba(147,1,1,1) 100%)", padding:"0px"}}>
      <Card.Body className='d-flex flex-md-row flex-column justify-content-around align-items-md-start align-items-center'>
        <Card style={{ backgroundColor: "rgba(0,0,0,0)", borderColor: "rgba(0,0,0,0)", color: "#DFD3C3", padding: "1rem", width:"22rem"}}>
          <Card.Title style={{ textAlign: "left", fontSize:"2rem" }}>Newsletter</Card.Title>
          <Form>
            <Form.Group className="mb-1" controlId="formBasicEmailFooter">
              <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
                <Form.Control type="email" placeholder="Enter email here..." />
              </motion.div>
            </Form.Group>
            <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.9 }}>
              <Button variant="primary" type="submit" style={{ width: "100%", color: "#DFD3C3" }} onClick={(event) => { event.preventDefault(); }}>
                Subscribe
              </Button>
            </motion.div>
          </Form>
        </Card>
        <Card style={{ backgroundColor: "rgba(0,0,0,0)", borderColor: "rgba(0,0,0,0)", color: "#DFD3C3", padding: "1rem", width: "22rem" }}>
            <Card.Title style={{textAlign:"left", fontSize:"2rem"}}>Keep Connected</Card.Title>
            <div className="d-flex align-items-center">
              <motion.div whileHover={{ scale: 1.5 }} whileTap={{ scale: 0.9 }}>
                <ImFacebook2 className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
              </motion.div>
              <p style={{marginBottom:"0px", marginLeft:"0.5rem"}}>Like us on Facebook</p>
            </div>
            <div className="d-flex align-items-center">
              <motion.div whileHover={{ scale: 1.5 }} whileTap={{ scale: 0.9 }}>
                <ImInstagram className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
              </motion.div>
              <p style={{ marginBottom: "0px", marginLeft: "0.5rem" }}>Follow us on Instagram</p>
            </div>
            <div className="d-flex align-items-center">
              <motion.div whileHover={{ scale: 1.5 }} whileTap={{ scale: 0.9 }}>
                <ImTwitter className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
              </motion.div>
              <p style={{ marginBottom: "0px", marginLeft: "0.5rem" }}>Follow us on Twitter</p>
            </div>
            <div className="d-flex align-items-center">
              <motion.div whileHover={{ scale: 1.5 }} whileTap={{ scale: 0.9 }}>
                <ImGooglePlus2 className="mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
              </motion.div> 
              <p style={{ marginBottom: "0px", marginLeft: "0.5rem" }}>Add us on Google Plus</p>
            </div>
        </Card>
        <Card style={{ backgroundColor: "rgba(0,0,0,0)", borderColor: "rgba(0,0,0,0)", color: "#DFD3C3", padding: "1rem", width: "22rem" }}>
          <Card.Title style={{ textAlign: "left", fontSize: "2rem" }}>Contact Information</Card.Title>
          <div className="d-flex align-items-center">
            <ImHome className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
            <p style={{ marginBottom: "0px", marginLeft: "0.5rem", textAlign:"left" }}>Bucharest, Sector 2,<br />Semilunei Street, Nr. 4-6<br/>Bucharest 020797</p>
          </div>
          <div className="d-flex align-items-center">
            <ImPhone className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
            <p style={{ marginBottom: "0px", marginLeft: "0.5rem" }}>0742696661</p>
          </div>
          <div className="d-flex align-items-center">
            <ImEnvelop className="mb-2 mt-2" style={{ width: "1.5rem", height: "1.5rem" }} />
            <p style={{ marginBottom: "0px", marginLeft: "0.5rem" }}>PizzaCuDeToate@gmail.com</p>
          </div>
        </Card>
      </Card.Body>
      <Card.Footer style={{backgroundImage:"radial-gradient(circle, rgba(220,50,50,0.85) 70%, rgba(177,35,35,1) 100%)", border:"2rem"}}>
        <Card.Title style={{ fontStyle: "italic", color: "#DFD3C3", margin:"0.2rem" }}>@ 2023 Copyright PizzaCuDeToate</Card.Title>
      </Card.Footer>
    </Card>
  )
}

export default Footer

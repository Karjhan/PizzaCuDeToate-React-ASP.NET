import ParticlesBackground from "../components/ParticlesBackground";
import NavbarMain from "../components/NavbarMain";
import Footer from "../components/Footer";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const Customize = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; logged: boolean; }) => {
  return (
    <>
        <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
        <Row>
            <NavbarMain logged={props.logged}/>
        </Row>
        <Row>
            CUSOMIZE
        </Row>
        <Row>
            <Footer/>
        </Row>
    </>
  )
}

export default Customize

import ParticlesBackground from "../components/ParticlesBackground";
import NavbarMain from "../components/NavbarMain";
import Footer from "../components/Footer";
import Row from "react-bootstrap/Row";

const Customize = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; logged: object; setLogged: any; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any; notify: (arg0: string, arg1: boolean) => void; }) => {
  return (
    <>
        <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
        <Row>
            <NavbarMain logged={props.logged} basket={props.basket} setBasket={props.setBasket} setLogged={props.setLogged}setSpinner={props.setSpinner} loading={props.loading} notify={props.notify}/>
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

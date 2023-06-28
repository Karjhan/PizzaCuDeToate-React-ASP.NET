import ParticlesBackground from "../components/ParticlesBackground";
import Footer from "../components/Footer";
import Row from "react-bootstrap/Row";

const Order = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; setLogged: any; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any }) => {
  return (
    <>
      <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
      <Row>
        ORDER
      </Row>
      <Row>
        <Footer />
      </Row>
    </>
  )
}

export default Order


import NavbarPizza from '../components/NavbarPizza'
import NavbarShawarma from '../components/NavbarShawarma'
import "../scss/home.scss"
import { Button } from 'react-bootstrap'
const Home = () => {
  return (
    <>
      {/* <div className="d-flex"><img
      className="justify-content-center "
        src="src\Images\logo.png">
      </img></div> */}
      <div className="main">
        <span className="webdev">PIZZA</span>
        <span className="socod">CU DE TOATE</span>
      </div>
      <NavbarPizza />
      <NavbarShawarma />


    </>
  )
}

export default Home
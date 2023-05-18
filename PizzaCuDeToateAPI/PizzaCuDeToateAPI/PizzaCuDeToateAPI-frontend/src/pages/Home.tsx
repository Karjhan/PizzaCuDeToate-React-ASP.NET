
import NavbarPizza from '../Components/NavbarPizza'
import NavbarShawarma from '../Components/NavbarShawarma'
import "../scss/home.scss"
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
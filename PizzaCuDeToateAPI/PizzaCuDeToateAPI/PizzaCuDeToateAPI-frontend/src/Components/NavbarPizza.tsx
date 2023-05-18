import { useState} from 'react'
import '../scss/navbarPizza.scss'
import '../scss/navbar.scss'
import { motion } from 'framer-motion'
import { Link } from 'react-router-dom'

const NavbarPizza = () => {
  const [pressed, setPressed] = useState(false);

  const toggleClass = () => {
    setPressed(!pressed);

  }
  return (
    <>
      <motion.button

        className="nav-wrapper-left"
        onClick={() => { toggleClass(); }}>
        <motion.img
        
          className="pizzaImg"
          src="src\Images\pizzaRef.png"
          animate={{ rotate: pressed ? 90 : -90 }}
          transition={{ type: "tween", duration: 1 }}>
        </motion.img>
      </motion.button>
      
      <motion.div animate={{ x: !pressed ? "-20rem" : "40rem" }}
        transition={{ type: "tween", duration: 1 }}
        initial={{ x: "-20rem" }}
        className="nav-body-left">
          <Link to='/menu'>
        <button className="wrapper">
        <h1 data-heading='PIZZA'>PIZZA</h1>
        </button>
        </Link>
          
      </motion.div>
      
    </>

  )
}

export default NavbarPizza

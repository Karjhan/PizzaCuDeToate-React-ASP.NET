import { useState, useRef, useEffect } from 'react'
import { motion } from 'framer-motion'
import '../scss/navbarShawarma.scss'
import '../scss/navbar.scss'
const NavbarShawarma = () => {
  const [pressed, setPressed] = useState(false);
  const buttonRef = useRef() as React.MutableRefObject<HTMLButtonElement>
  useEffect(() => {
    console.log(buttonRef.current.offsetTop)
  }, [])
  const toggleClass = () => {
    setPressed(!pressed);

  }

  return (
    <>
      <motion.button
        className="nav-wrapper-right"
        ref={buttonRef}
        onClick={() => { toggleClass(); }}>
        <motion.img
          className="kebapImg"
          src="src\Images\kebapRef.png"
          animate={{ rotate: pressed ? 110 : -70 }}
          transition={{ type: "tween", duration: 1 }}
        ></motion.img>
      </motion.button>
      <motion.div animate={{ x: !pressed ? 400 : -100 }}
        transition={{ type: "tween", duration: 1 }}
        initial={{ x: 400 }}
        className="nav-body-right">
        <button className="wrapper">
          <h1 data-heading="KEBAB">KEBAB</h1>
        </button>
      </motion.div>
    </>

  )
}

export default NavbarShawarma

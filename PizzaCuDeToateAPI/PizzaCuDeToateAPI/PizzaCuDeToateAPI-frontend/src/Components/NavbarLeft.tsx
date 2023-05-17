import {useState} from 'react'
import '../scss/navbar.scss'
import {motion} from 'framer-motion'
const NavbarLeft = () => {
  const [pressed,setPressed]=useState(false);

  const toggleClass=()=>{
    //!pressed?navBody.current.className="slide-left nav-body-left":navBody.current.className="nav-body-left";
    setPressed(!pressed);
    
    
  }
  return (
    <>
        <motion.button 
        className="nav-wrapper-left"
        onClick={()=>{toggleClass();}}>
        <motion.img 
        className="pizzaImg"
        src="src\Images\pizzaRef.png"
        animate={{rotate:pressed?90:-90}}
        transition={{type:"tween",duration:1}}>
        </motion.img>
        </motion.button>
      <motion.div animate={{x:!pressed?-400:100}}
      transition={{type:"tween",duration:1}}
      initial={{x:-400}}
        className="nav-body-left">Pizza Place for Everyo</motion.div>
    </>
    
  )
}

export default NavbarLeft

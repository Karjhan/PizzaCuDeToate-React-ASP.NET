import PizzaNavBar from "./PizzaNavBar"
import PizzaNavBar2 from "./PizzaNavBar2"
function Header(){
  return(
  <>
  <div>
    <PizzaNavBar logged={false} totalItems={0} totalPrice={0}/>
    <PizzaNavBar2/>
  </div>
  </>
  
  )
}

export default Header
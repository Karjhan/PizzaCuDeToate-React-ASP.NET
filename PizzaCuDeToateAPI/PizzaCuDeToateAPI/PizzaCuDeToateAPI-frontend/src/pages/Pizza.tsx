import { useState, useEffect} from 'react'
import axios from 'axios';
import Header from '../components/Header'
import PizzaCard from '../components/PizzaCard';
const Pizza = () => {

  const [pizzas,setPizzas] = useState([]);

  useEffect(()=>{
    axios.get("https://localhost:44388/pizzas").then((response)=>{
    setPizzas(response.data)
    console.log(response.data)
  })
    },[])


  return (
    <>
      <Header/>
      <div className='container'>
      <PizzaCard pizzas={pizzas}/>
      </div>
    </>
  )
}

export default Pizza

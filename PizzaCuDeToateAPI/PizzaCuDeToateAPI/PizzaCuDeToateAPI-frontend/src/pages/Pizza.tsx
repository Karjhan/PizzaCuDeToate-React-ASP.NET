import React,{useState, useEffect} from 'react'
import axios from 'axios';
import Header from '../components/Header'
import PizzaCard from '../components/PizzaCard';
const Pizza = () => {

  const [pizzas,setPizzas] = useState([]);

  useEffect(()=>{
    axios.get("http://localhost:5228/pizzas").then((response)=>{
    setPizzas(response.data)
    console.log(response.data)
  })
    },[])


  return (
    <div>
      <Header/>
      <PizzaCard pizzas={pizzas}/>
    </div>
  )
}

export default Pizza

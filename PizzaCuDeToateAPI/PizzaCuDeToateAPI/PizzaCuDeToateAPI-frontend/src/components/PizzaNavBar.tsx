
import { Link } from "react-router-dom";
import { useState } from "react";
import { IoPizzaOutline } from "react-icons/io5";
import { GiDonerKebab} from "react-icons/gi";
import { RiCake3Line } from "react-icons/ri";
import { TbBottle } from "react-icons/tb";
import { BsList } from "react-icons/bs";
import { CiReceipt } from "react-icons/ci";

function PizzaNavBar(props: {totalItems:number; totalPrice:number}) {
    const [navToggle, setNavToggle] = useState(false);

    return (
        <nav className={navToggle ? "show" : ""}  style={{background:'#DC0037'}}>
            <ul>
                <li className="logo" style={{fontSize:'25px'}}>
                    <a id="logo" href="#">
                        PizzaCuDeToate
                    </a>
                </li>
                <li>
                    <a href="#">About Us</a>
                </li>
                <li>
                    <a href="#">Sign Up</a>
                </li>
                <li>
                    <a href="#">Login</a>
                </li>
                <li>
                  <div className="dropdown">
                    <button style={{background:"green", color:"white"}} className="rounded-pill">Your Cart is Empty</button>
                    <div className="dropdown-options">
                      <p>{props.totalItems} items {props.totalPrice} RON</p>
                    </div>
                    </div>
                </li>
                <li className="toggle">
                    <button onClick={() => setNavToggle(!navToggle)}>
                        <i><BsList/></i>
                    </button>
                </li>
            </ul>
            <hr style={{color:"white"}}/>
            <ul className="nav2">
                <li>
                    <i><IoPizzaOutline/></i>
                    <a href="#">Pizza</a>
                </li>
                <li>
                  <i><GiDonerKebab/></i>
                    <a href="#">Shawarma</a>
                </li>
                <li>
                  <i><RiCake3Line/></i>
                    <Link to="/desserts">Desserts</Link>
                </li>
                <li>
                  <i><TbBottle/></i>
                    <a href="#">Drinks</a>
                </li>
            </ul>
        </nav>
  );
}

export default PizzaNavBar;
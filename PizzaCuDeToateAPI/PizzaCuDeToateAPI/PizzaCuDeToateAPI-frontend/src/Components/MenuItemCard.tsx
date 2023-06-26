import '../MenuItemCard.css'
import { Image, Button } from 'react-bootstrap'
import { useState } from 'react'
import { motion } from 'framer-motion'

const MenuItemCard = (props: { menuItem: { id: number, name: string, categoryId: number, description: string, ingredientIds: number[], ingredientNames: string[], logo: string, unitPrice: number, images: string[] }, basket : {id: number, name: string, amount: number}[], setBasket : any }) => {
    const [selectedAmount, setSelectedAmount] = useState(0);
    const [cartButtonClicked, setCartButtonClicked] = useState(false);

    const handleIncreaseAmount = () => {
        setSelectedAmount(selectedAmount+1);
    }
    const handleDecreaseAmount = () => {
        selectedAmount > 0 ? setSelectedAmount(selectedAmount - 1) : 0;
    }
    const handleCartClick = async () => {
        setCartButtonClicked(!cartButtonClicked);
        const indexOfItem = props.basket.findIndex((item) => item.name == props.menuItem.name && item.id == props.menuItem.id);
        if (indexOfItem === -1) {
            props.setBasket([...props.basket, { id: props.menuItem.id, name: props.menuItem.name, amount: selectedAmount }]);
        } else {
            let tempArray = [...props.basket];
            tempArray[indexOfItem].amount += selectedAmount;
            props.setBasket(tempArray);
        }
        setSelectedAmount(0);
        await new Promise(resolve => setTimeout(resolve, 2000));
        setCartButtonClicked(cartButtonClicked);
    }

    return (
        <div className="menu-card-container">
            <div className="menu-card">
                    <Image src={props.menuItem.logo} alt='logo' style={{borderRadius:"1rem", border:"5px", borderColor:"black", borderStyle:"ridge"}} />  
                    <p style={{ marginTop: "1rem" }} className='menu-item-font'>{props.menuItem.name}</p>
                    <div className="menu-card-details">
                        {props.menuItem.ingredientNames.length > 1 ? 
                            <div className="menu-ingredients">
                                <h3>Ingredients</h3>
                                <span>{props.menuItem.ingredientNames.join(", ")}</span>
                                <br /><br />
                                <span style={{ fontSize: "20px" }}>{props.menuItem.unitPrice} RON</span>
                            </div>
                            :
                            <div className="menu-ingredients">
                                <h3>500 ML</h3>
                            </div>
                        }
                        <div className="menu-quantity" style={{ marginTop: "1rem" }}>
                            <motion.div whileHover={{ scale: 1.1 }} whileTap={{ scale: 0.9 }}>
                                <Button style={{ borderRadius: "0px", width: "40px", fontWeight: "bold", backgroundColor: "#E64848", borderColor: "#E64848" }} onClick={(event) => { handleDecreaseAmount() }}>-</Button>
                            </motion.div>
                            <input
                                type="text"
                                value={selectedAmount}
                            ></input>
                            <motion.div whileHover={{ scale: 1.1 }} whileTap={{ scale: 0.9 }}>
                                <Button style={{ borderRadius: "0px", width: "40px", fontWeight: "bold", backgroundColor: "#2EB086", borderColor: "#2EB086" }} onClick={(event) => { handleIncreaseAmount() }}>+</Button>
                            </motion.div>                        
                        </div>
                        <div style={{marginTop:"1rem", paddingRight:"1.6rem", paddingLeft:"1.6rem"}} className="d-flex justify-content-center">
                            <button className={`cart-button ${cartButtonClicked ? "clicked" : ""}`} onClick={handleCartClick} disabled={cartButtonClicked || selectedAmount === 0}>
                                <span className="add-to-cart">Add to cart</span>
                                <span className="added">Added</span>
                                <i className="fas fa-shopping-cart"></i>
                                <i className="fas fa-box"></i>
                            </button>
                        </div>
                    </div>
                    <div className="menu-layers">
                        <div className="menu-layer"></div>
                    </div>
            </div>
        </div>
    )
}

export default MenuItemCard
import { useState, useEffect } from 'react'
import ParticlesBackground from "../components/ParticlesBackground";
import NavbarMain from "../components/NavbarMain";
import Footer from "../components/Footer";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import MenuItemCard from '../components/MenuItemCard';
import { GiFullPizza } from 'react-icons/gi'
import { GiDonerKebab } from "react-icons/gi";
import { RiCake3Line } from "react-icons/ri";
import { TbBottle } from "react-icons/tb";
import Carousel from 'react-bootstrap/Carousel';

const Menu = (props: { setSpinner: (arg0: boolean) => void; loading: boolean; logged: object; setLogged: any; basket: { logo: string, unitPrice: number, name: string, amount: number }[]; setBasket: any; notify: (arg0: string, arg1: boolean) => void; }) => {
    const [categories, setCategories] = useState([]);
    const [foodItems, setFoodItems] = useState({});
    const [selectedCategory, setSelectedCategory] = useState("Pizza");
    const [isMobile, setIsMobile] = useState(false);

    useEffect(() => {
        const mediaQuery = window.matchMedia("(max-width: 992px)");
        setIsMobile(mediaQuery.matches);
        const handleMediaQueryChange = (event: { matches: boolean | ((prevState: boolean) => boolean); }) => {
            setIsMobile(event.matches);
        };
        mediaQuery.addEventListener("change", handleMediaQueryChange);
        return () => {
            mediaQuery.removeEventListener("change", handleMediaQueryChange);
        };
    }, []);

    useEffect(() => {
        const fetchCategoriesData = async () => {
            props.setSpinner(true);
            const responseCategories = await fetch("https://localhost:44388/api/foodItems/categories", {
                headers: {
                    "Content-Type": "application/json",
                    "Access-Control-Allow-Origin": "*"
                }
            });
            const categoriesData = await responseCategories.json();
            setCategories(categoriesData);
            return categoriesData;
        }
        const fetchFoodData = async (categoriesArray : string[]) => {
            let temporaryObject = {};
            for(let i = 0; i < categoriesArray.length; i++){
                let responseData = await fetch(`https://localhost:44388/api/foodItems/category=${categoriesArray[i]}`, {
                    headers: {
                        "Content-Type": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    }
                })
                let foodData = await responseData.json();
                temporaryObject[categoriesArray[i]] = foodData;
            }
            setFoodItems(temporaryObject);
            props.setSpinner(false);
        }
        fetchCategoriesData().then((categoriesData) => fetchFoodData(categoriesData));
        return;
    }, [])

    return (
        <>
            <ParticlesBackground style={{ filter: props.loading ? "blur(10px)" : "none", pointerEvents: props.loading ? "none" : "auto" }} />
            <Row>
                <NavbarMain logged={props.logged} basket={props.basket} setBasket={props.setBasket} setLogged={props.setLogged} setSpinner={props.setSpinner} loading={props.loading} notify={props.notify} />
            </Row>
            <Row style={{ backgroundImage: `${Object.keys(foodItems).length > 1 ? "radial-gradient(circle, rgba(230,0,0,1) 70%, rgba(147,1,1,1) 100%)" : "transparent"}`, position: "relative", boxShadow: "0px -4px 3px rgba(50, 50, 50, 0.75)" }}>
                <Col className="d-flex flex-row justify-content-around" style={{ padding: "0", width: "100%" }}>
                    {Object.keys(foodItems).length > 1 ? (
                        isMobile ?
                            <Carousel style={{width: "100%"}} indicators={false}>
                                {categories.map((category, index) => {
                                    if (category === "Pizza") {
                                        return <Carousel.Item>
                                            <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`}  onClick={() => setSelectedCategory(category)}>
                                                <GiFullPizza style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                                <p className="menu-text-title-font">{category}</p>
                                            </div>
                                        </Carousel.Item>
                                    } else if (category === "Shawarma") {
                                        return <Carousel.Item>
                                            <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`}  onClick={() => setSelectedCategory(category)}>
                                                <GiDonerKebab style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                                <p className="menu-text-title-font">{category}</p>
                                            </div>
                                        </Carousel.Item>
                                    } else if (category === "Dessert") {
                                        return <Carousel.Item>
                                            <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`}  onClick={() => setSelectedCategory(category)}>
                                                <RiCake3Line style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                                <p className="menu-text-title-font">{category}</p>
                                            </div>
                                        </Carousel.Item>
                                    } else if (category === "Beverage") {
                                        return <Carousel.Item>
                                            <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`}  onClick={() => setSelectedCategory(category)}>
                                                <TbBottle style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                                <p className="menu-text-title-font">{category}</p>
                                            </div>
                                        </Carousel.Item>
                                    } else {
                                        return <Carousel.Item>
                                            <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`}  onClick={() => setSelectedCategory(category)}>
                                                <p className="menu-text-title-font">{category}</p>
                                            </div>
                                        </Carousel.Item>
                                    }
                                })}
                            </Carousel>
                            :
                            <>
                                {categories.map((category, index) =>
                                {
                                    if (category === "Pizza") {
                                        return <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`} style={{ width: `${100 / categories.length}%` }} onClick={() => setSelectedCategory(category)}>
                                            <GiFullPizza style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                            <p className="menu-text-title-font">{category}</p>
                                        </div>
                                    } else if (category === "Shawarma") {
                                        return <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`} style={{ width: `${100 / categories.length}%` }} onClick={() => setSelectedCategory(category)}>
                                            <GiDonerKebab style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                            <p className="menu-text-title-font">{category}</p>
                                        </div>
                                    } else if (category === "Dessert") {
                                        return <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`} style={{ width: `${100 / categories.length}%` }} onClick={() => setSelectedCategory(category)}>
                                            <RiCake3Line style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                            <p className="menu-text-title-font">{category}</p>
                                        </div>
                                    } else if (category === "Beverage") {
                                        return <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`} style={{ width: `${100 / categories.length}%` }} onClick={() => setSelectedCategory(category)}>
                                            <TbBottle style={{ fontSize: "300%", marginRight: "0.5rem", color: "white" }} />
                                            <p className="menu-text-title-font">{category}</p>
                                        </div>
                                    } else {
                                        return <div key={index} className={`menu-div-option ${category === selectedCategory ? "menu-div-option-active" : ""}`} style={{ width: `${100 / categories.length}%` }} onClick={() => setSelectedCategory(category)}>
                                            <p className="menu-text-title-font">{category}</p>
                                        </div>
                                    }  
                                }
                                )}
                            </>
                    )
                    :
                    (
                        <div style={{width: "100%", height:"70vh", backgroundColor:"transparent"}}/>
                    )
                    }
                </Col>
            </Row>
            <Row style={{padding:"2rem"}}>
                {Object.keys(foodItems).length > 1 && (
                    foodItems[selectedCategory].map((foodItem, index) =>
                        <Col xs={12} md={6} lg={4} xl={3} className="d-flex justify-content-center" key={index}>
                            <MenuItemCard menuItem={foodItem} basket={props.basket} setBasket={props.setBasket}/>
                        </Col>
                    )
                )}
            </Row>
            <Row>
                <Footer/>
            </Row>
        </>
    )
}

export default Menu

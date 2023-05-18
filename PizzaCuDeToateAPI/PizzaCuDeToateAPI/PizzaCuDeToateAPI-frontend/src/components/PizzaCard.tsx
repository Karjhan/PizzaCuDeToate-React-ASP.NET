
import {useState,useEffect} from 'react';
// import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';

function PizzaCard(props: {pizzas:Array<{id:number,name:string,description:string, unitPrice:number,logo:string, ingredients:[number]}>}){
const [count, setCount] = useState(0);
const [message, setMessage] = useState("");


   const handleMinusClick = () => {
    if (count > 1) {
      setCount(count - 1);
      setMessage("");
    } else {
      setMessage("Please Enter a Valid Number");
    }
  };

  // ---- handlePlusClick increments our input value with 1 onClick
  const handlePlusClick = () => {
    if (count === null || count == 0) {
      setCount(1);
    } else {
      setCount(count + 1);
      setMessage("");
    }
  };

  function handleInputValueChange(e) {
    e.preventDefault();
    const regex = /^[0-9\b]+$/;
    if (e.target.value === "" || regex.test(e.target.value)) {
      const inputValue = Number(e.target.value);
      setCount(inputValue);
      setMessage("Qty updated");
    } else {
      setMessage("Your input is not valid");
    }
  }

  return(
  <div id="table">
            {props.pizzas.map((pizza, index) => (
              <Card key={pizza.name} style={{ width: "20%" }}>
                <Card.Img variant={top} src={pizza.logo} fluid="true" />
                <Card.Body>
                  <Card.Title>{pizza.name}</Card.Title>
                  <Card.Text>
                    {pizza.description}
                  </Card.Text>
                  {/* <Card.Text>{}</Card.Text> */}
                  <Card.Text>Price: {pizza.unitPrice} RON</Card.Text>
                  <div className="input">
                    <div className="counter">
                      <button
                        className="minus"
                        onClick={() => handleMinusClick()}>
                        -
                      </button>
                      <input
                        type="text"
                        min="1"
                        value={count}
                        onClick={() => setCount(0)}
                        onChange={(e) => handleInputValueChange(e)}
                        style={{ width: "20%", textAlign: "center" }}
                      />
                      <button
                        className="plus"
                        onClick={() => handlePlusClick()}>
                        +
                      </button>
                    </div>
                    <br />
                    {message}
                    <br />
                    <button className="AddBtn">
                      Add
                    </button>
                  </div>
                </Card.Body>
              </Card>
            ))}
          </div>);
}

export default PizzaCard;
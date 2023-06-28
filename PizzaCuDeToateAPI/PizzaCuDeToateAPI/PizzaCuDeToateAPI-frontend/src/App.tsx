import { Container } from 'react-bootstrap'
import { Routes, Route } from 'react-router-dom';
import { useState, useEffect, useRef } from 'react';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import { ThreeCircles } from 'react-loader-spinner';
import { GoogleOAuthProvider } from '@react-oauth/google';
import Menu from './pages/Menu';
import './App.css'
import EmailConfirmed from './pages/EmailConfirmed';
import Customize from './pages/Customize';
import { encryptData, decryptData } from './scripts/utils.ts'
import Order from './pages/Order.tsx';

// window.onbeforeunload = function () {
//   localStorage.clear();
//   return;
// }

function App() {
  const [logged, setLogged] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const [basket, setBasket] = useState([]);
  const didMountBasket = useRef(false);
  const didMountJwt = useRef(false);
  
  useEffect(() => {
    const basketInStorage = decryptData("basket");
    const jwtInStorage = decryptData("jwt");
    if(basketInStorage === null){
      encryptData("basket", []);
    } else {
      setBasket(basketInStorage);
    }
    if(jwtInStorage === null){
      encryptData("jwt", {});
    } else {
      setLogged(jwtInStorage);
    }
  }, [window.location.pathname])

  useEffect(() => {
    if (didMountBasket.current) {
      encryptData("basket", basket);
    } else {
      didMountBasket.current = true;
    }
  }, [basket])
  
  useEffect(() => {
    if (didMountJwt.current) {
      encryptData("jwt", logged);
    } else {
      didMountJwt.current = true;
    }
    console.log(decryptData("jwt")["http://schemas.microsoft.com/ws/2008/06/identity/claims/name"])
  }, [logged])

  return (
    <>
      <GoogleOAuthProvider clientId="243357289852-s807hi6ior999l811crverov3lenloi7.apps.googleusercontent.com">
        <ThreeCircles
          height="100%"
          width="20%"
          color="crimson"
          wrapperStyle={{}}
          wrapperClass="threee-circles-wrapper"
          visible={isLoading}
          ariaLabel="three-circles-rotating"
          outerCircleColor=""
          innerCircleColor=""
          middleCircleColor=""
        />
        <Container fluid style={{ padding:"0", filter: isLoading ? "blur(10px)" : "none", pointerEvents: isLoading ? "none" : "auto" }}>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/register" element={<Register setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged}/>} />
              <Route path="/login" element={<Login setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged}/>} />
              <Route path="/emailConfirmed/:email/:username" element={<EmailConfirmed/>} />
              <Route path="/customize" element={<Customize setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged}/>}/>
              <Route path="/menu" element={<Menu setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged}/>} />
              <Route path="/order" element={<Order setSpinner={setIsLoading} loading={isLoading} basket={basket} setBasket={setBasket} setLogged={setLogged}/>}/>
            </Routes>
        </Container>
      </GoogleOAuthProvider>
    </>
  )
}

export default App

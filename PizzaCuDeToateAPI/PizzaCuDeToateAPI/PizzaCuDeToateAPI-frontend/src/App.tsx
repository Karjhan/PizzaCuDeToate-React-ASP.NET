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
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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
  }, [logged])

  const notify = (message: string, isError: boolean) => {
    if(isError){
      toast.error(message, {
        position: "top-center",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "dark",
      });
    }else{
      toast(message, {
        position: "top-center",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "dark",
      });
    }
  }

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
              <Route path="/register" element={<Register setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged} notify={notify}/>} />
              <Route path="/login" element={<Login setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged} notify={notify}/>} />
              <Route path="/emailConfirmed/:email/:username" element={<EmailConfirmed/>} />
              <Route path="/customize" element={<Customize setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged} notify={notify}/>}/>
              <Route path="/menu" element={<Menu setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} setLogged={setLogged} notify={notify}/>} />
              <Route path="/order" element={<Order setSpinner={setIsLoading} loading={isLoading} logged={logged} basket={basket} setBasket={setBasket} notify={notify}/>}/>
            </Routes>
        </Container>
        <ToastContainer
          position="top-center"
          autoClose={3000}
          limit={1}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="dark"
          // toastStyle={{ backgroundColor: "crimson" }}
        />
      </GoogleOAuthProvider>
    </>
  )
}

export default App

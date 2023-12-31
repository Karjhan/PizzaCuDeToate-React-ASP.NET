import React, { Suspense, useEffect, useState } from "react";
import { Canvas } from "@react-three/fiber";
import { OrbitControls, Preload, useGLTF } from "@react-three/drei";
import CanvasLoader from './Loader'

const KebabSalad = () => {
    

    return (
        <primitive object={kebabSalad.scene} scale={2.5} position-x={0} position-y={0.35} rotation-y={0} rotation-z={0} />
    );
};

const KebabSaladCanvas = () => {
    const kebabSalad = useGLTF("./3DModels/KebabSaladFormat/scene.gltf");
    const [isMobile, setIsMobile] = useState(false);

    useEffect(() => {
        // Add a listener for changes to the screen size
        const mediaQuery = window.matchMedia("(max-width: 1400px)");

        // Set the initial value of the `isMobile` state variable
        setIsMobile(mediaQuery.matches);

        // Define a callback function to handle changes to the media query
        const handleMediaQueryChange = (event: { matches: boolean | ((prevState: boolean) => boolean); }) => {
            setIsMobile(event.matches);
        };

        // Add the callback function as a listener for changes to the media query
        mediaQuery.addEventListener("change", handleMediaQueryChange);

        // Remove the listener when the component is unmounted
        return () => {
            mediaQuery.removeEventListener("change", handleMediaQueryChange);
        };
    }, []);


    return (
        <Canvas
            shadows
            frameloop='demand'
            gl={{ preserveDrawingBuffer: true }}
            camera={{
                fov: 30,
                near: 0.1,
                far: 200,
                position: [-3, 1, 3.5]
            }}
        >
            <Suspense fallback={<CanvasLoader />}>
                <OrbitControls
                    autoRotate
                    enableZoom={false}
                    maxPolarAngle={Math.PI / 6}
                    minPolarAngle={Math.PI / 6}
                />
                <primitive
                    object={kebabSalad.scene}
                    scale={isMobile ? 1.5 : 2.5}
                    rotation={[0, 0, Math.PI / 10]}
                    position={[0, isMobile ? 0.35 : 0.45,0]}
                />
            </Suspense>
        </Canvas>
    )
}

export default KebabSaladCanvas

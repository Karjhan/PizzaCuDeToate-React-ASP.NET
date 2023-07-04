import React, { Suspense } from "react";
import { Canvas } from "@react-three/fiber";
import { OrbitControls, useGLTF } from "@react-three/drei";
import CanvasLoader from './Loader';

const RestaurantInteriorCanvas = () => {
    const restaurant = useGLTF("./3DModels/RestaurantInteriorFormat/scene.gltf");

    return (
        <Canvas
            shadows
            frameloop='demand'
            gl={{ preserveDrawingBuffer: true }}
            camera={{
                fov: 60,
                near: 0.1,
                far: 200,
                position: [-10, 20, 30]
            }}
        >
            <Suspense fallback={<CanvasLoader />}>
                <OrbitControls
                    autoRotate
                    enableZoom={false}
                    maxPolarAngle={Math.PI / 3}
                    minPolarAngle={Math.PI / 3}
                />
                <hemisphereLight intensity={1} groundColor='black' />
                <spotLight
                    position={[-20, 30, -50]}
                    angle={0.12}
                    penumbra={1}
                    intensity={1}
                    castShadow
                    shadow-mapSize={1024}
                />
                
                <primitive
                    object={restaurant.scene}
                    scale={0.1}
                    rotation={[-Math.PI/100, 0, 0]}
                    position={[0, 0.1, 0]}
                />
            </Suspense>
        </Canvas>
    )
}

export default RestaurantInteriorCanvas

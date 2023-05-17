import { loadFull } from "tsparticles"
import { useCallback } from "react"
import Particles from 'react-tsparticles'
import particlesConfig from "../scripts/particles-config.js"
import { Engine } from "tsparticles-engine"

const ParticlesBackground = () => {
  const particlesInit = useCallback(async (engine: Engine) => {
    console.log(engine);
    // you can initiate the tsParticles instance (engine) here, adding custom shapes or presets
    // this loads the tsparticles package bundle, it's the easiest method for getting everything ready
    // starting from v2 you can add only the features you need reducing the bundle size
    await loadFull(engine);
  }, []);

  const particlesLoaded = useCallback(async (container: any) => {
    console.log(container);
  }, []);

  return (
    <Particles
      id="tsparticles"
      init={particlesInit}
      loaded={particlesLoaded}
      style={{
        position: "absolute", top: "0", left: "0" }}
      height="95%"
      width="95%"
      options={particlesConfig}
    >
        
    </Particles>
  )
}

export default ParticlesBackground

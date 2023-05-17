const particlesConfig: object = {
    "fullScreen": {
        "enable": true,
        "zIndex": 0
    },
    "particles": {
        "number": {
            "value": 40,
            "density": {
                "enable": false,
                "value_area": 1000
            }
        },
        "color": {
            "value": "#fff"
        },
        "shape": {
            "character": [
                {
                    "fill": true,
                    "font": "Font Awesome 5 Free",
                    "style": "",
                    "value": ["\uf818", "\uf2e7", "\uf816"],
                    "weight": "900"
                }
            ],
            "image": {
                "height": 100,
                "replace_color": true,
                "src": "https://particles.js.org/images/github.svg",
                "width": 100
            },
            "polygon": { nb_sides: 5 },
            "stroke": { color: "#ffffff", width: 1 },
            "type": "char"
        },
        "opacity": {
            "value": 0.8,
            "random": false,
            "anim": {
                "enable": false,
                "speed": 1,
                "opacity_min": 0.1,
                "sync": false
            }
        },
        "size": {
            "value": 10,
            "random": false,
            "anim": {
                "enable": false,
                "speed": 40,
                "size_min": 0.1,
                "sync": false
            }
        },
        "rotate": {
            "value": 0,
            "random": true,
            "direction": "clockwise",
            "animation": {
                "enable": true,
                "speed": 15,
                "sync": false
            }
        },
        "line_linked": {
            "enable": true,
            "distance": 200,
            "color": "#ffffff",
            "opacity": 0.4,
            "width": 2
        },
        "move": {
            "enable": true,
            "speed": 2,
            "direction": "none",
            "random": false,
            "straight": false,
            "out_mode": "out",
            "attract": {
                "enable": false,
                "rotateX": 600,
                "rotateY": 1200
            }
        }
    },
    "interactivity": {
        "events": {
            "onhover": {
                "enable": true,
                "mode": ["grab"]
            },
            "onclick": {
                "enable": false,
                "mode": "bubble"
            },
            "resize": true
        },
        "modes": {
            "grab": {
                "distance": 400,
                "line_linked": {
                    "opacity": 1
                }
            },
            "bubble": {
                "distance": 400,
                "size": 40,
                "duration": 2,
                "opacity": 8,
                "speed": 3
            },
            "repulse": {
                "distance": 200
            },
            "push": {
                "particles_nb": 4
            },
            "remove": {
                "particles_nb": 2
            }
        }
    },
    "retina_detect": true,
    "background": {
        "image": "linear-gradient(90deg, #8E0505, #D8B6A4)",
        "position": "50% 50%",
        "repeat": "no-repeat",
        "size": "cover",
        "opacity": 1
    }
}

export default particlesConfig
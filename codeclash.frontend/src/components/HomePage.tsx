import React, {useEffect} from "react";
import TypeIt from "typeit-react";
import {motion, useMotionTemplate, useMotionValue, animate} from "framer-motion";
import '../style/HomePage/Main.css'
import '../style/HomePage/NavBar.css'


export default function HomePage() {
    Grid();
    return (
        <body>
            <div className="navbar_container">
                <div className="navbar-content">
                    <div className="navbar-text-content">
                        <a href="/" aria-current="page" className="navbar-logo">CodeClash</a>
                        <nav className="navbar-menu-links">
                            <div className="menu-buttons">
                                <button className="navbar-menu-button">Features</button>
                                <button className="navbar-menu-button">About us</button>
                            </div>
                            <div className="menu-buttons">
                                <button className="navbar-menu-button">Register</button>
                                <button className="navbar-menu-button">Login</button>
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
            <main className="main-wrapper">
                <header className="section_header position-relative">
                    <div className="text-align-center">
                        <h1 className="hero-section">
                            <span className="text-style-hero">CodeClash</span>
                            <br/>challenge the strongest
                        </h1>
                        <button className="get-started-button">Get Started</button>
                    </div>
                </header>
            </main>
        </body>
)
}

const Grid = () => {
    const rows = 10;
    const cols = 10;

    const grid = [];
    for (let i = 0; i < rows; i++) {
        for (let j = 0; j < cols; j++) {
            grid.push(<div key={`${i}-${j}`} className="grid-cell"></div>)
        }
    }

    return <div className="grid-container">{grid}</div>;
}





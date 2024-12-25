import React from "react";
import '../style/HomePage/Main.css'
import '../style/HomeAndAuthDefault/BackGround.css'
import HomeNavBar from "./HomeNavBar.tsx";


export default function HomePage() {
    const getStartedClick = async () => window.location.href = '/reg1';

    return (
        <div className="home-page">
            <HomeNavBar/>
            <main className="home-main-wrapper">
                <header className="section_header position-relative">
                    <div className="text-align-center">
                        <h1 className="hero-section">
                            <span className="text-style-hero">CodeClash</span>
                            <br/>challenge the strongest
                        </h1>
                        <button className="get-started-button" onClick={getStartedClick}>Get Started</button>
                    </div>
                </header>
            </main>
            <section>
                <div className="first-ellipse-container">
                    <div className="ellipse bottom-ellipse"/>
                    <div className="ellipse inner-ellipse"/>
                </div>
                <div className="first-fade-ellipse"/>
                <div className="second-fade-ellipse"/>
                <div className="third-fade-ellipse"/>
                <div className="ellipse right-ellipse"/>
                <div className="ellipse top-ellipse"/>
            </section>
        </div>
    )
};





import React, { useState } from 'react';
import HomeNavBar from "./HomeNavBar.tsx";
import '../style/HomeAndAuthDefault/BackGround.css'
import '../style/RegisterPage/Main.css'
import '../style/RegisterPage/RegField.css'
import '../style/RegisterPage/Input.css'

export default function RegisterPage() {
    return(
        <div className="register-page">

            <div className="ellipses-container">
                <div className="ellipse bottom-ellipse"/>
                <div className="ellipse inner-ellipse"/>
                <div className="first-fade-ellipse"/>
                <div className="second-fade-ellipse"/>
                <div className="third-fade-ellipse"/>
                <div className="ellipse right-ellipse"/>
                <div className="ellipse top-ellipse"/>
            </div>
            <div className="main-container">
                <div className="glass-effect"/>
                <HomeNavBar/>
                <main className="reg-main-wrapper">
                    <div className="reg-container">
                        <div className="reg-section">
                            <div className="reg-field">
                                <h1 className="reg-header">
                                    <span className="">Sign Up</span>
                                </h1>
                                <div className="input-field-container">
                                    <div className="input-box">
                                        <input type="text" required/>
                                        <label>Username</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>
    )
};
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
                                    <div className="input-box type-md">
                                        <input type='text' required name="name" id="name"/>
                                        <label htmlFor="name">Username</label>
                                        <span className="border"></span>
                                    </div>
                                    <div className="input-box type-md">
                                        <input type="email" required name="email" id="email"/>
                                        <label htmlFor="email">Email</label>
                                        <span className="border"></span>
                                    </div>
                                    <div className="input-box type-md">
                                        <input type="password" required name="password" id="password"/>
                                        <label htmlFor="password">Password</label>
                                        <span className="border"></span>
                                    </div>
                                    <div className="input-box type-md">
                                        <input type="password" required name="confirm-pw" id="confirm-pw"/>
                                        <label htmlFor="confirm-pw">Confirm Password</label>
                                        <span className="border"></span>
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
import React from "react";
import '../../style/Default/AuthNavBar.css'

export default function AuthNavBar() {
    return (
        <div className="navbar_container">
            <div className="navbar-content">
                <div className="navbar-text-content">
                    <a href="/public" aria-current="page" className="navbar-logo">CodeClash</a>
                </div>
            </div>
        </div>
    );
};
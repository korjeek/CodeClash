import React, {useEffect, useRef, useState} from "react";
import '../../style/Default/BaseNavBar.css'
import { motion } from "framer-motion";

const tabs = ["Competitions", "Problems", "Leaderboard"];

export default function BaseNavBar() {
    const [selected, setSelected] = useState(tabs[0]);

    return (
        <div className="navbar_container">
            <div className="navbar-content">
                <div className="navbar-text-content">
                    <a href="/public" aria-current="page" className="navbar-logo">CodeClash</a>
                    <nav className="menu-navbar">
                        {tabs.map((tab) => (
                            <Chip
                                text={tab}
                                selected={selected === tab}
                                setSelected={setSelected}
                                key={tab}
                            />
                        ))}
                    </nav>
                </div>
            </div>
        </div>
    );
};

const Chip = ({text, selected, setSelected}) => {
    return (
        <button
            onClick={() => setSelected(text)}
            className="nav-bar-button"
        >
            <span className="motion-span">{text}</span>
            {selected && (
                <motion.span
                    layoutId="pill-tab"
                    transition={{ type: "spring", duration: 0.5 }}
                    className="active-button"
                ></motion.span>
            )}
        </button>
    );
};
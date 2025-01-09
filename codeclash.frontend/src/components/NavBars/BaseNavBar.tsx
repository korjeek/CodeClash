import React, {useCallback, useState} from "react";
import '../../style/Default/BaseNavBar.css'
import {motion} from "framer-motion";
import {ChipProps} from "../../Props/ButtonsProps.ts";
import {useNavigate} from "react-router-dom";
import {initialSelectedTab, TabItem} from "../../Props/PageStateProps.ts";

const tabs = [
    { text: TabItem.Competitions, href: "/competitions" },
    { text: TabItem.Problems, href: "/problems" },
    { text: TabItem.Leaderboard, href: "/leaderboard" }
];

export default function BaseNavBar({ tab }: initialSelectedTab) {
    const [selected, setSelected] = useState<string>(tab);

    return (
        <div className="navbar_container">
            <div className="navbar-content">
                <div className="navbar-text-content">
                    <a href="/" aria-current="page" className="navbar-logo">CodeClash</a>
                    <nav className="menu-navbar">
                        {tabs.map((tab) => (
                            <Chip
                                text={tab.text}
                                href={tab.href}
                                selected={selected === tab.text}
                                setSelected={setSelected}
                                key={tab.text}
                            />
                        ))}
                    </nav>
                </div>
            </div>
        </div>
    );
};

const Chip: React.FC<ChipProps> = React.memo(({text, href, selected, setSelected}) => {
    const navigate = useNavigate();

    const handleClick = useCallback(() => {
        setSelected(text);
        navigate(href);
    }, [text, href, setSelected, navigate]);

    return (
        <button
            onClick={() => handleClick()}
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
});
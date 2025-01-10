import React, {useCallback, useEffect, useState} from "react";
import '../../style/Default/BaseNavBar.css'
import '../../style/Default/Buttons.css'
import {motion} from "framer-motion";
import {ChipProps} from "../../Props/ButtonsProps.ts";
import {useNavigate} from "react-router-dom";
import {initialSelectedTab, TabItem} from "../../Props/PageStateProps.ts";
import {getTokenFromCookies, getUserInfoFromToken} from "../../services/JWTTokenService.ts";
import {getRoomStatus} from "../../services/UserService.ts";
import {RoomStatus} from "../../interfaces/UserInterfaces.ts";

const tabs = [
    { text: TabItem.Competitions, href: "/competitions" },
    { text: TabItem.Problems, href: "/problems" },
    //{ text: TabItem.Leaderboard, href: "/leaderboard" }
];

export default function BaseNavBar({ tab }: initialSelectedTab) {
    const [selected, setSelected] = useState<string>(tab);
    const [userInfo, setUserInfo] = useState<{name: string, email: string}>({name: '', email: ''})
    const [roomStatus, setRoomStatus] = useState<RoomStatus>({hasRoom: false, competitionIssueId: ''});
    const navigate = useNavigate();

    useEffect(() => {
        const getUserInfo = () => {
            const cookies = getTokenFromCookies('spooky-cookies');
            if (cookies){
                const userInfo = getUserInfoFromToken(cookies)
                if (userInfo){
                    const name = userInfo["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
                    const email = userInfo["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
                    setUserInfo({name, email})
                }
            }
        }

        const fetchRoomStatus = async () => {
            const status = await getRoomStatus();
            setRoomStatus(status);
        }

        getUserInfo();
        fetchRoomStatus();
    }, []);

    const chooseLinkByStatus = async () => {
        return roomStatus.competitionIssueId ? navigate(`/problem/${roomStatus.competitionIssueId}`) :
            navigate(`/lobby`)
    }

    return (
        <div className="navbar_container">
            <div className="navbar-content">
                <div className="navbar-text-content">
                    <a href="/" aria-current="page" className="navbar-logo">CodeClash</a>
                    <nav className="menu-navbar">
                        <div className="navbar-menu-chips">
                            {tabs.map((tab) => (
                                <Chip
                                    text={tab.text}
                                    href={tab.href}
                                    selected={selected === tab.text}
                                    setSelected={setSelected}
                                    key={tab.text}
                                />
                            ))}
                        </div>
                        <div className="navbar-user-container">
                            {roomStatus.hasRoom &&
                                <button
                                    className={roomStatus.competitionIssueId ?
                                        "user-room-status-btn user-room-ready-btn" : "user-room-status-btn user-room-wait-btn"}
                                    onClick={chooseLinkByStatus}>
                                    {roomStatus.competitionIssueId ? 'Go to competition' : 'Return to lobby'}
                                </button>
                            }
                            <div className="navbar-profile">
                                {userInfo.name}
                                <p>
                                    {userInfo.email}
                                </p>
                            </div>
                        </div>
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
import {useEffect, useMemo, useState} from 'react';
import {useLocation, useNavigate} from "react-router-dom";
import {Room} from "../../interfaces/RoomInterfaces.ts";
import SignalRService from "../../services/SignalRService.ts";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import '../../style/Lobby/Buttons.css';
import '../../style/Lobby/Main.css';
import {checkForAdmin, getRoom} from "../../services/RoomService.ts";
import {TabItem} from "../../Props/PageStateProps.ts";

export default function Lobby() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const roomId = queryParams.get('id')!;
    const [room, setRoom] = useState<Room>();
    const [isAdmin, setIsAdmin] = useState<boolean>(false);
    const [count, setCount] = useState<number>(3);
    const [ready, setReady] = useState(false);
    const [competitionUrl, setCompetitionUrl] = useState<string>('');
    const signalR = useMemo(() => new SignalRService(), []);
    const navigate = useNavigate();

    useEffect(() => {
        signalR.onUserAction<Room>((room: Room) => {
            setRoom(room);
        }, "UserJoined");

        signalR.onUserAction<string>((url: string) =>
        {
            setReady(true);
            setCompetitionUrl(url);
        }, "CompetitionStarted");

        signalR.onUserAction<Room>((room: Room) => {
            if (!room)
                navigate('/competitions');
            setRoom(room);
        }, "UserLeave");

        const fetchAdminStatus = async () => {
            const isAdmin = await checkForAdmin();
            setIsAdmin(isAdmin);
        }

        const connectToRoom = async () => {
            await signalR.startConnection();
            const room = await getRoom()
            setRoom(room)
        };

        connectToRoom();
        fetchAdminStatus();
    }, [navigate, roomId, signalR]);

    useEffect(() => {
        if (ready && count > 0) {
            const timer = setTimeout(() => {
                setCount(count - 1);
            }, 1000);

            return () => clearTimeout(timer);
        } else if (ready && count === 0) {
            navigate(competitionUrl);
        }
    }, [competitionUrl, count, navigate, ready]);

    const quitRoom = async () => {
        await signalR.invoke<null, Room>("QuitRoom", null)
        await signalR.stopConnection()
        navigate('/competitions');
    }

    const startCompetition = async () => {
        if (room)
            await signalR.invoke<Room, string>("StartCompetition", room);
        else
            console.log("Can't start competition. Room doesn't exist.")
    }

    if (!room)
        return null;

    return (
        <div className="menu-page">
            <BaseNavBar tab={TabItem.Competitions}/>
            <div className="content-wrapper">
                <h1 className="room-header">{room.name} lobby</h1>
                <div className="lobby-grid">
                    <div className="online-count">Participants: {room.users.length}</div>
                    {/*<div className="competition-info">*/}
                    {/*    <h3 className="info-item">Author: {room.time.split(':')[1]} min</h3>*/}
                    {/*    <h3 className="info-item">Task: {room.issueName}</h3>*/}
                    {/*    <h3 className="info-item">Time: {room.time.split(':')[1]} min</h3>*/}
                    {/*</div>*/}
                    <div className="users-container">
                        {room.users.map((user, index) => (
                            <div key={user.email} className="user-container">
                                <div className="num-user">{index + 1}</div>
                                <div className="user-item">{user.name}</div>
                                {isAdmin && <button className="kick-room-btn">kick</button>}
                            </div>
                        ))}
                    </div>
                    <div className={ready ? "ready lobby-status" : "not-ready lobby-status"}>
                        {!ready && "Waiting for players..."}
                        {ready && `Starting in ${count}`}
                    </div>
                </div>
                <div className="btn-container">
                    <button className="lobby-btn quit-room-btn" onClick={quitRoom}>Quit Room</button>
                        {isAdmin &&
                            <button className="lobby-btn start-competition-btn" onClick={startCompetition}>Start
                                Competition</button>}
                </div>
            </div>
        </div>
    );
};
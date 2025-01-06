import {useEffect, useMemo, useState} from 'react';
import {useLocation, useNavigate} from "react-router-dom";
import {Room} from "../../interfaces/RoomInterfaces.ts";
import SignalRService from "../../services/SignalRService.ts";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import '../../style/Lobby/Buttons.css';
import '../../style/Lobby/Main.css';
import {checkForAdmin, getRoom} from "../../services/RoomService.ts";

export default function Lobby() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const roomId = queryParams.get('id')!;
    const [room, setRoom] = useState<Room>()
    const [isAdmin, setIsAdmin] = useState<boolean>(false)
    const signalR = useMemo(() => new SignalRService(), []);
    const navigate = useNavigate();

    useEffect(() =>{
        const fetchAdminStatus = async () => {
            const isAdmin = await checkForAdmin();
            setIsAdmin(isAdmin);
        }

        const connectToRoom = async () => {
            await signalR.startConnection();
            await signalR.invoke<string, Room>("JoinRoom", roomId)
            const room = await getRoom()
            setRoom(room)
        };

        connectToRoom();
        fetchAdminStatus();
    }, [roomId, signalR]);

    const quitRoom = async () => {
        const response = await signalR.invoke<string, Room>("QuitRoom", roomId)
        console.log(response)
        await signalR.stopConnection()
        navigate('/competitions');
    }

    const startCompetition = async () => {
        if (room)
            await signalR.invoke<Room, Room>("StartCompetition", room)
        else
            console.log("Can't start competition. Room doesn't exist.")
    }

    if (!room)
        return null;

    return (
        <div className="menu-page">
            <BaseNavBar/>
            <div className="content-wrapper">
                <h1>{room.name} lobby</h1>
                <h3>Competitive time: {room.time.split(':')[1]} min</h3>
                <div className="users-container">
                    {room.users.map((user) => (
                        <div key={user.email} className="user-item">{user.name}</div>
                    ))}
                </div>
                <div className="btn-container">
                    <button className="lobby-btn quit-room-btn" onClick={quitRoom}>Quit Room</button>
                    {isAdmin &&
                        <button className="lobby-btn start-competition-btn" onClick={startCompetition}>Start Competition</button>}
                </div>
            </div>
        </div>
    );
};
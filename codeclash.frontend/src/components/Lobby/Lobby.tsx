import React, {useContext, useEffect, useMemo, useState} from 'react';
import {getRoomsList, RoomMethods, RoomService, StartCompetitionData} from "../../services/roomService.ts";
import {useLocation, useNavigate} from "react-router-dom";
import {Room} from "../../interfaces/roomInterfaces.ts";
import {RoomServiceContext, useRoomService} from "../RoomServiceContext.tsx";
import {HubConnectionState} from "@microsoft/signalr";
import {useSignalR} from "../SignalRContext.tsx";
import SignalRService from "../SignalRService.ts";
import {checkForAdmin} from "../RoomService.ts";

export default function Lobby() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const roomId = queryParams.get('roomId')!;
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
            const room = await signalR.invoke<string, Room>("JoinRoom", roomId)
            setRoom(room)
        };

        connectToRoom();
        fetchAdminStatus();
    }, [roomId, signalR]);

    const quitRoom = async () => {
        const response = await signalR.invoke<string, Room>("QuitRoom", roomId)
        console.log(response)
        await signalR.stopConnection()
        navigate('/menu');
    }

    const startCompetition = async () => {
        if (room)
            await signalR.invoke<Room, Room>("StartCompetition", room)
        else
            console.log("Can't start competition. Room doesn't exist.")
    }

    return (
        <div style={{padding: '20px'}}>
            <h1>Lobby</h1>
            <button style={{padding: '10px 20px'}} onClick={quitRoom}>Quit Room</button>
            {isAdmin && <button style={{padding: '10px 20px'}} onClick={startCompetition}>Start Competition</button>}
        </div>
    );
};
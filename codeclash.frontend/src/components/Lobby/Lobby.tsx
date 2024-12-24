import React, {useEffect, useState} from 'react';
import {getRoomsList, RoomMethods, RoomService} from "../../services/roomService.ts";
import {useLocation} from "react-router-dom";
import {Room} from "../../interfaces/roomInterfaces.ts";

export default function Lobby() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const roomId = queryParams.get('roomId')!;
    const [room, setRoom] = useState<Room>()
    const roomService = new RoomService();

    useEffect(() => {
        async function fetchRoom() {
            await roomService.startConnection();
            const room = await roomService.actionWithRoom(roomId, RoomMethods.JoinRoom)
            setRoom(room)
        }

        fetchRoom();
    }, [])
    console.log(room)

    const quitRoom = async () => {
        await roomService.actionWithRoom(roomId, RoomMethods.QuitRoom)
        window.location.href = '/menu';
    }

    return (
        <div style={{padding: '20px'}}>
            <h1>Lobby</h1>
            <button style={{padding: '10px 20px'}} onClick={quitRoom}>Quit Room</button>
            <button style={{padding: '10px 20px'}}>Create Room</button>
        </div>
    );
};
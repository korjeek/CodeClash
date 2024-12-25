import React, {useContext, useEffect, useMemo, useState} from 'react';
import {getRoomsList, RoomMethods, RoomService, StartCompetitionData} from "../../services/roomService.ts";
import {useLocation} from "react-router-dom";
import {Room} from "../../interfaces/roomInterfaces.ts";
import {RoomServiceContext, useRoomService} from "../RoomServiceContext.tsx";
import {HubConnectionState} from "@microsoft/signalr";

export default function Lobby() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const roomId = queryParams.get('roomId')!;
    const [room, setRoom] = useState<Room>()
    const [admin, setAdmin] = useState<boolean>(false)
    const roomService = useRoomService();

    useEffect(() =>{
        const CheckUserForAdmin = async () => {
            const isUserAdmin = await roomService.checkIsUserAdmin();
            setAdmin(isUserAdmin);
        }
        const connectToRoom = async () => {
            await roomService.startConnection();
            const room = await roomService.actionWithRoom<string>(roomId, RoomMethods.JoinRoom)
            setRoom(room)
        };

        if (!roomService)
            connectToRoom();
        CheckUserForAdmin();
    }, [roomId, roomService]);

    const quitRoom = async () => {
        await roomService.actionWithRoom<string>(roomId, RoomMethods.QuitRoom)
        await roomService.closeConnection();
    }

    const startCompetition = async () => {
        const id = room!.id
        const time = room!.time
        await roomService.actionWithRoom<StartCompetitionData>({id, time}, RoomMethods.StartCompetition)
    }

    return (
        <div style={{padding: '20px'}}>
            <h1>Lobby</h1>
            <button style={{padding: '10px 20px'}} onClick={quitRoom}>Quit Room</button>
            {admin && <button style={{padding: '10px 20px'}}>Start Competition</button>}
        </div>
    );
};
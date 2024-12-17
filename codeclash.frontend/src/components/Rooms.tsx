import { Room } from "../interfaces/roomInterfaces.ts";
import {getRoomsList} from "../services/roomService.ts";

/*interface Room {
    key: number;
    Name: string;
    host: string;
    connected: number;
    total: number;
}*/

interface RoomsProp {
    Rooms: Room[];
}

export default function Rooms() {
    
    return (
        <div id="roomsScroll">
            {rooms.map((room) => (
                <div className="roomListItem">
                    <div>{room.id}</div>
                    <div style={{ "flex": "right"}}>Task: {room.issue.id}</div>
                    <div style={{ "flex": "right"}}>People: {room.participants.length}</div>
                </div>
            ))}
        </div>
    );
}
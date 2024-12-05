import { Room } from "../interfaces/roomInterfaces.ts";

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

export default function Rooms ({Rooms} : RoomsProp) {
    return (
        <div id="roomsScroll">
            {Rooms.map((room) => (
                <div className="roomListItem">
                    <div>{room.id}</div>
                    <div style={{ "flex": "right"}}>Task: {room.issue.id}</div>
                    <div style={{ "flex": "right"}}>People: {room.participants.length}</div>
                </div>
            ))}
        </div>
    );
}
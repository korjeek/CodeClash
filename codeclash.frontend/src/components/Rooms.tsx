interface Room {
    key: number;
    Name: string;
    host: string;
    connected: number;
    total: number;
}

interface RoomsProp {
    Rooms: Room[];
}

export default function Rooms ({Rooms} : RoomsProp) {
    return (
        <div id="roomsScroll">
            {Rooms.map((room) => (
                <div className="roomListItem">
                    <div>{room.Name}</div>
                    <div style={{ "flex": "right"}}>Host: {room.host}</div>
                    <div style={{ "flex": "right"}}>People: {room.connected}/{room.total}</div>
                </div>
            ))}
        </div>
    );
}
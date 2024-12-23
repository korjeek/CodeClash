import React, {useEffect, useState} from "react";
import '../../style/MenuPage/Main.css'
import '../../style/MenuPage/Input.css'
import '../../style/MenuPage/Buttons.css'
import {Room} from "../../interfaces/roomInterfaces.ts";
import {getRoomsList} from "../../services/roomService.ts";


export default function Menu() {
    const [rooms, setRooms] = useState([]);

    useEffect(() => {
        async function fetchRooms() {
            const roomsList = await getRoomsList();
            setRooms(roomsList);
        }

        fetchRooms();
    }, [])

    return (
        <div className="menu-page">
            <div className="menu nav-bar"></div>
            <div className="content-wrapper">
                <div className="grid-container">
                    <div className="item item-1">
                        <button className="create-room-btn">Create Room +</button>
                    </div>
                    <div className="item item-2">
                        <div className="input-wrapper">
                            <input
                                type="text"
                                name="room-id"
                                id="room-id"
                                placeholder="Find by ID"
                            />
                        </div>
                    </div>
                    <div className="item item-3">
                        <div className="content-container"></div>
                    </div>
                    <div className="item item-4">
                        <div className="rooms-container">
                            {rooms.map((room: Room) => (
                                <div className="room-item" key={room.id}>
                                    <div>{room.id}</div>
                                    <div style={{"flex": "right"}}>Name: {room.name}</div>
                                    <div style={{"flex": "right"}}>People: {room.users.length}</div>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
};





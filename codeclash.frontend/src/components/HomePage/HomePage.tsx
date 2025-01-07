import {useEffect, useState} from "react";
import '../../style/HomePage/Main.css'
import '../../style/HomePage/Input.css'
import '../../style/HomePage/Buttons.css'
import '../../style/HomePage/Rooms.css'
import '../../style/Default/AuthNavBar.css'
import '../../style/Default/BaseNavBar.css'
import {Room} from "../../interfaces/RoomInterfaces.ts";
import {useNavigate} from "react-router-dom";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import {getRoomsList} from "../../services/RoomService.ts";


export default function HomePage() {
    const [rooms, setRooms] = useState<Room[]>([]);
    const [pages, setPages] = useState<number[]>([1]);
    const [search, setSearch] = useState<string>('');
    const [currentPage, setCurrentPage] = useState<number>(1);
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchRooms() {
            const roomsList = await getRoomsList();
            const paginationRange = await getPaginationRange(currentPage, Math.ceil(roomsList.length / 6));
            setRooms(roomsList);
            setPages(paginationRange);
        }

        fetchRooms();
    }, [currentPage])

    const joinRoom = async (roomId: string) => navigate(`/lobby?id=${roomId}`);
    const createRoom = async () => navigate(`/create-competition`);

    return (
        <div className="menu-page">
            <BaseNavBar/>
            <div className="content-wrapper">
                <div className="grid-container">
                    <div className="item item-1">
                        <button className="create-room-btn" onClick={createRoom}>Create Room +</button>
                    </div>
                    <div className="item item-2">
                        <div className="input-wrapper">
                            <input
                                type="text"
                                name="room-id"
                                id="room-id"
                                placeholder="Find room by id or name"
                                onChange={(e) => setSearch(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="item item-3">
                        <div className="content-container"></div>
                    </div>
                    <div className="item item-4">
                        <div className="rooms-container">
                            {rooms.filter((room) => searchRoom(room, search)).map((room) => (
                                <button className="room-item" key={room.id} onClick={() => joinRoom(room.id)}>
                                    <div>{room.id}</div>
                                    <div style={{"flex": "right"}}>Name: {room.name}</div>
                                    <div style={{"flex": "right"}}>People: {room.users?.length ?? 0}</div>
                                </button>
                            ))}
                        </div>
                    </div>
                    <div className="item item-5">
                        <div className="pagination-container">
                            {pages.map((page) => (
                                <button className="page-button" key={page} onClick={
                                    () => setCurrentPage(page)}>
                                    {page}
                                </button>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
};

async function getPaginationRange(currentPage: number, totalPages: number){
    let startPage = Math.max(currentPage - 2, 1);
    let endPage = Math.min(currentPage + 2, totalPages);

    if (startPage === 1) {
        endPage = Math.min(5, totalPages);
    }

    if (endPage === totalPages) {
        startPage = Math.max(totalPages - 4, 1);
    }

    return Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
}

function searchRoom(room: Room, search: string){
    return search.toLowerCase() === '' ? room : room.name.toLowerCase().includes(search)
}





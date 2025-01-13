import {Room} from "../interfaces/RoomInterfaces.ts";

export async function getPaginationRange(currentPage: number, totalPages: number){
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

export function searchRoom(room: Room, search: string){
    return search.toLowerCase() === '' ? room :
        (room.name.toLowerCase().includes(search) || room.id.toLowerCase().includes(search))
}
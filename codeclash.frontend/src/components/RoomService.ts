import axios from 'axios';
import {Issue} from "../interfaces/issueInterfaces.ts";
import {Room} from "../interfaces/roomInterfaces.ts";

const API_URL = 'https://localhost:7282/rooms';

export const checkForAdmin = async (): Promise<boolean> => {
    const response = await axios.get(`${API_URL}/check-for-admin`, { withCredentials: true });
    return response.data;
};

export const getRoom = async (): Promise<Room> => {
    const response = await axios.get(`${API_URL}/get-room-info`, { withCredentials: true });
    return response.data.data;
}

import axios from 'axios';
import {Room} from "../interfaces/RoomInterfaces.ts";
import apiClient from "./InterceptionService.ts";

const API_URL = 'https://localhost:7282/rooms';

export const checkForAdmin = async (): Promise<boolean> => {
    const response = await axios.get(`${API_URL}/check-for-admin`, { withCredentials: true });
    return response.data.data;
};

export const getRoom = async (): Promise<Room> => {
    const response = await axios.get(`${API_URL}/get-room-info`, { withCredentials: true });
    return response.data.data;
}

export const getRoomsList = async (): Promise<Room[]> => {
    const response = await apiClient.get(`${API_URL}/get-rooms`, { withCredentials: true });
    return response.data.data;
}

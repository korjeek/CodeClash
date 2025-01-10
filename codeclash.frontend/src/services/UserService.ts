import axios from 'axios';
import {RoomStatus} from "../interfaces/UserInterfaces.ts";

const API_URL = 'https://localhost:7282/user';

export const getRoomStatus = async (): Promise<RoomStatus> => {
    const response = await axios.get(`${API_URL}/get-user-state`, { withCredentials: true });
    return response.data.data;
};
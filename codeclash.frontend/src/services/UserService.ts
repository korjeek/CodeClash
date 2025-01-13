import {AxiosInstance} from 'axios';
import {RoomStatus} from "../interfaces/UserInterfaces.ts";

const API_URL = 'https://localhost:7282/user';

export const getRoomStatus = async (axiosPrivate: AxiosInstance): Promise<RoomStatus> => {
    const response = await axiosPrivate.get(`${API_URL}/get-user-state`, { withCredentials: true });
    return response.data.data;
};
import {Room} from "../interfaces/RoomInterfaces.ts";
import {AxiosInstance} from "axios";

const API_URL = '/rooms';
const controller = new AbortController();

export const checkForAdmin = async (axiosPrivate: AxiosInstance): Promise<boolean> => {
    const response = await axiosPrivate.get(`${API_URL}/check-for-admin`,
        {
            signal: controller.signal,
            withCredentials: true
        });
    return response.data.data;
};

export const getRoom = async (axiosPrivate: AxiosInstance): Promise<Room> => {
    const response = await axiosPrivate.get(`${API_URL}/get-room-info`,
        {
            signal: controller.signal,
            withCredentials: true
        });
    return response.data.data;
}

export const getRoomsList = async (axiosPrivate: AxiosInstance): Promise<Room[]> => {
    const response = await axiosPrivate.get(`${API_URL}/get-rooms`,
        {
            signal: controller.signal,
            withCredentials: true
        });
    return response.data.data;
}

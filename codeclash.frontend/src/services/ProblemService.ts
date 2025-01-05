import {Issue} from "../interfaces/issueInterfaces.ts";
import axios from "axios";

const API_URL = 'https://localhost:7282/issue';

export const getProblems = async (): Promise<Issue[]> => {
    const response = await axios.get(`${API_URL}/get-issues`, { withCredentials: true })
    return response.data
}
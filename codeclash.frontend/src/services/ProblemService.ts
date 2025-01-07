import {Issue} from "../interfaces/IssueInterfaces.ts";
import axios from "axios";

const API_URL = 'https://localhost:7282/issue';

export const getAllProblems = async (): Promise<Issue[]> => {
    const response = await axios.get(`${API_URL}/get-all-issues`, { withCredentials: true });
    return response.data
}

export const getProblem = async (): Promise<Issue> => {
    const response = await axios.get(`${API_URL}/get-issue`, { withCredentials: true });
    return response.data.data;
}
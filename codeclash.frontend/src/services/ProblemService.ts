import {Issue} from "../interfaces/IssueInterfaces.ts";
import axios from "axios";

const API_URL = 'https://localhost:7282/issue';

export const getAllProblems = async (): Promise<Issue[]> => {
    const response = await axios.get(`${API_URL}/get-all-issues`, { withCredentials: true });
    return response.data
}

export const getProblem = async (issueId: string): Promise<Issue> => {
    const response = await axios.post(`${API_URL}/get-issue`, issueId, { withCredentials: true });
    console.log(response.data);
    return response.data.data;
}
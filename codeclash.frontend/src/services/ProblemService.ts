import {Issue} from "../interfaces/IssueInterfaces.ts";
import axios from "axios";

const API_URL = 'https://localhost:7282/issue';

export const getAllProblems = async (): Promise<Issue[]> => {
    const response = await axios.get(`${API_URL}/get-all-issues`, { withCredentials: true });
    return response.data
}

export const getProblem = async (id: string): Promise<Issue> => {
    const issue = {id: id, name: '', description: '', initialCode: '' };
    console.log(issue)
    const response = await axios.post(`${API_URL}/get-issue`, issue, { withCredentials: true });
    console.log(response.data);
    return response.data.data;
}


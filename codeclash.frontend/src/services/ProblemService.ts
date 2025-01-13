import {Issue} from "../interfaces/IssueInterfaces.ts";
import {AxiosInstance} from "axios";

const API_URL = 'http://localhost:5099/api/issue';

export const getAllProblems = async (axiosPrivate: AxiosInstance): Promise<Issue[]> => {
    const response = await axiosPrivate.get(`${API_URL}/get-all-issues`, { withCredentials: true });
    return response.data
}

export const getProblem = async (axiosPrivate: AxiosInstance, id: string): Promise<Issue> => {
    const issue = {id: id, name: '', description: '', initialCode: '' };
    const response = await axiosPrivate.post(`${API_URL}/get-issue`, issue, { withCredentials: true });
    return response.data.data;
}


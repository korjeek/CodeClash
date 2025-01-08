import axios from "axios";
import {CheckSolutionRequest} from "../interfaces/CompetitionInterfaces.ts";

const API_URL = 'https://localhost:7282/competition';

export const sendCode = async (solution: CheckSolutionRequest): Promise<void> => {
    const response = await axios.post(`${API_URL}/check-solution`, solution, { withCredentials: true });
    return response.data;
}
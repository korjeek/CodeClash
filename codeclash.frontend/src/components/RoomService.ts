import axios from 'axios';

const API_URL = 'https://localhost:7282/rooms';

export const checkForAdmin = async (): Promise<boolean> => {
    const response = await axios.get(`${API_URL}/check-for-admin`, { withCredentials: true });
    return response.data;
};

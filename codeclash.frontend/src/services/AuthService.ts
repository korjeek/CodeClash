import axios from 'axios';
import {LoginUser, RegisterUser} from "../interfaces/AuthInterfaces.ts";

const API_URL = 'https://localhost:7282/auth';

export const register = async (userData: RegisterUser) => {
  const response = await axios.post(`${API_URL}/register`, userData, { withCredentials: true });
  return response.data;
};

export const login = async (userData: LoginUser) => {
  const response = await axios.post(`${API_URL}/login`, userData, { withCredentials: true });
  return response.data;
};

import axios from '../api/axios.ts'
import {LoginResponse, LoginUser, RegisterUser} from "../interfaces/AuthInterfaces.ts";

const API_ROUTE = '/auth';

export const register = async (userData: RegisterUser) => {
  const response = await axios.post(`${API_ROUTE}/register`, userData,
      {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true
      });
  return response.data;
};

export const login = async (userData: LoginUser): Promise<LoginResponse> => {
  const response = await axios.post(`${API_ROUTE}/login`, userData,
      {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true
      });
  return response.data;
};

// src/services/authService.ts
import axios from 'axios';

const API_URL = 'https://localhost:7282/auth';

export interface RegisterUser {
  username: string;
  email: string;
  password: string;
  passwordConfirm: string;
}

export interface LoginUser {
  email: string;
  password: string;
}

export const register = async (userData: RegisterUser) => {
  const response = await axios.post(`${API_URL}/register`, userData, { withCredentials: true });
  return response.data;
};

export const login = async (userData: LoginUser) => {
  const response = await axios.post(`${API_URL}/login`, userData, { withCredentials: true });
  return response.data;
};

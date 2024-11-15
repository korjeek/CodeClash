// src/services/authService.ts
import axios from 'axios';

const API_URL = 'http://localhost:5099/auth';

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
  const response = await axios.post(`${API_URL}/register`, userData);
  return response.data;
};

export const login = async (userData: LoginUser) => {
  const response = await axios.post(`${API_URL}/login`, userData);
  return response.data;
};

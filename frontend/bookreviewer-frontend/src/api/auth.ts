import { http } from "./http";
import type { LoginResponse, UserMe } from "../models/auth";


export async function login(username: string, password: string): Promise<LoginResponse> {
  const response = await http.post<LoginResponse>("/Auth/login", {
    username,
    password
  });
  return response.data;
}


export async function getMe(): Promise<UserMe> {
  const response = await http.get<UserMe>("/Auth/me");
  return response.data;
}
export interface LoginResponse {
  token: string;
}

export interface UserMe {
  id: string;
  username: string;
  role: string;
}
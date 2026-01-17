import axios from "axios";

export const http = axios.create({
  baseURL: "https://localhost:7251/api",
});

http.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  console.log("Wysyłam token:", token); // Sprawdź, czy to nie jest null!
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

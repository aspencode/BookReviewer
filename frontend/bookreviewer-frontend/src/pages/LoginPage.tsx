import { useState } from "react";
import { login } from "../api/auth";
import { useNavigate } from "react-router-dom";

export function LoginPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const data = await login(username, password);
      localStorage.setItem("token", data.token); // Zapisujemy czysty token
      alert("Zalogowano pomyślnie!");
      navigate("/");
    } catch (err: unknown) {
      const errorMessage = err instanceof Error ? err.message : "Nieprawidłowe dane";
      alert("Błąd: " + errorMessage);
    }
  };

  return (
    <div style={{ maxWidth: "300px", margin: "2rem auto" }}>
      <h2>Logowanie</h2>
      <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
        <input placeholder="Użytkownik" onChange={e => setUsername(e.target.value)} required />
        <input type="password" placeholder="Hasło" onChange={e => setPassword(e.target.value)} required />
        <button type="submit">Zaloguj</button>
      </form>
    </div>
  );
}
import { useState } from "react";
import { createAuthor } from "../api/authors";
import { useNavigate } from "react-router-dom";

export function AddAuthorPage() {
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createAuthor(name);
      alert("Autor dodany!");
      navigate("/"); // Powrót do listy
    } catch (err: unknown) {
      if (err instanceof Error && err.message.includes("401")) {
        alert("Musisz być zalogowano, aby dodać autora!");
      } else {
        alert("Wystąpił błąd podczas dodawania.");
      }
    }
  };

  return (
    <div>
      <h2>Dodaj nowego autora</h2>
      <form onSubmit={handleSubmit}>
        <input 
          placeholder="Imię i nazwisko" 
          value={name} 
          onChange={e => setName(e.target.value)} 
          required 
        />
        <button type="submit">Zapisz</button>
      </form>
    </div>
  );
}
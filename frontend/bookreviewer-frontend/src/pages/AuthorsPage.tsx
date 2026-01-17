import { useEffect, useState } from "react";
import { getAuthors } from "../api/authors";
import type { AuthorListDto } from "../models/authors";

export function AuthorsPage() {
  const [authors, setAuthors] = useState<AuthorListDto[]>([]);

useEffect(() => {
  getAuthors().then(data => {
    console.log("Dane z API:", data); // To pokaże nam prawdę w konsoli F12
    setAuthors(data);
  }).catch(err => {
    console.error("Błąd pobierania:", err);
  });
}, []);

  return (
    <div>
      <h1>Authors</h1>
      <ul>
        {authors.map(a => (
          <li key={a.id}>{a.name}</li>
        ))}
      </ul>
    </div>
  );
}


import { Routes, Route, Link } from "react-router-dom";
import { AuthorsPage } from "./pages/AuthorsPage";
import { LoginPage } from "./pages/LoginPage";
import { AddAuthorPage } from "./pages/AddAuthorPage";

function App() {
  return (
    <div className="App">
      {/*  */}
      <nav style={{ padding: "1rem", background: "#f4f4f4", marginBottom: "1rem" }}>
        <Link to="/authors" style={{ marginRight: "1rem" }}>Autorzy</Link>
        <Link to="/add-author">Dodaj autora</Link>
        <Link to="/login">Zaloguj</Link>
      </nav>

      <main>
        <Routes>
          <Route path="/authors" element={<AuthorsPage />} />
          <Route path="/add-author" element={<AddAuthorPage />} />
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </main>
    </div>
  );
}

export default App;
import { useState } from "react";
import api from "../api/axios";
import { useNavigate } from "react-router-dom";

export default function Upload() {
  const [file, setFile] = useState(null);
  const navigate = useNavigate();

  const submit = async e => {
    e.preventDefault();
    const form = new FormData();
    form.append("File", file);
    form.append("Usuario", "frontend");

    await api.post("/control/control/carga/crear", form);
    navigate("/history");
  };

  return (
    <form onSubmit={submit}>
      <input type="file" accept=".xlsx" onChange={e => setFile(e.target.files[0])} />
      <button type="submit">Subir</button>
    </form>
  );
}
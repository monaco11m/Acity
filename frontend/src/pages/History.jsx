import { useEffect, useState } from "react";
import api from "../api/axios";
import { Link } from "react-router-dom";

export default function History() {
  const [items, setItems] = useState([]);

  useEffect(() => {
    api.get("/control/carga/listar").then(r => setItems(r.data));
  }, []);

  return (
    <ul>
      {items.map(i => (
        <li key={i.id}>
          {i.nombreArchivo} - {i.estado}
          <Link to={`/detail/${i.id}`}>Ver</Link>
        </li>
      ))}
    </ul>
  );
}
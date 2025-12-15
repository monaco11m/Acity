import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../api/axios";

export default function Detail() {
  const { id } = useParams();
  const [item, setItem] = useState(null);

  useEffect(() => {
    const i = setInterval(() => {
      api.get(`/control/carga/${id}`).then(r => setItem(r.data));
    }, 3000);
    return () => clearInterval(i);
  }, [id]);

  if (!item) return null;

  return (
    <div>
      <p>Archivo: {item.nombreArchivo}</p>
      <p>Estado: {item.estado}</p>
    </div>
  );
}
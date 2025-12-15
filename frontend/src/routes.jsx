import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Upload from "./pages/Upload";
import History from "./pages/History";
import Detail from "./pages/Detail";
import { useAuth } from "./auth/AuthContext";

function Private({ children }) {
  const { token } = useAuth();
  return token ? children : <Navigate to="/" />;
}

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/upload" element={<Private><Upload /></Private>} />
      <Route path="/history" element={<Private><History /></Private>} />
      <Route path="/detail/:id" element={<Private><Detail /></Private>} />
    </Routes>
  );
}
import { Outlet } from "react-router-dom";
import { colors } from "../theme/colors";
import Sidebar from "../components/Sidebar";

export default function AdminLayout() {
  return (
    <div className="flex min-h-screen">
      <Sidebar />

      <div
        className="flex-1 p-6"
        style={{ backgroundColor: colors.background, color: colors.text }}
      >
        <Outlet />
      </div>
    </div>
  );
}
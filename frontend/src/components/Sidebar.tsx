import { NavLink } from "react-router-dom";
import { colors } from "../theme/colors";

export default function Sidebar() {
  const linkClass = ({ isActive }: any) =>
    `block px-4 py-2 rounded-lg mb-2 ${isActive ? "font-semibold" : ""}`;

  return (
    <div className="w-64 p-4" style={{ backgroundColor: colors.surface }}>
      <h2 className="text-xl font-bold mb-6" style={{ color: colors.text }}>
        Admin Panel
      </h2>

      <NavLink
        to="/dashboard"
        className={linkClass}
        style={({ isActive }) => ({
          color: isActive ? colors.primary : colors.text,
        })}
      >
        Dashboard
      </NavLink>

      <p
        className="mt-4 mb-2 text-sm opacity-70"
        style={{ color: colors.text }}
      >
        User Management
      </p>

      <NavLink
        to="/admin/users"
        className={linkClass}
        style={({ isActive }) => ({
          color: isActive ? colors.primary : colors.text,
        })}
      >
        Users
      </NavLink>

      <NavLink
        to="/admin/roles"
        className={linkClass}
        style={({ isActive }) => ({
          color: isActive ? colors.primary : colors.text,
        })}
      >
        Roles
      </NavLink>

      <NavLink
        to="/admin/permissions"
        className={linkClass}
        style={({ isActive }) => ({
          color: isActive ? colors.primary : colors.text,
        })}
      >
        Permissions
      </NavLink>

    </div>
  );
}

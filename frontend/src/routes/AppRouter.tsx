import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "../pages/Login";
import Dashboard from "../pages/Dashboard";
import AdminLayout from "../pages/AdminLayout";
import UsersPage from "../pages/admin/UsersPage";
import RolesPage from "../pages/admin/RolesPage";
import PermissionsPage from "../pages/admin/PermissionsPage";
import BranchesPage from "../pages/admin/BranchesPage";
import SystemsPage from "../pages/admin/SystemsPage";
import TicketsPage from "../pages/admin/TicketsPage";
import ReportsPage from "../pages/admin/ReportsPage";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/admin" element={<AdminLayout />}>
          <Route path="users" element={<UsersPage />} />
          <Route path="roles" element={<RolesPage />} />
          <Route path="permissions" element={<PermissionsPage />} />
          <Route path="branches" element={<BranchesPage />} />
          <Route path="systems" element={<SystemsPage />} />
          <Route path="/admin/tickets" element={<TicketsPage />} />
          <Route path="/admin/reports" element={<ReportsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
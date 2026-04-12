import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import { getAllUsers, type GetAllUsersResponse } from "../../api/user.api";
import { assignRole } from "../../api/role.api";
import Modal from "../../components/Modal";
import toast from "react-hot-toast";
import { assignPermissionToUser } from "../../api/permission.api";
import { assignSystem, getSystems } from "../../api/system.api";

export default function UsersPage() {
  const [users, setUsers] = useState<GetAllUsersResponse[]>([]);
  const [selectedUser, setSelectedUser] = useState<GetAllUsersResponse | null>(
    null,
  );
  const [open, setOpen] = useState(false);
  const [roleName, setRoleName] = useState("");
  const [permissionName, setPermissionName] = useState("");
  const [systems, setSystems] = useState<any[]>([]);
  const [selectedSystemId, setSelectedSystemId] = useState("");
  const [activeTab, setActiveTab] = useState<"role" | "permission" | "system">(
    "role",
  );

  useEffect(() => {
    fetchUsers();
    fetchSystems();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getAllUsers(null);
      setUsers(data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load users");
    }
  };

  const fetchSystems = async () => {
    try {
      const data = await getSystems();
      setSystems(data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load systems");
    }
  };

  const handleAssignRole = async () => {
    if (!roleName) {
      toast.error("Enter a role");
      return;
    }

    if (!selectedUser) return;

    try {
      await assignRole({
        email: selectedUser.email,
        roleName: roleName,
      });

      toast.success("Role assigned successfully");

      setOpen(false);
      setRoleName("");
      fetchUsers();
    } catch (error) {
      console.error(error);
      toast.error("Failed to assign role");
    }
  };

  const handleAssignPermission = async () => {
    if (!permissionName) {
      toast.error("Enter permission");
      return;
    }

    if (!selectedUser) return;

    try {
      await assignPermissionToUser({
        email: selectedUser.email,
        permissionName: permissionName,
      });

      toast.success("Permission assigned");

      setPermissionName("");
      setOpen(false);
      fetchUsers();
    } catch (error) {
      console.error(error);
      toast.error("Failed to assign permission");
    }
  };

  const handleAssignSystem = async () => {
    if (!selectedSystemId) {
      toast.error("Select a system");
      return;
    }

    if (!selectedUser) return;

    try {
      await assignSystem({
        systemId: Number(selectedSystemId),
        userId: selectedUser.id,
      });

      toast.success("System assigned");

      setSelectedSystemId("");
      setOpen(false);
    } catch (error) {
      console.error(error);
      toast.error("Failed to assign system");
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Users</h1>

      <div
        className="rounded-xl p-4"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <table className="w-full text-left">
          <thead>
            <tr>
              <th>Email</th>
              <th>Roles</th>
              <th>Actions</th>
            </tr>
          </thead>

          <tbody>
            {users.map((user, index) => (
              <tr key={index} className="border-t">
                <td>{user.email}</td>
                <td>{user.roles?.join(", ") || "N/A"}</td>
                <td>
                  <button
                    className="text-green-400"
                    onClick={() => {
                      setSelectedUser(user);
                      setOpen(true);
                    }}
                  >
                    Manage
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <Modal
        isOpen={open}
        onClose={() => {
          setOpen(false);
          setRoleName("");
          setPermissionName("");
          setSelectedSystemId("");
          setActiveTab("role");
        }}
      >
        <h2 className="text-xl font-bold mb-4">Manage User</h2>

        <p className="mb-4 text-sm opacity-70">{selectedUser?.email}</p>

        {/* TABS */}
        <div className="flex mb-4 border-b border-gray-600">
          {["role", "permission", "system"].map((tab) => (
            <button
              key={tab}
              onClick={() => setActiveTab(tab as any)}
              className={`flex-1 p-2 text-sm ${
                activeTab === tab
                  ? "border-b-2 border-green-500 font-semibold"
                  : ""
              }`}
            >
              {tab.toUpperCase()}
            </button>
          ))}
        </div>

        {/* ROLE TAB */}
        {activeTab === "role" && (
          <div>
            <input
              placeholder="admin / manager"
              value={roleName}
              onChange={(e) => setRoleName(e.target.value)}
              className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
            />

            <button
              className="w-full bg-green-600 p-2 rounded"
              onClick={handleAssignRole}
            >
              Assign Role
            </button>
          </div>
        )}

        {/* PERMISSION TAB */}
        {activeTab === "permission" && (
          <div>
            <input
              placeholder="CREATE_TICKET"
              value={permissionName}
              onChange={(e) => setPermissionName(e.target.value)}
              className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
            />

            <button
              className="w-full bg-blue-600 p-2 rounded"
              onClick={handleAssignPermission}
            >
              Assign Permission
            </button>
          </div>
        )}

        {/* SYSTEM TAB */}
        {activeTab === "system" && (
          <div>
            <select
              value={selectedSystemId}
              onChange={(e) => setSelectedSystemId(e.target.value)}
              className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
            >
              <option value="">Select system</option>

              {systems.map((sys) => (
                <option key={sys.systemId} value={sys.systemId}>
                  {sys.systemName}
                </option>
              ))}
            </select>

            <button
              className="w-full bg-purple-600 p-2 rounded"
              onClick={handleAssignSystem}
            >
              Assign System
            </button>
          </div>
        )}
      </Modal>
    </div>
  );
}

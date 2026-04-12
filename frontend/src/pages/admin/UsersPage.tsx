import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import {
  getAllUsers,
  type GetAllUsersResponse,
} from "../../api/user.api";
import { assignRole } from "../../api/role.api";
import Modal from "../../components/Modal";
import toast from "react-hot-toast";
import { assignPermissionToUser } from "../../api/permission.api";

export default function UsersPage() {
  const [users, setUsers] = useState<GetAllUsersResponse[]>([]);
  const [selectedUser, setSelectedUser] = useState<GetAllUsersResponse | null>(null);
  const [open, setOpen] = useState(false);
  const [roleName, setRoleName] = useState("");
  const [permissionName, setPermissionName] = useState("");

  useEffect(() => {
    fetchUsers();
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

      {/* MODAL */}
      <Modal
        isOpen={open}
        onClose={() => {
          setOpen(false);
          setRoleName("");
        }}
      >
        <h2 className="text-xl font-bold mb-4">Assign Role</h2>

        <p className="mb-2 text-sm opacity-70">
          {selectedUser?.email}
        </p>

        {/* INPUT */}
        <input
          placeholder="Enter role (admin / manager)"
          value={roleName}
          onChange={(e) => setRoleName(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        {/* BUTTON */}
        <button
          className="w-full bg-green-600 p-2 rounded hover:bg-green-700 transition"
          onClick={handleAssignRole}
        >
          Assign Role
        </button>
      </Modal>
      <Modal
  isOpen={open}
  onClose={() => {
    setOpen(false);
    setRoleName("");
    setPermissionName("");
  }}
>
  <h2 className="text-xl font-bold mb-4">Manage User</h2>

  <p className="mb-3 text-sm opacity-70">
    {selectedUser?.email}
  </p>

  {/* ASSIGN ROLE */}
  <div className="mb-4">
    <p className="text-sm mb-1">Assign Role</p>

    <input
      placeholder="admin / manager"
      value={roleName}
      onChange={(e) => setRoleName(e.target.value)}
      className="w-full p-2 mb-2 rounded bg-gray-800 text-white"
    />

    <button
      className="w-full bg-green-600 p-2 rounded"
      onClick={handleAssignRole}
    >
      Assign Role
    </button>
  </div>

  {/* ASSIGN PERMISSION */}
  <div>
    <p className="text-sm mb-1">Assign Permission</p>

    <input
      placeholder="e.g. CREATE_TICKET"
      value={permissionName}
      onChange={(e) => setPermissionName(e.target.value)}
      className="w-full p-2 mb-2 rounded bg-gray-800 text-white"
    />

    <button
      className="w-full bg-blue-600 p-2 rounded"
      onClick={handleAssignPermission}
    >
      Assign Permission
    </button>
  </div>
</Modal>
    </div>
  );
}
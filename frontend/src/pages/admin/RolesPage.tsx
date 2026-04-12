import { useState } from "react";
import { colors } from "../../theme/colors";
import { createRole } from "../../api/role.api";
import toast from "react-hot-toast";

export default function RolesPage() {
  const [roleName, setRoleName] = useState("");
  const [loading, setLoading] = useState(false);

  const handleCreateRole = async () => {
    if (!roleName) {
      toast.error("Enter role name");
      return;
    }

    try {
      setLoading(true);

      await createRole({ roleName });

      toast.success("Role created");

      setRoleName("");
    } catch (error) {
      console.error(error);
      toast.error("Failed to create role");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Role Management</h1>

      <div
        className="max-w-md p-4 rounded-xl"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <p className="mb-2 text-sm">Create New Role</p>

        <input
          placeholder="Enter role name"
          value={roleName}
          onChange={(e) => setRoleName(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        <button
          onClick={handleCreateRole}
          disabled={loading}
          className="w-full bg-green-600 p-2 rounded hover:bg-green-700 transition"
        >
          {loading ? "Creating..." : "Create Role"}
        </button>
      </div>
    </div>
  );
}

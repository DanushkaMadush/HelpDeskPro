import { useState } from "react";
import { colors } from "../../theme/colors";
import { createPermission } from "../../api/permission.api";
import toast from "react-hot-toast";

export default function PermissionsPage() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(false);

  const handleCreatePermission = async () => {
    if (!name) {
      toast.error("Enter permission name");
      return;
    }

    try {
      setLoading(true);

      await createPermission({
        name,
        description,
      });

      toast.success("Permission created");

      setName("");
      setDescription("");
    } catch (error) {
      console.error(error);
      toast.error("Failed to create permission");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Permission Management</h1>

      <div
        className="max-w-md p-4 rounded-xl"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <p className="mb-2 text-sm">Create Permission</p>

        {/* NAME */}
        <input
          placeholder="e.g. CREATE_TICKET"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        {/* DESCRIPTION */}
        <input
          placeholder="Description (optional)"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        <button
          onClick={handleCreatePermission}
          disabled={loading}
          className="w-full bg-blue-600 p-2 rounded hover:bg-blue-700 transition"
        >
          {loading ? "Creating..." : "Create Permission"}
        </button>
      </div>
    </div>
  );
}

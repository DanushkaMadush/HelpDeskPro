import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import {
  getSystems,
  createSystem,
  type System,
} from "../../api/system.api";
import toast from "react-hot-toast";

export default function SystemsPage() {
  const [systems, setSystems] = useState<System[]>([]);
  const [systemName, setSystemName] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchSystems();
  }, []);

  const fetchSystems = async () => {
    try {
      const data = await getSystems();
      setSystems(data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load systems");
    }
  };

  const handleCreateSystem = async () => {
    if (!systemName) {
      toast.error("Enter system name");
      return;
    }

    try {
      setLoading(true);

      await createSystem({
        systemName,
        createdBy: "admin", // ⚠️ replace later with logged-in user
      });

      toast.success("System created");

      setSystemName("");
      fetchSystems();
    } catch (error) {
      console.error(error);
      toast.error("Failed to create system");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">System Management</h1>

      {/* CREATE */}
      <div
        className="max-w-md p-4 mb-6 rounded-xl"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <p className="mb-2 text-sm">Create System</p>

        <input
          placeholder="System name"
          value={systemName}
          onChange={(e) => setSystemName(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        <button
          onClick={handleCreateSystem}
          disabled={loading}
          className="w-full bg-purple-600 p-2 rounded hover:bg-purple-700 transition"
        >
          {loading ? "Creating..." : "Create System"}
        </button>
      </div>

      {/* LIST */}
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
              <th>ID</th>
              <th>System Name</th>
            </tr>
          </thead>

          <tbody>
            {systems.map((sys) => (
              <tr key={sys.systemId} className="border-t">
                <td>{sys.systemId}</td>
                <td>{sys.systemName}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
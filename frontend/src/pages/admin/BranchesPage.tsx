import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import {
  getBranches,
  createBranch,
  type Branch,
} from "../../api/branch.api";
import toast from "react-hot-toast";

export default function BranchesPage() {
  const [branches, setBranches] = useState<Branch[]>([]);
  const [branchName, setBranchName] = useState("");
  const [address, setAddress] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchBranches();
  }, []);

  const fetchBranches = async () => {
    try {
      const data = await getBranches();
      setBranches(data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load branches");
    }
  };

  const handleCreateBranch = async () => {
    if (!branchName || !address) {
      toast.error("Fill all fields");
      return;
    }

    try {
      setLoading(true);

      await createBranch({
        branchName,
        address,
        createdBy: "admin", //  replace later with real user
      });

      toast.success("Branch created");

      setBranchName("");
      setAddress("");

      fetchBranches();
    } catch (error) {
      console.error(error);
      toast.error("Failed to create branch");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Branch Management</h1>

      {/* CREATE */}
      <div
        className="max-w-md p-4 mb-6 rounded-xl"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <p className="mb-2 text-sm">Create Branch</p>

        <input
          placeholder="Branch name"
          value={branchName}
          onChange={(e) => setBranchName(e.target.value)}
          className="w-full p-2 mb-2 rounded bg-gray-800 text-white"
        />

        <input
          placeholder="Address"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          className="w-full p-2 mb-3 rounded bg-gray-800 text-white"
        />

        <button
          onClick={handleCreateBranch}
          disabled={loading}
          className="w-full bg-green-600 p-2 rounded hover:bg-green-700 transition"
        >
          {loading ? "Creating..." : "Create Branch"}
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
              <th>Name</th>
              <th>Address</th>
            </tr>
          </thead>

          <tbody>
            {branches.map((branch) => (
              <tr key={branch.branchId} className="border-t">
                <td>{branch.branchId}</td>
                <td>{branch.branchName}</td>
                <td>{branch.address}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import { getAllUsers, type GetAllUsersResponse } from "../../api/user.api.ts";

export default function UsersPage() {
  const [users, setUsers] = useState<GetAllUsersResponse[]>([]);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getAllUsers(null);
      setUsers(data);
    } catch (error) {
      console.error(error);
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
              <th>Role</th>
              <th>Actions</th>
            </tr>
          </thead>

          <tbody>
            {users.map((user, index) => (
              <tr key={index} className="border-t">
                <td>{user.email}</td>
                <td>{user.roles?.join(", ") || "N/A"}</td>
                <td>
                  <button className="text-green-400">
                    Manage
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
import { useEffect, useState } from "react";
import { colors } from "../../theme/colors";
import { getAllTickets, type Ticket } from "../../api/ticket.api";
import toast from "react-hot-toast";

export default function TicketsPage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const data = await getAllTickets();
      setTickets(data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load tickets");
    } finally {
      setLoading(false);
    }
  };

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case "pending":
        return colors.statusOpen;
      case "ongoing":
        return colors.statusInProgress;
      case "completed":
        return colors.statusResolved;
      default:
        return colors.border;
    }
  };

  if (loading) {
    return <div className="p-4">Loading tickets...</div>;
  }

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Ticket Management</h1>

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
              <th>Title</th>
              <th>Status</th>
              <th>Created</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => (
              <tr key={ticket.ticketId} className="border-t">
                <td>{ticket.ticketId}</td>
                <td>{ticket.title}</td>

                <td>
                  <span
                    className="px-2 py-1 rounded text-sm"
                    style={{
                      backgroundColor: getStatusColor(ticket.status!),
                    }}
                  >
                    {ticket.status}
                  </span>
                </td>

                <td>
                  {new Date(ticket.createdAt).toLocaleDateString()}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
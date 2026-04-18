import { useState } from "react";
import { colors } from "../../theme/colors";
import {
  getMonthlyTickets,
  getBranchWiseTickets,
  getPendingSystems,
} from "../../api/report.api";
import toast from "react-hot-toast";

export default function ReportsPage() {
  const [loading, setLoading] = useState(false);

  const downloadCSV = (data: any[], filename: string) => {
    if (!data.length) {
      toast.error("No data to export");
      return;
    }

    const headers = Object.keys(data[0]);
    const csvRows = [
      headers.join(","),
      ...data.map((row) => headers.map((field) => `"${row[field]}"`).join(",")),
    ];

    const blob = new Blob([csvRows.join("\n")], { type: "text/csv" });
    const url = window.URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = filename;
    a.click();

    window.URL.revokeObjectURL(url);
  };

  const handleMonthlyReport = async () => {
    try {
      setLoading(true);

      const year = new Date().getFullYear();
      const data = await getMonthlyTickets(year);

      downloadCSV(data, `monthly-tickets-${year}.csv`);
      toast.success("Monthly report downloaded");
    } catch (error) {
      console.error(error);
      toast.error("Failed to download monthly report");
    } finally {
      setLoading(false);
    }
  };

  const handleBranchReport = async () => {
    try {
      setLoading(true);

      const data = await getBranchWiseTickets();

      downloadCSV(data, "branch-wise-tickets.csv");
      toast.success("Branch report downloaded");
    } catch (error) {
      console.error(error);
      toast.error("Failed to download branch report");
    } finally {
      setLoading(false);
    }
  };

  const handlePendingSystemsReport = async () => {
    try {
      setLoading(true);

      const data = await getPendingSystems();

      downloadCSV(data, "pending-systems.csv");
      toast.success("Pending systems report downloaded");
    } catch (error) {
      console.error(error);
      toast.error("Failed to download pending systems report");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Reports</h1>

      <div
        className="max-w-md p-4 rounded-xl space-y-3"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <p className="text-sm mb-2">Download Reports</p>

        <button
          onClick={handleMonthlyReport}
          disabled={loading}
          className="w-full p-2 rounded transition"
          style={{ backgroundColor: colors.primary }}
        >
          {loading ? "Processing..." : "Download Monthly Tickets Report"}
        </button>

        <button
          onClick={handleBranchReport}
          disabled={loading}
          className="w-full p-2 rounded transition"
          style={{ backgroundColor: colors.secondary }}
        >
          {loading ? "Processing..." : "Download Branch-wise Tickets Report"}
        </button>

        <button
          onClick={handlePendingSystemsReport}
          disabled={loading}
          className="w-full p-2 rounded transition"
          style={{ backgroundColor: colors.statusInProgress }}
        >
          {loading ? "Processing..." : "Download System-wise Pending Tickets Report"}
        </button>
      </div>
    </div>
  );
}

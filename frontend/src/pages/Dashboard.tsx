import { useEffect, useState } from "react";
import { colors } from "../theme/colors";
import {
  getAvgResolutionTime,
  getKpi,
  getTicketsByBranch,
  getTicketsByStatus,
  getTicketsBySystem,
  getTicketsOverTime,
  type BranchChartResponse,
  type KpiResponse,
  type StatusChartResponse,
  type SystemChartResponse,
  type TimeSeriesResponse,
} from "../api/dashboard.api";
import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  ResponsiveContainer,
  Legend,
  LineChart,
  Line,
  YAxis,
  XAxis,
  CartesianGrid,
  BarChart,
  Bar,
} from "recharts";

export default function Dashboard() {
  const [kpi, setKpi] = useState<KpiResponse | null>(null);
  const [avgTime, setAvgTime] = useState<number>(0);
  const [loading, setLoading] = useState(true);
  const [statusData, setStatusData] = useState<StatusChartResponse[]>([]);
  const [timeData, setTimeData] = useState<TimeSeriesResponse[]>([]);
  const [systemData, setSystemData] = useState<SystemChartResponse[]>([]);
  const [branchData, setBranchData] = useState<BranchChartResponse[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const kpiData = await getKpi();
        const avgData = await getAvgResolutionTime();
        const status = await getTicketsByStatus();
        const time = await getTicketsOverTime();
        const systems = await getTicketsBySystem();
        const branches = await getTicketsByBranch();
        setBranchData(branches);
        setSystemData(systems);
        setTimeData(time);
        setStatusData(status);
        setKpi(kpiData);
        setAvgTime(avgData.avgResolutionHours);
      } catch (error) {
        console.error("Dashboard error:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return (
      <div
        className="p-6 min-h-screen"
        style={{ backgroundColor: colors.background, color: colors.text }}
      >
        Loading dashboard...
      </div>
    );
  }

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

  const formatDate = (dateStr: string) => {
    const date = new Date(dateStr);
    return `${date.getDate()}/${date.getMonth() + 1}`;
  };

  return (
    <div
      className="p-6 min-h-screen space-y-6"
      style={{ backgroundColor: colors.background, color: colors.text }}
    >
      <h1 className="text-2xl font-bold">Dashboard</h1>

      {/*  KPI CARDS */}
      <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-5 gap-4">
        {/* Total */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{ backgroundColor: colors.card }}
        >
          <p style={{ color: colors.text }}>Total Tickets</p>
          <h2 className="text-xl font-bold" style={{ color: colors.primary }}>
            {kpi?.totalTickets}
          </h2>
        </div>

        {/* Pending */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{ backgroundColor: colors.card }}
        >
          <p style={{ color: colors.text }}>Pending</p>
          <h2
            className="text-xl font-bold"
            style={{ color: colors.statusOpen }}
          >
            {kpi?.pendingTickets}
          </h2>
        </div>

        {/* Ongoing */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{ backgroundColor: colors.card }}
        >
          <p style={{ color: colors.text }}>Ongoing</p>
          <h2
            className="text-xl font-bold"
            style={{ color: colors.statusInProgress }}
          >
            {kpi?.ongoingTickets}
          </h2>
        </div>

        {/* Completed */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{ backgroundColor: colors.card }}
        >
          <p style={{ color: colors.text }}>Completed</p>
          <h2
            className="text-xl font-bold"
            style={{ color: colors.statusResolved }}
          >
            {kpi?.completedTickets}
          </h2>
        </div>

        {/* Avg Time */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{ backgroundColor: colors.card }}
        >
          <p style={{ color: colors.text }}>Avg Resolution (hrs)</p>
          <h2 className="text-xl font-bold" style={{ color: colors.secondary }}>
            {avgTime.toFixed(1)}
          </h2>
        </div>
      </div>
      {/*  STATUS CHART */}
      <div
        className="rounded-2xl p-4 shadow"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <h2 className="text-lg font-semibold mb-4">Tickets by Status</h2>

        <div className="w-full h-80">
          <ResponsiveContainer>
            <PieChart>
              <Pie
                data={statusData}
                dataKey="ticketCount"
                nameKey="status"
                cx="50%"
                cy="50%"
                outerRadius={100}
                label
              >
                {statusData.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={getStatusColor(entry.status)}
                  />
                ))}
              </Pie>

              <Tooltip
                contentStyle={{
                  backgroundColor: colors.surface,
                  border: `1px solid ${colors.border}`,
                  color: colors.text,
                }}
              />

              <Legend />
            </PieChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/* TICKETS OVER TIME */}
      <div
        className="rounded-2xl p-4 shadow"
        style={{
          backgroundColor: colors.card,
          border: `1px solid ${colors.border}`,
        }}
      >
        <h2 className="text-lg font-semibold mb-4">Tickets Over Time</h2>

        <div className="w-full h-80">
          <ResponsiveContainer>
            <LineChart data={timeData}>
              <CartesianGrid stroke={colors.border} strokeDasharray="3 3" />

              <XAxis
                dataKey="ticketDate"
                tickFormatter={formatDate}
                stroke={colors.text}
              />

              <YAxis stroke={colors.text} />

              <Tooltip
                contentStyle={{
                  backgroundColor: colors.surface,
                  border: `1px solid ${colors.border}`,
                  color: colors.text,
                }}
                labelFormatter={(label) => formatDate(label)}
              />

              <Line
                type="monotone"
                dataKey="ticketCount"
                stroke={colors.primary}
                strokeWidth={3}
                dot={{ r: 4 }}
                isAnimationActive={true}
              />
            </LineChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/*  BAR CHARTS ROW */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
        {/*  Tickets by System */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{
            backgroundColor: colors.card,
            border: `1px solid ${colors.border}`,
          }}
        >
          <h2 className="text-lg font-semibold mb-4">Tickets by System</h2>

          <div className="w-full h-80">
            <ResponsiveContainer>
              <BarChart data={systemData}>
                <CartesianGrid stroke={colors.border} strokeDasharray="3 3" />

                <XAxis dataKey="systemName" stroke={colors.text} />
                <YAxis stroke={colors.text} />

                <Tooltip
                  contentStyle={{
                    backgroundColor: colors.surface,
                    border: `1px solid ${colors.border}`,
                    color: colors.text,
                  }}
                />

                <Bar
                  dataKey="ticketCount"
                  fill={colors.primary}
                  radius={[6, 6, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>

        {/*  Tickets by Branch */}
        <div
          className="rounded-2xl p-4 shadow"
          style={{
            backgroundColor: colors.card,
            border: `1px solid ${colors.border}`,
          }}
        >
          <h2 className="text-lg font-semibold mb-4">Tickets by Branch</h2>

          <div className="w-full h-80">
            <ResponsiveContainer>
              <BarChart data={branchData}>
                <CartesianGrid stroke={colors.border} strokeDasharray="3 3" />

                <XAxis dataKey="branchName" stroke={colors.text} />
                <YAxis stroke={colors.text} />

                <Tooltip
                  contentStyle={{
                    backgroundColor: colors.surface,
                    border: `1px solid ${colors.border}`,
                    color: colors.text,
                  }}
                />

                <Bar
                  dataKey="ticketCount"
                  fill={colors.secondary}
                  radius={[6, 6, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>
      </div>
    </div>
  );
}

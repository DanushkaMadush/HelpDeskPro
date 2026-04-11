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
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-3xl font-bold">Dashboard</h1>
          <p style={{ color: colors.text }}>
            Overview of system performance and ticket activity
          </p>
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-5 gap-4">
        {[
          {
            label: "Total Tickets",
            value: kpi?.totalTickets,
            color: colors.primary,
          },
          {
            label: "Pending",
            value: kpi?.pendingTickets,
            color: colors.statusOpen,
          },
          {
            label: "Ongoing",
            value: kpi?.ongoingTickets,
            color: colors.statusInProgress,
          },
          {
            label: "Completed",
            value: kpi?.completedTickets,
            color: colors.statusResolved,
          },
          {
            label: "Avg Resolution (hrs)",
            value: avgTime.toFixed(1),
            color: colors.secondary,
          },
        ].map((card, index) => (
          <div
            key={index}
            className="rounded-2xl p-5 transition-all duration-300"
            style={{
              backgroundColor: colors.card,
              border: `1px solid ${colors.border}`,
            }}
          >
            <p style={{ color: colors.text }} className="text-lg">
              {card.label}
            </p>

            <h2
              className="text-2xl font-bold mt-2"
              style={{ color: card.color }}
            >
              {card.value}
            </h2>
          </div>
        ))}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div
          className="lg:col-span-2 rounded-2xl p-5"
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
                />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </div>

        <div
          className="rounded-2xl p-5"
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
                  outerRadius={100}
                >
                  {statusData.map((entry, index) => (
                    <Cell key={index} fill={getStatusColor(entry.status)} />
                  ))}
                </Pie>

                <Tooltip
                  contentStyle={{
                    backgroundColor: colors.surface,
                    border: `1px solid ${colors.border}`,
                  }}
                />

                <Legend />
              </PieChart>
            </ResponsiveContainer>
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div
          className="rounded-2xl p-5"
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

        <div
          className="rounded-2xl p-5"
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
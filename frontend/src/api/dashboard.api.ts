import { apiClient } from "./axios";

export type KpiResponse = {
  totalTickets: number;
  pendingTickets: number;
  ongoingTickets: number;
  completedTickets: number;
};

export const getKpi = async (): Promise<KpiResponse> => {
  const response = await apiClient.get<KpiResponse>("/v1/dashboard/kpi");
  return response.data;
};

export type AvgResolutionResponse = {
  avgResolutionHours: number;
};

export const getAvgResolutionTime =
  async (): Promise<AvgResolutionResponse> => {
    const response = await apiClient.get<AvgResolutionResponse>(
      "/v1/dashboard/avg-resolution-time",
    );
    return response.data;
  };

export type StatusChartResponse = {
  status: string;
  ticketCount: number;
};

export const getTicketsByStatus = async (): Promise<StatusChartResponse[]> => {
  const response = await apiClient.get<StatusChartResponse[]>(
    "/v1/dashboard/tickets-by-status",
  );
  return response.data;
};

export type TimeSeriesResponse = {
  ticketDate: string;
  ticketCount: number;
};

export const getTicketsOverTime = async (): Promise<TimeSeriesResponse[]> => {
  const response = await apiClient.get<TimeSeriesResponse[]>(
    "/v1/dashboard/tickets-over-time",
  );
  return response.data;
};

export type SystemChartResponse = {
  systemName: string;
  ticketCount: number;
};

export const getTicketsBySystem = async (): Promise<SystemChartResponse[]> => {
  const response = await apiClient.get<SystemChartResponse[]>(
    "/v1/dashboard/tickets-by-system",
  );
  return response.data;
};

export type BranchChartResponse = {
  branchName: string;
  ticketCount: number;
};

export const getTicketsByBranch = async (): Promise<BranchChartResponse[]> => {
  const response = await apiClient.get<BranchChartResponse[]>(
    "/v1/dashboard/tickets-by-branch",
  );
  return response.data;
};

export type PriorityChartResponse = {
  priority: string;
  ticketCount: number;
};

export const getTicketsByPriority = async (): Promise<
  PriorityChartResponse[]
> => {
  const response = await apiClient.get<PriorityChartResponse[]>(
    "/v1/dashboard/tickets-by-priority",
  );
  return response.data;
};
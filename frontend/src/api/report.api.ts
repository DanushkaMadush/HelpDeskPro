import { apiClient } from "./axios";

export interface MonthlyTicket {
  monthNumber: number;
  monthName: string;
  totalTickets: number;
}

export interface BranchTicket {
  branchId: number;
  branchName: string;
  totalTickets: number;
}

export interface PendingSystem {
  systemId: number;
  systemName: string;
  pendingTickets: number;
}

export const getMonthlyTickets = async (
  year: number,
): Promise<MonthlyTicket[]> => {
  try {
    const res = await apiClient.get<MonthlyTicket[]>(
      `/reports/monthly-tickets?year=${year}`,
    );
    return res.data;
  } catch (error: any) {
    console.error("Get monthly tickets error:", error);
    throw error;
  }
};

export const getBranchWiseTickets = async (): Promise<BranchTicket[]> => {
  try {
    const res = await apiClient.get<BranchTicket[]>(
      "/reports/branch-wise-tickets",
    );
    return res.data;
  } catch (error: any) {
    console.error("Get branch-wise tickets error:", error);
    throw error;
  }
};

export const getPendingSystems = async (): Promise<PendingSystem[]> => {
  try {
    const res = await apiClient.get<PendingSystem[]>(
      "/reports/pending-systems",
    );
    return res.data;
  } catch (error: any) {
    console.error("Get pending systems error:", error);
    throw error;
  }
};

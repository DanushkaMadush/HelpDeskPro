import { apiClient } from './axios';

export interface Branch {
  branchId: number;
  branchName: string;
  address: string;
}

export interface BranchCreateRequest {
  branchName: string;
  address: string;
  createdBy: string;
}

export interface BranchCreateResponse {
  successMessage?: string;
  errorMessage?: string;
}

export const getBranches = async (): Promise<Branch[]> => {
  try {
    const res = await apiClient.get<Branch[]>('/branches');
    return res.data;
  } catch (error: any) {
    console.error('Get branches error:', error);
    throw error;
  }
};

export const createBranch = async (data: BranchCreateRequest): Promise<BranchCreateResponse> => {
  try {
    const res = await apiClient.post<BranchCreateResponse>('/branches', data);
    return res.data;
  } catch (error: any) {
    console.error('Brnach create error:' , error);
    throw error;
  }
}
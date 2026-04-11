import { apiClient } from './axios';

export interface Department {
  departmentId: number;
  departmentName: string;
}

export interface DepartmentCreateRequest {

}

export interface DepartmentCreateResponse {

}

export const getDepartments = async (): Promise<Department[]> => {
  try {
    const res = await apiClient.get<Department[]>('/departments');
    return res.data;
  } catch (error: any) {
    console.error('Get departments error:', error);
    throw error;
  }
};

export const createDepartment = async (): Promise<DepartmentCreateResponse> => {
  try {
    const res = await apiClient.post<DepartmentCreateResponse>('/departments');
    return res.data;
  } catch (error: any) {
    console.error('Department create error:', error);
    throw error;
  }
}
import { apiClient } from "./axios";

export interface CreateRoleRequest {
    roleName: string;
}

export interface RoleAssignRequest {
    email: string;
    roleName: string;
}

export const createRole = async (data: CreateRoleRequest):Promise<any> => {
    try {
        const res = await apiClient.post<any>('/users/create-role', data);
        return res.data;
    } catch (error: any) {
        console.error('Role create error:', error);
        throw error;
    }
}

export const assignRole = async (data: RoleAssignRequest): Promise<any> => {
    try {
        const res = await apiClient.post<any>('/users/assign-role', data);
        return res;
    } catch (error: any) {
        console.error('Role assigning error');
        throw error;
    }
}
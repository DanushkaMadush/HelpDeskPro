import { apiClient } from "./axios";

export interface PermissionCreateRequest {
    name: string;
    description?: string;
}

export interface PermissionRoleAssignRequest {
    roleName: string;
    permissionName: string;
}

export interface PermissionUserAssignRequest {
    email: string;
    permissionName: string;
}

export const createPermission = async (data: PermissionCreateRequest): Promise<any> => {
    try {
        const res = await apiClient.post<any>('/users/create-permission', data);
        return res.data;
    } catch (error: any) {
        console.error('Permission create error:', error);
        throw error;
    }
}

export const assignPermissionToRole = async (data: PermissionUserAssignRequest): Promise<any> => {
    try {
        const res = await apiClient.post<any>('/users/assign-permission', data);
        return res.data;
    } catch (error: any) {
        console.error('Permission assign error:', error);
        throw error;
    }
}

export const assignPermissionToUser = async (data: PermissionUserAssignRequest): Promise<any> => {
    try {
        const res = await apiClient.post<any>('/users/assign-permission-to-user', data);
        return res.data;
    } catch (error: any) {
        console.error('Permission assign error:', error);
        throw error;
    }
}
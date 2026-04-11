import { apiClient } from "./axios";

export interface GetAllUsersResponse {
    id: string;
    email: string;
    firstName: string;
    lastName?: string;
    roles?: string[];
}

export const getAllUsers = async (role: string | null): Promise<GetAllUsersResponse> => {
    try {
        const res = await apiClient.get<GetAllUsersResponse>(role ? `/users?role=${role}` : '/users');
        return res.data;
    } catch (error: any) {
        console.error('User retrieve error', error);
        throw error;
    }
}
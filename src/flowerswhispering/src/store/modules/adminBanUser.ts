import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface User {
  userId: number;
  userName: string;
  userstatus: string;
  uemail: string;
  userrole: string;
  replyContent: string;
}

interface BanUserState {
  users: User[];
}

// 用户封禁功能板块
const adminBanUserModule: Module<BanUserState, any> = {
  state: {
    users: [],
  },
  mutations: {
    SET_USERS(state, users: User[]) {
      state.users = users;
    }
  },
  actions: {
    // 封禁用户
    async banUser(_, userId: number) {
      try {
        const response = await apiClient.put(`/api/AdminUser/ban?id=${userId}`);
        return !!response;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '封禁用户失败');
      }
    },
    // 解封用户
    async unblockUser(_, userId: number) {
      try {
        const response = await apiClient.put(`/api/AdminUser/unblock?id=${userId}`);
        return !!response;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '解封用户失败');
      }
    },
    // 获取所有用户
    async fetchAllUsers({ commit }) {
      try {
        const response = await apiClient.get('/api/AdminUser/all/user');
        if (response) {
          commit('SET_USERS', response.data);
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取用户列表失败');
      }
    }
  },
  getters: {
    getAllUsers(state): User[] {
      return state.users;
    }
  }
};

export default adminBanUserModule;

import { Module } from 'vuex';
import apiClient from '@/axios/apiClient'; // 确保你有一个 apiClient 文件来处理 Axios 请求

interface UserState {
  userInfo: Record<string, any> | null;
  isAuthenticated: boolean;
}

const userModule: Module<UserState, any> = {
  state: {
    userInfo: null,
    isAuthenticated: false,
  },
  mutations: {
    SET_USER_INFO(state, payload) {
      state.userInfo = payload;
      state.isAuthenticated = true;
    },
    CLEAR_USER_INFO(state) {
      state.userInfo = null;
      state.isAuthenticated = false;
    },
    UPDATE_USER_INFO(state, updatedInfo) {
      if (state.userInfo) {
        state.userInfo = { ...state.userInfo, ...updatedInfo };
      }
    },
  },
  actions: {
    async login({ commit }, { usernameOrEmail, password }) {
      try {
        const response = await apiClient.post('/api/UserAccount/login', { username: usernameOrEmail, password });
        if (response.status === 200) {
          commit('SET_USER_INFO', response.data);
          return true;
        }
        return false;
      } catch (error: any) {
        console.error('登陆失败:', error.response?.data || error.message);
        throw new Error(error.response?.data || '登录失败');
      }
    },
    async register({ commit }, { username, email, password, avatar }) {
      const languagePreference = "zh-CN";
      const bio = "这家伙很懒，什么都没留下"
      const gender = "不愿透露";
      try {
        const response = await apiClient.post('/api/UserAccount/register', {
          username, email, password, languagePreference, bio, avatar, gender
        });
        if (response.status === 200) { // HTTP状态码200表示成功
          return true;
        } else {
          console.error('注册失败:', response.data);
          return false;
        }
      } catch (error: any) {
        console.error('注册失败:', error.response?.data || error.message);
        throw new Error(error.response?.data || '注册失败');
      }
    },
    async guestLogin({ commit }) {
      // 创建默认的游客用户信息
      const guestUserInfo = {
        userId: '-1',
        username: '游客',
        email: 'guest@example.com',
        languagePreference: 'zh-CN',
        userState: 'guest', // 状态为游客
        userRole: 'guest',  // 角色为游客
        avatar: require('@/userprofile/images/user-avatar.jpg'),
      };
      commit('SET_USER_INFO', guestUserInfo);
      return true;
    },
    logout({ commit }) {
      commit('CLEAR_USER_INFO');
    },
    async updateUserInfo({ commit, state }, { username, email, password, languagePreference, bio, avatar, gender }) {
      if (!state.userInfo) {
        throw new Error('用户未登录');
      }
      try {
        const response = await apiClient.put('/api/UserAccount/edit/user', { username, password, email, languagePreference, bio, avatar, gender, userId: state.userInfo.userId });
        if (response.status === 200) {
          commit('UPDATE_USER_INFO', { username, email, password, languagePreference, bio, avatar, gender });
          return true;
        } else {
          console.error('用户信息更新失败:', response.data);
          return false;
        }
      } catch (error: any) {
        console.error('用户信息更新失败:', error.response?.data || error.message);
        throw new Error(error.response?.data || '用户信息更新失败');
      }
    },
  },
  getters: {
    getUserInfo(state) {
      return state.userInfo;
    },
    isAuthenticated(state) {
      return state.isAuthenticated;
    },
    isAdmin(state) {
      return state.userInfo?.userRole === 'admin';
    },
    isGuest(state) {
      return state.userInfo?.userState === 'guest';
    }
  },
};

export default userModule;

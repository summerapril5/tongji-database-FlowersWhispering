import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Feedback {
  feedbackId: number;
  userId: number;
  feedbackContent: string;
  submittedDate: string;
}

interface FeedbackState {
  feedbacks: Feedback[];

}
//管理用户反馈板块
const adminFeedbackModule: Module<FeedbackState, any> = {
  state: {
    feedbacks: [],
  },
  mutations: {
    SET_FEEDBACKS(state, feedbacks: Feedback[]) {
      state.feedbacks = feedbacks;
    },

  },
  actions: {
    // 获取所有反馈
    async fetchAllFeedbacks({ commit }) {
      try {
        const response = await apiClient.get('/api/AdminUser/all/feedback');
        if (response) {
          commit('SET_FEEDBACKS', response.data);
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取用户反馈列表失败');
      }
    }

  },
  getters: {
    getAllFeedbacks(state) {
      return state.feedbacks;
    }

  }
};

export default adminFeedbackModule;

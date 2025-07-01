import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Review {
    reviewId: string;              // 更改为 string 类型
    modifiedContent: string;
    submittedDate: string;         
    submitterId: number;
    plantId: number | null;
    reviewStatus: string;
    reviewDate: string | null;     // 审核时间可能为 null
  }
  

interface AdminReviewState {
  reviews: Review[];
}
//审核功能模块
const adminReviewModule: Module<AdminReviewState, any> = {
  state: {
    reviews: [],
  },
  mutations: {
    SET_REVIEWS(state, reviews: Review[]) {
      state.reviews = reviews;
    },
  },
  actions: {
    async fetchAllReviews({ commit }) {
      try {
        const response = await apiClient.get('/api/AdminReview/all');
        if (response) {
          commit('SET_REVIEWS', response.data);
          return true;
        }
        return false;
      } catch (error:any) {
        throw new Error(error.response?.data.message || '查询信息失败');
      }
    },
    async approveReview({ dispatch }, reviewId: number) {
      try {
        const response = await apiClient.put(`/api/AdminReview/pass?reviewId=${reviewId}`);
        if (response) {
          // 重新获取所有审核信息
          await dispatch('fetchAllReviews');
          return true;
        }
        return false;
      } catch (error:any) {
        throw new Error(error.response?.data.message || '操作失败');
      }
    },
    async rejectReview({ dispatch }, reviewId: number) {
      try {
        const response = await apiClient.put(`/api/AdminReview/pass/no?reviewId=${reviewId}`);
        if (response.status === 200) {
          // 重新获取所有审核信息
          dispatch('fetchAllReviews');
          return true;
        }
        return false;
      } catch (error:any) {
        throw new Error(error.response?.data.message || '操作失败');
      }
    },
  },
  getters: {
    getReviews(state): Review[] {
      return state.reviews;
    },
  },
};

export default adminReviewModule;


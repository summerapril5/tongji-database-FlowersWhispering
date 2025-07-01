import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Score {
  userId: number;
  totalScore: number;
  username: string;
}

interface ScoreState {
  scoreList: Score[];
  userScore: number;
}

const scoreModule: Module<ScoreState, any> = {
  state: {
    scoreList: [],    // 所有用户的分数列表
    userScore: 0,     // 当前用户的分数
  },
  mutations: {
    SET_SCORE_LIST(state, scores: Score[]) {
      state.scoreList = scores;
    },
    SET_USER_SCORE(state, score: number) {
      state.userScore = score;
    }
  },
  actions: {
    // 获取所有用户的分数排名
    async fetchScoreRank({ commit }) {
      try {
        const response = await apiClient.get('/api/CommuniScore/score/rank');
        if (response && response.data) {
          commit('SET_SCORE_LIST', response.data);
          return response.data;
        }
      } catch (error: any) {
        console.error('获取分数排名失败:', error);
        throw new Error(error.response?.data.message || '获取分数排名失败');
      }
    },

    // 获取当前用户的分数
    async fetchUserScore({ commit, rootGetters }) {
      try {
        const userId = rootGetters.getUserInfo.userId; // 从 Vuex 获取当前用户的ID
        const response = await apiClient.get(`/api/CommuniScore/score/user`, { params: { userId } });
        if (response && response.data) {
          commit('SET_USER_SCORE', response.data.totalScore);
          return response.data.totalScore;
        }
      } catch (error: any) {
        console.error('获取用户分数失败:', error);
        throw new Error(error.response?.data.message || '获取用户分数失败');
      }
    }
  },
  getters: {
    allScores(state): Score[] {
      return state.scoreList;
    },
    currentUserScore(state): number {
      return state.userScore;
    }
  }
};

export default scoreModule;

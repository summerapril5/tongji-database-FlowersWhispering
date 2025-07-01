import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Favorite {
  favorId: number;
  userId: number;
  articleId: number;
  title: string;
  content: string;
  publisherId: number;
  publishedDate: string;
}

interface FavoriteState {
  favorites: Favorite[];
}

const favoriteModule: Module<FavoriteState, any> = {
  state: {
    favorites: [],
  },
  mutations: {
    SET_FAVORITES(state, favorites: Favorite[]) {
      state.favorites = favorites;
    },
    ADD_FAVORITE(state, favorite: Favorite) {
      state.favorites.push(favorite);
    },
    DELETE_FAVORITE(state, favorId: number) {
      state.favorites = state.favorites.filter(favorite => favorite.favorId !== favorId);
    }
  },
  actions: {
    // 添加收藏
    async addFavorite({ commit, rootState }, articleId: number) {
      try {
        const userId = rootState.currentUser.userId; // 假设你在根状态中存储了当前用户信息
        const response = await apiClient.post('/api/CommuniFavor/favor/add', { userId, articleId });
        if (response) {
          commit('ADD_FAVORITE', response.data);
          return true;
        }
        return false;
      } catch (error: any) {
        console.error('添加收藏失败:', error);
        throw new Error(error.response?.data.message || '添加收藏失败');
      }
    },

    // 删除收藏
    async deleteFavorite({ commit }, favorId: number) {
      try {
        const response = await apiClient.delete(`/api/CommuniFavor/favor/delete?favorId=${favorId}`);
        if (response.status === 200) {
          commit('DELETE_FAVORITE', favorId);
          return true;
        }
        return false;
      } catch (error: any) {
        console.error('删除收藏失败:', error);
        throw new Error(error.response?.data.message || '删除收藏失败');
      }
    },

    // 获取用户收藏列表
    async fetchFavoritesByUser({ commit }, userId: number) {
      try {
        const response = await apiClient.get(`/api/CommuniFavor/favor/get?userId=${userId}`);
        console.log('API:', response); // 调试信息
        if (response) {
          commit('SET_FAVORITES', response.data);
          return response.data; // 返回收藏列表
        }
      } catch (error: any) {
        console.error('获取收藏列表失败:', error);
        throw new Error(error.response?.data.message || '获取收藏列表失败');
      }
    }
  },
  getters: {
    allFavorites(state): Favorite[] {
      return state.favorites;
    },
    favoriteByArticle: (state) => (articleId: number) => {
      return state.favorites.find(favorite => favorite.articleId === articleId);
    }
  }
};

export default favoriteModule;

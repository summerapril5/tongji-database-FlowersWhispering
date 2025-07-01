import { Module, ActionContext } from 'vuex';
import apiClient from '@/axios/apiClient';

// 定义状态类型
interface CatalogState {
  plants: any[]; // 植物信息列表
  favoritePlants: any[]; // 用户收藏的植物列表
  plantSearchResult: any; // 植物搜索结果
  latestPlants: any[]; // 最新植物列表
  loading: boolean; // 是否正在加载
  error: string | null; // 错误信息
}

// 初始化状态
const state: CatalogState = {
  plants: [],
  favoritePlants: [],
  latestPlants: [],
  plantSearchResult: null,
  loading: false,
  error: null,
};

// 定义 getters
const getters = {
  getPlants: (state: CatalogState) => state.plants,
  getFavoritePlants: (state: CatalogState) => state.favoritePlants,
  getPlantSearchResult: (state: CatalogState) => state.plantSearchResult,
  getLatestPlants: (state: CatalogState) => state.latestPlants,
  isLoading: (state: CatalogState) => state.loading,
  getError: (state: CatalogState) => state.error,
};

// 定义 mutations
const mutations = {
  SET_LATEST_PLANTS(state: CatalogState, plants: any[]) {
    state.latestPlants = plants;
  },
  SET_PLANTS(state: CatalogState, plants: any[]) {
    state.plants = plants;
  },
  SET_FAVORITE_PLANTS(state: CatalogState, plants: any[]) {
    state.favoritePlants = plants;
  },
  ADD_TO_FAVORITES(state: CatalogState, plant: any) {
    state.favoritePlants.push(plant);
  },
  REMOVE_FROM_FAVORITES(state: CatalogState, plantId: string) {
    state.favoritePlants = state.favoritePlants.filter((p) => p.id !== plantId);
  },
  SET_PLANT_SEARCH_RESULT(state: CatalogState, result: any) {
    state.plantSearchResult = result;
  },
  SET_LOADING(state: CatalogState, loading: boolean) {
    state.loading = loading;
  },
  SET_ERROR(state: CatalogState, error: string | null) {
    state.error = error;
  },
};

// 定义 actions
const actions = {
  // 根据植物名搜索植物
  async searchPlantsByName({ commit }: ActionContext<CatalogState, any>, plantName: string) {
    commit('SET_LOADING', true);
    try {
      const response = await apiClient.get(`/api/PlantFins/findInfo?pnantName=${plantName}`);
      commit('SET_SEARCH_RESULT', response.data);
      commit('SET_LOADING', false);
    } catch (error) {
      commit('SET_ERROR', '搜索植物失败');
      commit('SET_LOADING', false);
    }
  },

  // 获取用户收藏的植物列表
  async fetchFavoritePlantsByUserId({ commit }: ActionContext<CatalogState, any>, userId: string) {
    commit('SET_LOADING', true);
    try {
      const response = await apiClient.get(`/api/PlantFavor/findFavors/${userId}`); // 获取用户收藏的植物
      commit('SET_FAVORITE_PLANTS', response.data);
      commit('SET_LOADING', false);
    } catch (error) {
      commit('SET_ERROR', '获取用户收藏失败');
      commit('SET_LOADING', false);
    }
  },

  // 收藏植物
  async addToFavorites({ commit }: ActionContext<CatalogState, any>, { userId, plantId }: { userId: string, plantId: string }) {
    commit('SET_LOADING', true);
    try {
      const response = await apiClient.post(`/api/PlantFavor/addFavor`, { userId,plantId}); // 添加植物到收藏
      commit('ADD_TO_FAVORITES', response.data); // 更新收藏列表
      commit('SET_LOADING', false);
    } catch (error) {
      commit('SET_ERROR', '收藏植物失败');
      commit('SET_LOADING', false);
    }
  },

  // 取消收藏植物
  async removeFromFavorites({ commit }: ActionContext<CatalogState, any>, { userId, plantId }: { userId: string, plantId: string }) {
    commit('SET_LOADING', true);
    try {
      await apiClient.delete(`/api/PlantFavor/deleteFavor?userId=${userId}&plantId=${plantId}`); // 从收藏中移除植物
      commit('REMOVE_FROM_FAVORITES', plantId); // 更新收藏列表
      commit('SET_LOADING', false);
    } catch (error) {
      commit('SET_ERROR', '取消收藏植物失败');
      commit('SET_LOADING', false);
    }
  },

  async fetchLatestPlants({ commit }: ActionContext<CatalogState, any>) {
    commit('SET_LOADING', true);
    try {
      const response = await apiClient.get(`/api/PlantLatest/findLatest`); // 假设这是获取最新植物的接口
      commit('SET_LATEST_PLANTS', response.data);
      commit('SET_LOADING', false);
    } catch (error) {
      commit('SET_ERROR', '获取最新植物失败');
      commit('SET_LOADING', false);
    }
  },
};

// 导出 catalog 模块，作为 Vuex 的模块使用
const catalogModule: Module<CatalogState, any> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};

export default catalogModule;

import { Module } from 'vuex';
import apiClient from '@/axios/apiClient'; 

interface Post {
  articleId: number;
  title: string;
  content: string;
  publisherId: number;
  publishedDate: string;
  username: string;
}

interface PostState {
  posts: Post[];
}

const postModule: Module<PostState, any> = {
  state: {
    posts: [],
  },
  mutations: {
    SET_POSTS(state, posts: Post[]) {
      state.posts = posts;
    },
    ADD_POST(state, post: Post) {
      state.posts.push(post);
    },
    DELETE_POST(state, postId: number) {
      state.posts = state.posts.filter(post => post.articleId !== postId);
    }
  },
  actions: {
    // 获取所有帖子
    async fetchAllPosts({ commit }) {
      try {
        const response = await apiClient.get('/api/CommuniPostsandComments/post/all');
        if (response) {
          commit('SET_POSTS', response.data);
          return response.data;
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取帖子列表失败');
      }
    },
    // 获取某个用户的所有帖子
    async fetchUserPosts({ commit }, userId: number) {
      try {
        const response = await apiClient.get(`/api/CommuniPostsandComments/post/user/all?userId=${userId}`);
        if (response) {
          commit('SET_POSTS', response.data);
          return response.data; // 返回数据以便外部调用时使用
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取用户帖子失败');
      }
    },
    // 发布帖子
    async addPost({ commit, dispatch, rootGetters }, { title, content }) {
      try {
        const publisherId = rootGetters.getUserInfo.userId;
        const response = await apiClient.post('/api/CommuniPostsandComments/post/add', { title, content, publisherId });
        if (response) {
          commit('ADD_POST', response.data);
          await dispatch('fetchAllPosts');
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '添加帖子失败');
      }
    },
    // 删除帖子
    async deletePost({ commit }, postId: number) {
      try {
        const response = await apiClient.delete(`/api/CommuniPostsandComments/post/delete?postId=${postId}`);
        if (response) {
          commit('DELETE_POST', postId);
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '删除帖子失败');
      }
    },
    // 更新帖子
    async updatePost({ commit,dispatch }, { articleId, title, content, publishedDate, publisherId }) {
      try {
        const response = await apiClient.put('/api/CommuniPostsandComments/post/edit', {
          articleId,
          title,
          content,
          publisherId,
          publishedDate
        });
        if (response) {
          await dispatch('fetchAllPosts');
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '更新帖子失败');
      }
    },
    // 搜索帖子
    async searchPosts({ commit }, searchText: string) {
      try {
        const response = await apiClient.get(`/api/CommuniPostsandComments/post/all/search?text=${searchText}`);
        if (response) {
          commit('SET_POSTS', response.data);
          return response.data;
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '搜索帖子失败');
      }
    }
  },
  getters: {
    getPosts(state) {
      console.log('从 state 中获取帖子:', state.posts); // 调试信息
      return state.posts;
    }
  }
};

export default postModule;

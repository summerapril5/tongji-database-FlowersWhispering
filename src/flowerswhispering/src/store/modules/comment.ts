import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Comment {
  commentId: number;
  userId: number;
  articleId: number;
  content: string;
  createdDate: string;
  username: string;
  articleTitle: string;
}

interface CommentState {
  comments: Comment[];
}

const commentModule: Module<CommentState, any> = {
  state: {
    comments: [],
  },
  mutations: {
    SET_COMMENTS(state, comments: Comment[]) {
      state.comments = comments;
    },
    ADD_COMMENT(state, comment: Comment) {
      state.comments.push(comment);
    },
    DELETE_COMMENT(state, commentId: number) {
      state.comments = state.comments.filter(comment => comment.commentId !== commentId);
    }
  },
  actions: {
    // 添加评论
    async addComment({ commit,rootState,rootGetters }, { articleId, content }) {
      try {
        const userId = rootGetters.getUserInfo.userId;
        const response = await apiClient.post('/api/CommuniPostsandComments/comment/add', { userId, articleId, content });
        if (response) {
          commit('ADD_COMMENT', response.data);
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '添加评论失败');
      }
    },

    // 删除评论
    async deleteComment({ commit }, commentId: number) {
      try {
        const response = await apiClient.delete(`/api/CommuniPostsandComments/comment/delete?commentId=${commentId}`);
        if (response) {
          commit('DELETE_COMMENT', commentId);
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '删除评论失败');
      }
    },

    // 获取某个帖子下的所有评论
    async fetchCommentsByPost({ commit }, postId: number) {
      try {
        const response = await apiClient.get(`/api/CommuniPostsandComments/comment/all?postId=${postId}`);
        if (response) {
          commit('SET_COMMENTS', response.data);
          return response.data;
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取评论列表失败');
      }
    },

    // 获取某个用户的所有评论
    async fetchCommentsByUser({ commit }, userId: number) {
      try {
        const response = await apiClient.get(`/api/CommuniPostsandComments/comment/user/all?userId=${userId}`);
        if (response) {
          commit('SET_COMMENTS', response.data);
          return response.data;
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取用户评论失败');
      }
    }
  },
  getters: {
    allComments(state): Comment[] {
      return state.comments;
    },
    commentsByArticle: (state) => (articleId: number) => {
      return state.comments.filter(comment => comment.articleId === articleId);
    },
    commentsByUser: (state) => (userId: number) => {
      return state.comments.filter(comment => comment.userId === userId);
    }
  }
};

export default commentModule;

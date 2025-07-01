import { Module } from 'vuex';
import apiClient from '@/axios/apiClient'; 
interface Announcement {
  announcementId: number;
  title: string;
  content: string;
  publisherId: number;
  publishedDate: string;
}

interface AnnouncementState {
  announcements: Announcement[];
}
//管理公告板块
const adminAnnoModule: Module<AnnouncementState, any> = {
  state: {
    announcements: [],
  },
  mutations: {
    SET_ANNOUNCEMENTS(state, announcements: Announcement[]) {
      state.announcements = announcements;
    },
    ADD_ANNOUNCEMENT(state, announcement: Announcement) {
      state.announcements.push(announcement);
    },
    DELETE_ANNOUNCEMENT(state, announcementId: number) {
      state.announcements = state.announcements.filter(a => a.announcementId !== announcementId);
    }
  },
  actions: {
    // 获取所有公告
    async fetchAnnouncements({ commit}) {
      try {
        const response = await apiClient.get('/api/AdminAnno/announcement/all');
        if (response) {
          commit('SET_ANNOUNCEMENTS', response.data);
          //console.log('后端返回的公告数据:', response.data); // 调试信息
        }
      } catch (error: any) {
        throw new Error(error.response?.data.message || '获取公告列表失败');
      }
    },
    // 发布公告
    async publishAnnouncement({ commit,dispatch,rootState,rootGetters}, { title, content}) {
      try {
        const userInfo = rootGetters.getUserInfo;
        //console.log('用户信息:', userInfo); // 调试信息，确认 userInfo 是否为空
        const publisherId = userInfo.userId;
        //console.log('发布公告参数:', {title, content, publisherId}); // 添加调试信息
        const response = await apiClient.post('/api/AdminAnno/announcement/publish', { title, content, publisherId });
        if (response) {
          commit('ADD_ANNOUNCEMENT', response.data);
          await dispatch('fetchAnnouncements');
          return true;
        }
        return false;
      } catch (error: any) {
        //console.error('发布公告失败:', error.response?.data || error); // 输出更多的错误信息
        throw new Error(error.response?.data.message || '发布公告失败');
      }
    },
    // 删除公告
    async deleteAnnouncement({ commit }, announcementId: number) {
      try {
        const response = await apiClient.delete(`/api/AdminAnno/announcement/delete?announcementId=${announcementId}`);
        if (response) {
          commit('DELETE_ANNOUNCEMENT', announcementId);
          return true;
        }
        return false;
      } catch (error: any) {
        throw new Error(error.response?.data.message || '删除公告失败');
      }
    }
  },
  getters: {
    getAnnouncements(state): Announcement[] {
      return state.announcements;
    }
  }
};

export default adminAnnoModule;

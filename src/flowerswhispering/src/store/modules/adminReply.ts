import { Module } from 'vuex';
import apiClient from '@/axios/apiClient';

interface Reply {
    id: number;
    userId: number;
    content: string;
    replyDate: string;
}

interface AdminReplyState {
    replies: Reply[];
}

// 管理员回复功能板块
const adminReplyModule: Module<AdminReplyState, any> = {
    state: {
        replies: [],
    },
    mutations: {
        SET_REPLIES(state, replies: Reply[]) {
            state.replies = replies;
        }
    },
    actions: {
        // 获取所有回复
        async fetchAllReplies({ commit }) {
            try {
                const response = await apiClient.get('/api/AdminReply/all/reply');
                if (response) {
                    commit('SET_REPLIES', response.data);
                }
            } catch (error: any) {
                throw new Error(error.response?.data.message || '获取回复列表失败');
            }
        },
        // 传参数进行管理员回复
        async replyToUser({ commit }, { userId, content }) {
            try {
                const response = await apiClient.post('/api/AdminUser/reply', { userId, content });
                if (response.status === 200) {
                    return true;
                }
                return false;
            } catch (error: any) {
                console.error('回复失败:', error.response?.data || error.message);
                throw new Error(error.response?.data || '回复失败');
            }
        }

    },
    getters: {
        getAllReplies(state): Reply[] {
            return state.replies;
        }
    }
};

export default adminReplyModule;
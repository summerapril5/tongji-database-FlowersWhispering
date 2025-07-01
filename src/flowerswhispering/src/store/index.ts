import { createStore } from 'vuex';
import userModule from './modules/user';
import adminReviewModule from './modules/adminReview'; // 引入管理员审核模块
import createPersistedState from 'vuex-persistedstate';
//import adminPlantModule from './modules/adminPlant';
import adminAnnoModule from './modules/adminAnno';
import adminBanUserModule from './modules/adminBanUser';
import adminFeedbackModule from './modules/adminFeedback';
import postModule from './modules/post';
import commentModule from './modules/comment';
import favoriteModule from './modules/favorite';
import scoreModule from './modules/score';
import adminReplyModule from './modules/adminReply';
export default createStore({
  modules: {
    user: userModule,
    adminReview: adminReviewModule, // 注册管理员审核模块
   // adminPlant: adminPlantModule,
    adminAnno: adminAnnoModule,
    adminBanUser: adminBanUserModule,
    adminFeedback: adminFeedbackModule,
    post: postModule,
    comment:commentModule,
    favorite:favoriteModule,
    score:scoreModule,
    adminReply:adminReplyModule,
  },
  plugins: [createPersistedState()],
});

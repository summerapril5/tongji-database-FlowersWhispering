import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import store from '@/store';
import Home from '../home/Home.vue';
import Login from '../login/Login.vue';
import AdminPanel from '../adminpanel/AdminPanel.vue';
import Catalog from '../catalog/Catalog.vue';
import Search from '../catalog/Search.vue';
import Information from '../catalog/information.vue';
import FindError from '../catalog/FindError.vue';
import Recognition from '../catalog/Recognition.vue';
import Community from '../community/Community.vue';
import Profile from '../userprofile/UserProfile.vue';
import PostDetail from '../community/PostDetail.vue';
import Mycomment from '../community/Mycomment.vue';
import Myfavourite from '../community/Myfavourite.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/login'  // 重定向到登录页面
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/catalog',
    name: 'Catalog',
    component: Catalog,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/search',
    name: 'Search',
    component: Search,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/information',
    name: 'Information',
    component: Information,
    meta: { requiresAuth: true } , // 需要验证
    props: route => ({ outputName: route.query.outputName }) //以传输植物名字到 outputName
  },
  {
    path: '/FindError',
    name: 'FindError',
    component: FindError,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/recognition',
    name: 'Recognition',
    component: Recognition,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/community',
    name: 'Community',
    component: Community,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/userprofile',
    name: 'UserProfile',
    component: Profile,
    meta: { requiresAuth: true } // 需要验证
  },
  {
    path: '/postDetail',
    name: 'PostDetail',
    component: PostDetail,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/mycomment',
    name: 'Mycomment',
    component: Mycomment,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/myfavourite',
    name: 'Myfavourite',
    component: Myfavourite,
    meta: { requiresAuth: true }  // 需要验证
  },
  {
    path: '/home',
    name: 'Home',
    component: Home,
    meta: { requiresAuth: true }  // 需要验证
  },
  
  {
    path: '/adminpanel',
    name: 'AdminPanel',
    component: AdminPanel,
    meta: { requiresAuth: true, requiresAdmin: true }  // 需要管理员权限
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    // 检查用户是否已认证
    if (!store.getters.isAuthenticated) {
      alert('请先登录!');
      next({ name: 'Login' });
    } else if (to.matched.some(record => record.meta.requiresAdmin)) {
      // 检查用户是否是管理员
      if (!store.getters.isAdmin) {
        alert('您无权访问该页面。');
        next({ name: 'Home' }); // 重定向到主页
      } else {
        next();
      }
    } else {
      next();
    }
  } else {
    next(); // 确保总是调用 next() 方法
  }
});

export default router;

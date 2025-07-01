<template>
  <header class="header">
    <div class="logo">Flowers Whispering</div>
    <div class="nav-user-container">
      <nav class="nav-links">
        <nav v-if="currentUser && currentUser.userRole === 'admin'">
          <button @click="goToAdminPanel" class="nav-button">管理</button>
        </nav>
        <button @click="goHome" class="nav-button">首页</button>
        <button @click="goToCommunity" class="nav-button">社区</button>
        <button @click="goToCatalog" class="nav-button">图鉴</button>
      </nav>

      <div class="user-info">
        <div class="user-avatar-wrapper">
          <img :src="currentUser?.avatar || 'default-avatar.png'" alt="User Avatar" @click="handleUserAvatarClick">
          <div class="user-info-list" v-if="currentUser">
            <div v-if="currentUser.userRole !== 'guest'">
              <p>用户名: {{ currentUser.username }}</p>
              <p>邮箱: {{ currentUser.email }}</p>
              <p>角色: {{ currentUser.userRole === 'admin' ? '管理员' : '普通用户' }}</p>
            </div>
            <div v-else>
              <p class="login-prompt" @click="goToLogin">点击登录</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取用户信息
      isAuthenticated: 'isAuthenticated', // 检查是否已登录
    })
  },
  methods: {
    goToAdminPanel() {
      this.$router.push('/adminpanel');
    },
    goHome() {
      this.$router.push('/home');
    },
    goToCommunity() {
      this.$router.push('/community');
    },
    goToCatalog() {
      this.$router.push('/catalog');
    },
    handleUserAvatarClick() {
      if (this.currentUser.userRole === 'guest') {
        this.goToLogin();
      } else {
        this.goToUserProfile();
      }
    },
    goToUserProfile() {
      this.$router.push('/userprofile');
    },
    goToLogin() {
      this.$router.push('/login');
    }
  },
  mounted() {
    // 确保用户信息已经加载
    if (!this.currentUser) {
      this.$store.dispatch('fetchCurrentUser'); // 如果没有用户信息，获取用户信息
    }
  }
};
</script>

<style scoped>

.header {
  display:flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  background-color: rgba(70, 180, 118, 0.8); /* 使用 rgba 设置透明度 */
  color: white;
  z-index: 10; /* 提高 z-index，确保 header 在其他内容上层 */
  width:98%;
  position: fixed;
  height: 60px; /* 或者你想设置的其他适合的高度 */
}
.user-avatar-wrapper img {
  width: 40px; /* 设置头像图片的固定宽度 */
  height: 40px; /* 设置头像图片的固定高度 */
  border-radius: 50%; /* 如果想要圆形头像 */
  object-fit: cover; /* 确保图片按比例填充，避免变形 */
  cursor: pointer;
}
.logo {
  font-family: 'Caveat-VariableFont', 'ZhiMangXing-Regular', sans-serif;
  font-size: 28px;
  font-weight: bold;
}

.nav-user-container {
  display: flex;
  align-items: center;
  gap: 20px; /* 按钮与头像之间的间距 */
}

.nav-links {
  display: flex;
  gap: 20px; /* 按钮之间的间距 */
}

.nav-button {
  background: none;
  border: none;
  color: white;
  font-size: 18px;
  cursor: pointer;
  transition: color 0.3s;
}

.nav-button:hover {
  color: #ffcc00; /* 鼠标悬停时按钮变色 */
}

.user-info {
  position: relative;
}

.user-avatar-wrapper {
  position: relative;
  display: inline-block;
}

.user-info-list {
  position: absolute;
  top: 50px;
  left: -200px!important;
  background-color: rgba(255, 255, 255, 0.95);
  border: 2px solid #46b476;
  border-radius: 8px; 
  padding: 15px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
  z-index: 10;
  min-width: 200px;
  opacity: 0;
  transform-origin: top;
  transform: translateY(0px) scale(0.05);
  transition: opacity 0.3s ease, transform 0.3s ease;
  pointer-events: none;
}

.user-avatar-wrapper:hover .user-info-list {
  opacity: 1;
  transform: translateY(0) scale(1);
  pointer-events: auto;
}

.user-info-list p {
  margin: 2px 0;
  font-size: 14px;
  color: #333;
  font-family: '宋体','ZhiMangXing-Regular', sans-serif;
}

.login-prompt {
  color: blue;
  cursor: pointer;
}

.login-prompt:hover {
  text-decoration: underline;
}
</style>

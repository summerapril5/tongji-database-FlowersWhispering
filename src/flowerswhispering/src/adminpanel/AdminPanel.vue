<template>
  <div class="admin-panel-page">


    <!-- 侧边栏导航 -->
    <aside class="top-navbar">
      <nav>
        <ul>
          <li @click="currentView = 'users'" :class="{ active: currentView === 'users' }">用户管理</li>
          <li @click="currentView = 'posts'" :class="{ active: currentView === 'posts' }">帖子管理</li>
          <li @click="currentView = 'plants'" :class="{ active: currentView === 'plants' }">植物审核</li>
          <li @click="currentView = 'feedbacks'" :class="{ active: currentView === 'feedbacks' }">用户反馈</li>
          <li @click="currentView = 'announcements'" :class="{ active: currentView === 'announcements' }">公告发布</li>
          <li @click="toggleHomeDropdown" :class="{ 'fast-navigater': true, 'active-dropdown': showHomeDropdown }">快速导航
          </li> <!-- 下拉选择栏 -->
          <ul v-if="showHomeDropdown" class="dropdown">
            <li @click="goToHomeSection">返回主页</li>
            <li @click="goToCatalogueSection">图鉴部分</li>
            <li @click="goToCommunitySection">社区部分</li>
          </ul>
        </ul>
      </nav>
    </aside>

    <!-- 主内容区域 -->
    <main class="main-content">
      <!-- 用户管理视图 -->
      <div v-if="currentView === 'users'">
        <h2>用户管理</h2>

        <ul class="item-list">
          <li v-for="user in filteredUsers" :key="user.userId" class="item">
            <div class="item-details">
              <p><strong>ID:</strong> {{ user.userId }}</p>
              <p><strong>用户名:</strong> {{ user.userName }}</p>
              <p><strong>邮箱:</strong> {{ user.uemail }}</p>
              <p><strong>角色:</strong>{{ user.userrole }}</p>
            </div>

            <div v-if="replyingUser === user" class="expanded-content my-animation-slide-bottom">
              <form @submit.prevent="sendReply(user.userId)" class="form-reply">
                <textarea v-model="user.replyContent" placeholder="输入回复内容" class="form-input"></textarea>
                <button type="submit" class="btn-submit">发送</button>
              </form>
            </div>
            <div class="item-actions">
              <button @click="toggleReplyForm(user)" class="btn-edit btn-release my-animation-slide-bottom">
                        {{ replyingUser === user ? '取消' : '联系' }}
              </button>

              <button v-if="user.userstatus == 'active'" @click="changeUserState(user)"
                class="btn-edit btn-ban my-animation-slide-bottom">封禁</button>
              <button v-else @click="changeUserState(user)"
                class="btn-edit btn-release my-animation-slide-bottom">解封</button>
            </div>

            
          </li>
        </ul>

      </div>

      <!-- 帖子管理视图 -->
      <div v-if="currentView === 'posts'">
        <h2>帖子管理</h2>
        <ul class="item-list">s
          <li v-for="post in posts" :key="post.articleId" class="item">
            <div class="item-details">
              <p><strong>标题:</strong> {{ post.title }}</p>
              <p><strong>发布人:</strong> {{ post.username }}</p>
              <p><strong>发布时间:</strong> {{ post.publishedDate }}</p>
              <button @click="toggleExpandPost(post)" class="btn-details my-animation-slide-top">
                {{ expandedPost === post ? '收起' : '展开' }}内容
              </button>
            </div>

            <div v-if="expandedPost === post" class="expanded-content my-animation-slide-bottom">
              <p>{{ post.content }}</p>
            </div>

            <div class="item-actions">
              <button @click="deletePost(post.articleId)" class="btn-delete my-animation-slide-bottom">删除</button>
            </div>
          </li>
        </ul>
      </div>
      <!-- 植物审核视图 -->
      <div v-if="currentView === 'plants'" class="plants-review-section">
        <!-- 已通过审核 -->
        <div class="plants-column">
          <h3>已通过审核</h3>
          <li v-for="review in approvedReviews" :key="review.reviewId" class="table-row">
            <div class="item-details">
              <p><strong>修改时间:</strong> {{ review.submittedDate }}</p>
              <p><strong>修改用户:</strong> {{ review.submitterId }}</p>
              <p><strong>植物ID:</strong>{{ review.plantId }}</p>
              <button @click="toggleExpandedaApprovedReviews(review)" class="btn-details">
                {{ expandedApprovedReviews === review ? '收起' : '展示' }}修改信息
              </button>
            </div>
            <div v-if="expandedApprovedReviews === review" class="expanded-content">
              <p>{{ review.modifiedContent }}</p>
            </div>
          </li>

        </div>

        <!-- 待审核 -->
        <div class="plants-column">
          <h3>待审核</h3>
          <li v-for="review in pendingReviews" :key="review.reviewId" class="table-row">
            <div class="item-details">
              <p><strong>修改时间:</strong> {{ review.submittedDate }}</p>
              <p><strong>修改用户:</strong> {{ review.submitterId }}</p>
              <p><strong>植物ID:</strong>{{ review.plantId }}</p>
              <button @click="toggleExpandePendingReviews(review)" class="btn-details">
                {{ expandedPendingReviews === review ? '收起' : '展示' }}修改信息
              </button>
            </div>
            <div v-if="expandedPendingReviews === review" class="expanded-content">
              <p>{{ review.modifiedContent }}</p>
            </div>
            <div class="item-actions plant-btn">
              <button @click="approveRev(review.reviewId)"
                class="btn-edit btn-release my-animation-slide-bottom">通过</button>
              <button @click="rejectRev(review.reviewId)" class="btn-edit btn-ban my-animation-slide-bottom">拒绝</button>
            </div>
          </li>
        </div>
      </div>

      <!-- 用户反馈视图 -->
      <div v-if="currentView === 'feedbacks'">
        <h2>用户反馈</h2>
        <ul class="item-list">
          <li v-for="feedback in feedbacks" :key="feedback.feedbackId" class="item">
            <div class="item-details">
              <p><strong>反馈用户:</strong> {{ feedback.userId }}</p>
              <button @click="toggleExpandFeedback(feedback)" class="btn-details my-animation-slide-bottom">
                {{ expandedFeedback === feedback ? '收起' : '展开' }}详情
              </button>
            </div>
            <div v-if="expandedFeedback === feedback" class="expanded-content my-animation-slide-bottom">
              <p>{{ feedback.feedbackContent }}</p>
              <p><strong>反馈提交时间:</strong> {{ feedback.submittedDate }}</p>
            </div>
          </li>
        </ul>
      </div>

      <!-- 公告管理视图 -->
      <div v-if="currentView === 'announcements'">
        <h2>公告管理</h2>
        <button @click="toggleAddAnnouncementForm" class="btn-toggle-form my-animation-slide-top">
          {{ showAddAnnouncementForm ? '取消' : '添加公告' }}
        </button>

        <form v-if="showAddAnnouncementForm" @submit.prevent="handleAddAnnouncement" class="form-container">
          <div class="form-group">
            <label for="announcement-title">标题</label>
            <input id="announcement-title" v-model="newAnnouncement.title" placeholder="请输入标题" class="form-input" />
          </div>
          <div class="form-group">
            <label for="announcement-content">内容</label>
            <textarea id="announcement-content" v-model="newAnnouncement.content" placeholder="请输入内容"
              class="form-input"></textarea>
          </div>
          <button type="submit" class="btn-submit">提交公告</button>
        </form>

        <ul class="item-list assignment">
          <li v-for="announcement in announcements" :key="announcement.announcementId" class="item">
            <div class="item-details">
              <p><strong>标题:</strong> {{ announcement.title }}</p>
              <p><strong>发布者:</strong> {{ announcement.publisherId }}</p>
              <p><strong>发布日期:</strong> {{ announcement.publishedDate }}</p>
              <button @click="toggleExpandAnnouncement(announcement)" class="btn-details">
                {{ expandedAnnouncement === announcement ? '收起' : '展示' }}内容
              </button>
            </div>

            <div v-if="expandedAnnouncement === announcement" class="expanded-content">
              <p>{{ announcement.content }}</p>
            </div>
            <div class="item-actions">
              <button @click="handleDeleteAnnouncement(announcement.announcementId)"
                class="btn-delete my-animation-slide-bottom">删除</button>
            </div>
          </li>
        </ul>
      </div>

    </main>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';

export default {
  name: 'AdminPanel',
  data() {
    return {
      currentView: 'users',
      showAddUserForm: false,
      showAddAnnouncementForm: false,
      expandedPost: null,
      expandedFeedback: null,
      expandedAnnouncement: null, // 当前展开的公告
      expandedApprovedReviews: null,//当前展开的已审核植物信息
      expandedPendingReviews: null,//当前展开的未审核植物信息
      replyingUser: null, // 当前回复的用户
      replyContent: '', // 存储回复内容
      showHomeDropdown: false, // 控制返回主页下拉菜单显示
      newUser: {
        username: '',
        email: '',
        password: '',
        role: ''
      },
      newAnnouncement: {
        title: '',
        content: ''
      }

    };
  },
  computed: {
    ...mapGetters(['getAllUsers', 'getPosts', 'getAllFeedbacks', 'getAnnouncements', 'getReviews']),
    users() {
      console.log('从 Vuex 获取的用户数据:', this.getAllUsers); // 调试信息
      return this.getAllUsers;
    },
    filteredUsers() {
      return this.users.filter(user => user.userrole === 'user');
    },
    posts() {
      return this.getPosts;
    },
    comments() {
      return this.allComments;
    },
    feedbacks() {
      return this.getAllFeedbacks;
    },
    announcements() {
      //console.log('组件中的公告数据:', this.getAnnouncements); // 调试信息
      return this.getAnnouncements;
    },
    approvedReviews() {
      return this.getReviews.filter(review => review.reviewStatus === '审核通过');
    },
    pendingReviews() {
      return this.getReviews.filter(review => review.reviewStatus === '待审核');
    },

  },
  methods: {
    ...mapActions(['banUser', 'unblockUser', 'deletePost', 'deleteComment', 'deleteAnnouncement', 'publishAnnouncement',
      'fetchAnnouncements', 'fetchAllFeedbacks', 'fetchAllReviews', 'fetchAllPosts', 'fetchAllUsers',
      'approveReview', 'rejectReview','replyToUser']),

    //管理员联系用户相关方法
    toggleReplyForm(user) {
      if (this.replyingUser === user) {
        this.replyingUser = null;
      } else {
        this.replyingUser = user;
        user.replyContent = ''; // 重置输入框内容
      }
    },
    async sendReply(userId) {
      const content = this.replyingUser.replyContent;
      if (content.trim()) {
        try {
          const success = await this.replyToUser({ userId, content });
          if (success) {
            alert('回复成功');
            this.replyingUser = null; // 关闭文本框
          } else {
            alert('回复失败');
          }
        } catch (error) {
          alert('回复失败: ' + error.message);
        }
      } else {
        alert('请输入回复内容');
      }
    },  

    changeUserState(user) {
      this.fetchAllUsers();
      if (user.userstatus == 'active')
        this.banUser(user.userId);
      else
        this.unblockUser(user.userId);
      this.fetchAllUsers();
    },


    toggleExpandAnnouncement(announcement) {
      if (this.expandedAnnouncement === announcement) {
        this.expandedAnnouncement = null;
      } else {
        this.expandedAnnouncement = announcement;
      }
    },
    toggleExpandedaApprovedReviews(review) {
      if (this.expandedApprovedReviews === review) {
        this.expandedApprovedReviews = null;
      } else {
        this.expandedApprovedReviews = review;
      }
    },
    toggleExpandePendingReviews(review) {
      if (this.expandedPendingReviews === review) {
        this.expandedPendingReviews = null;
      } else {
        this.expandedPendingReviews = review;
      }
    },



    toggleHomeDropdown() {
      this.showHomeDropdown = !this.showHomeDropdown;
    },
    goToHomeSection() {
      this.$router.push({ name: 'Home' });
      this.showHomeDropdown = false;
    },
    goToCatalogueSection() {
      this.$router.push('/catalog');
      this.showHomeDropdown = false;
    },
    goToCommunitySection() {
      this.$router.push('/community');
      this.showHomeDropdown = false;
    },
    toggleAddUserForm() {
      this.showAddUserForm = !this.showAddUserForm;
    },
    handleAddUser() {
      if (this.newUser.username && this.newUser.email && this.newUser.password) {
        this.addUser({ ...this.newUser });
        this.newUser = { username: '', email: '', password: '', role: '' };
        this.showAddUserForm = false;
      }
    },
    handleDeletePost(postId) {
      this.deletePost(postId);
    },
    handleDeleteComment(commentId) {
      this.deleteComment(commentId);
    },
    toggleAddAnnouncementForm() {
      this.showAddAnnouncementForm = !this.showAddAnnouncementForm;
    },
    handleAddAnnouncement() {
      if (this.newAnnouncement.title && this.newAnnouncement.content) {
        const newAnnouncement = {
          ...this.newAnnouncement,
          id: Date.now(),
          date: new Date().toLocaleDateString()
        };
        this.publishAnnouncement(newAnnouncement);
        this.newAnnouncement = { title: '', content: '' };
        this.showAddAnnouncementForm = false;
      } else {
        alert('请填写公告的标题和内容');
      }
    },
    handleDeleteAnnouncement(announcementId) {
      this.deleteAnnouncement(announcementId);
    },

    toggleExpandPost(post) {
      this.expandedPost = this.expandedPost === post ? null : post;
    },
    toggleExpandFeedback(feedback) {
      this.expandedFeedback = this.expandedFeedback === feedback ? null : feedback;
    },
    toggleReplyForm(user) {
      this.replyingUser = this.replyingUser === user ? null : user;
    },
    //审核review
    approveRev(reviewId) {
      this.fetchAllReviews();
      this.approveReview(reviewId);
      this.fetchAllReviews();
    },
    rejectRev(reviewId) {
      this.fetchAllReviews();
      this.rejectReview(reviewId);
      this.fetchAllReviews();
    },
    //植物管理
    editPlant(plant) {
      plant.editing = true;
    },
    finishEditPlant(plant) {
      plant.editing = false;
    },
    approvePlant(plant) {
      plant.approved = true;
    },
    deletePlant(plantId) {
      this.$store.commit('DELETE_PLANT', plantId);
    },
  },
  mounted() {
    this.fetchAnnouncements();
    this.fetchAllFeedbacks();
    this.fetchAllReviews();
    this.fetchAllPosts();
    this.fetchAllUsers();
  }
}

</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');

.admin-panel-page {
  min-height: 100vh;
  background-image: url('\\images\\ground.jpg');
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
  /* 背景固定，不随页面滚动 */
  padding: 20px;
  box-sizing: border-box;
}

.top-navbar {
  background-color: rgba(15, 123, 47, 0.7);
  padding: 15px 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.5);
  z-index: 10;
  position: fixed;
  width: 8%;
  height: 100%;
  top: 0;
  left: 0;
}

.top-navbar ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
}

.top-navbar li {
  padding: 20px 33px;
  cursor: pointer;
  border-radius: 4px;
  color: rgb(255, 255, 255);
  font-weight: bold;
  margin-bottom: 10px;
  /* 每条数据条之间的间距 */
  transition: background-color 0.3s ease;
}

.top-navbar li:hover,
.top-navbar li.active {
  background-color: #3fcea1;
  /* 其他导航项的悬停颜色 */
}

/* 快速导航按钮悬停时的颜色 */
.fast-navigater:hover {
  background-color: #631b1b;
  /* 鼠标悬停时更亮的绿色 */
  color: #f5f5f5;
  /* 悬停时的字体颜色 */
}

/* 快速导航按钮样式 */
.fast-navigater {
  background-color: #3fce6a;
  /* 默认背景色 */
  color: #ecf0f1;
  /* 浅色字体 */
  font-size: 14px;
  font-weight: bold;
}



/* 下拉菜单激活时 */
.active-dropdown {
  background-color: #44f27e;
  /* 激活状态时的背景色 */
}

/* 下拉菜单样式 */
.dropdown {
  margin-top: 50px;
  background-color: #1b4228;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);
  list-style: none;
  padding: 0;
}

.dropdown li {
  padding: 15px 20px;
  cursor: pointer;
  color: #ace6c5;
  font-size: 13px;
}

.dropdown li:hover {
  background-color: #85eeb1;
  color: #43b052;
}

.main-content {
  margin-top: 120px;
  /* 增加 margin-top 值，将内容向下移动 */
  flex: 1;
  width: 83%;
  margin-left: 11%;
  padding: 40px;
  background-color: rgba(255, 255, 255, 0.9);
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  overflow-y: auto;
}


h2 {
  font-size: 24px;
  color: #333;
  margin-top: 0px;
  margin-bottom: 20px;
}

.item-list {
  list-style: none;
  align-items: center;
  /* 垂直居中 */
  padding: 0;
  margin-top: 10px;
  width: 80vw;
  display: flex;
  /* 启用 Flexbox 布局 */
  flex-wrap: wrap;
  /* 允许换行 */
  gap: 10px;
  /* 设置列和行之间的间距 */
}

.item-list li {
  display: flex;
  /* 让内容在同一行内对齐 */
  align-items: center;
  /* 垂直居中 */
  width: calc(49% - 5px);
  /* 设置每条数据条占父容器宽度的50%，并减去一半的间距 */
  padding: 15px 20px;
  /* 增加内部间距，使条目显得更宽 */
  margin-bottom: 10px;
  /* 每条数据条之间的间距 */
  background-color: #ffffff;
  /* 背景色，便于视觉区分 */
  border-radius: 8px;
  /* 圆角效果 */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  /* 添加阴影 */
  box-sizing: border-box;
  /* 包含内边距和边框在内 */
}

.assignment {
  height: 120px;
}

.item {
  background-color: #f9f9f9;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(153, 97, 97, 0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 50px;
  /* 设置宽度为父容器的 100%，使其适应 flex 布局 */
}

.item-details p {
  margin: 10px;
  color: #238d89;
}



.item-actions {
  height: 20%;
  display: flex;
  gap: 10px;
}

.plant-btn {
  margin-top: 7%;
}

.btn-ban {
  background-color: #f00;
  color: #fff;
}

.btn-release {
  background-color: rgb(112, 157, 112);
  color: #fff;
}


.btn-toggle-form,
.btn-submit,
.btn-edit,
.btn-approve,
.btn-delete,
.btn-details {
  border: none;
  padding: 8px 16px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease-in-out;
  font-size: 14px;
}

.btn-toggle-form:hover,
.btn-submit:hover,
.btn-edit:hover,
.btn-approve:hover,
.btn-details:hover {
  background-color: #128a22;
}

.btn-delete:hover,
.btn-ban:hover {
  background-color: #b00b0b
}


.form-container {
  background-color: rgba(255, 255, 255, 0.9);
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  gap: 20px;
  /* 设置输入框之间的间距 */
  animation: fade-in 0.5s ease-out forwards;
}

.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-size: 14px;
  margin-bottom: 5px;
  color: #333;
}

.form-input {
  margin-bottom: 10px;
  padding: 10px;
  border: 2px solid #ced4da;
  border-radius: 8px;
  font-size: 16px;
  background-color: #ffffff;
}

textarea.form-input {
  height: 100px;
  resize: none;
}

.expanded-content {
  margin-top: 10px;
  padding: 10px;
  background-color: #e9ecef;
  border-radius: 8px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

/*联系用户相关的css*/
.form-reply {
  background-color: rgba(255, 255, 255, 0.9);
  padding: 10px 50px;
  border-radius: 15px; /* 更圆润的边角 */
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  display: flex; /* 使用 Flexbox 布局 */
  flex-direction: column; /* 元素垂直排列 */
  align-items: center; /* 水平居中 */
  justify-content: center; /* 垂直居中 */
  gap: 20px;
  animation: fade-in 0.5s ease-out forwards;
}

.form-input,
textarea.form-input {
  border: 2px solid #ced4da;
  border-radius: 12px; /* 更圆润的边角 */
  font-size: 16px;
  background-color: #ffffff;
}

textarea.form-input {
  width: 100%; /* 宽度充满容器 */
  padding: 5px 30px;
  height: 100px;
  resize: none; /* 阻止调整大小 */
}

.btn-submit {
  border: none;
  padding: 10px 20px;
  border-radius: 12px; /* 更圆润的边角 */
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease-in-out;
  font-size: 14px;
}

.btn-submit:hover {
  background-color: #128a22; /* 鼠标悬停时更亮的绿色 */
}


/* PLANTS*/
/* 省略其他样式，只展示植物详情样式 */

.plant-details {
  background-color: rgba(255, 255, 255, 0.9);
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  margin-top: 20px;
}

.plant-details .form-group {
  margin-bottom: 15px;
}

.plant-details label {
  font-weight: bold;
  margin-bottom: 5px;
  display: block;
}

.plant-details .form-input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ced4da;
  border-radius: 8px;
  background-color: #e9ecef;
  color: #495057;
  resize: none;
}

/**/
.plants-review-section {
  width: 100%;
  display: flex;
  justify-content: space-between;
  gap: 12px;
}

.plants-column {
  width: 50%;
  background-color: #fff;
  padding: 10px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  overflow-y: auto;
  /* 添加滚动条 */
}

.table-header,
.table-row {
  display: flex;
  justify-content: space-between;
  padding: 10px;
  background-color: #f5f5f5;
  border-bottom: 1px solid #ccc;
}

.table-header {
  font-weight: bold;
}

.table-row {
  background-color: #f9f9f9;
  margin-bottom: 5px;
}

.plant-image {
  width: 50px;
  height: 50px;
  object-fit: cover;
  border-radius: 4px;
}

.form-input {
  width: 100%;
  padding: 5px;
  margin: 2px 0;
}

.btn-edit,
.btn-submit,
.btn-approve,
.btn-delete {
  margin-left: 10px;
  cursor: pointer;
}

/* Slide-in and fade-in animations */
.my-animation-slide-top {
  animation: slide-top 1s ease-out forwards;
}

.my-animation-slide-bottom {
  animation: slide-bottom 1s ease-out forwards;
}

.my-animation-fade-in {
  animation: fade-in 0.5s ease-out forwards;
}


@keyframes fade-in {
  0% {
    opacity: 0;
    transform: translateY(-10px);
  }

  100% {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes slide-top {
  0% {
    opacity: 0;
    transform: translateY(-20%);
  }

  100% {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes slide-bottom {
  0% {
    opacity: 0;
    transform: translateY(20%);
  }

  100% {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>

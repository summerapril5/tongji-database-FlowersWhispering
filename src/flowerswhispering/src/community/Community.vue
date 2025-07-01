<template>

  <!--大标题-->
  <Header />
  <!--大标题-->


  <div class="book-background">
    <!-- 视频容器 -->
    <div id="videoContainer">
      <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
        <source src="../assets/video/background.mp4" type="video/mp4">
      </video>
    </div>
  </div>


  <!-- 用户操作界面容器 ，所有界面内的按钮，文本都在此改动-->
  <div class="user-interfacer">
    <!-- 竖条图片容器 -->
    <div class="side-bar">
      <div class="user-info">
        <p class="user-name">{{ userName }}</p> <!--显示用户名-->

        <div class="tabs"> <!--显示选项卡-->

          <button :class="{ active: activeTab === 'HotPosts' }" @click="setActiveTab('HotPosts')">精选帖子</button>
          <button :class="{ active: activeTab === 'PersonalCenter' }"
            @click="setActiveTab('PersonalCenter')">个人中心</button>


          <button :class="{ active: activeTab === 'SentPost' }" @click="setActiveTab('SentPost')">帖子发布</button>
          <button :class="{ active: activeTab === 'ContributorsList' }"
            @click="setActiveTab('ContributorsList')">贡献榜单</button>
          <button :class="{ active: activeTab === 'home' }" @click="GoToHome">返回首页</button>

        </div>
      </div>
    </div>

    <div class="top-banner">
      <button class="user-button" @click="Gotouserpage"> <!--用户头像-->
        <img src="./images/logo.png" alt="User" />

      </button>
    </div>



    <!--个人中心部分-->
    <div v-if="activeTab === 'PersonalCenter'">

      <!--功能点按钮设置卡片效果-->
      <div class="card-container">
        <div class="love-time-title1">
          欢迎来到叶语花谣社区
        </div>
      </div>



      <div class="card-container1">
        <div class="card" @click="GotoPostDetil()">
          <img src="./images/button1.png" alt="Card 1" />
          <div class="card-info">我的帖子</div>
        </div>
        <div class="card" @click="GoToMycomment()">
          <img src="./images/button2.png" alt="Card 2" />
          <div class="card-info">我的评论</div>
        </div>
        <div class="card" @click="GotoMyfavourite()">
          <img src="./images/button3.png" alt="Card 3" />
          <div class="card-info">我的收藏</div>
        </div>
      </div>
      <!--功能点按钮，设置卡片效果-->
    </div>
    <!--个人中心部分-->



    <!--公告部分-->
    <div v-if="activeTab === 'Announcement'" class="card-container">
      <div class="card-container">
        <div class="love-time-title1">
          欢迎来到叶语花谣社区
        </div>
      </div>


    </div>
    <!--公告部分-->
      

    <!--发帖部分-->
    <div v-if="activeTab === 'SentPost'">
      <div class="post-container">
        <div class="post-title">
          发布帖子
        </div>
        <form @submit.prevent="submitPost">



          <div class="form-group">
            <label for="title" class="form-label">标题:</label>
            <input type="text" id="title" v-model="title" class="form-input" required />
          </div>
          <div class="form-group">
            <label for="content" class="form-label">内容:</label>
            <textarea id="content" v-model="content" class="form-textarea" rows="5" required></textarea>
          </div>
          <div class="form-buttons">
            <button type="submit" class="btn-submit">发布</button>
            <button type="button" @click="discard" class="btn-cancel">放弃</button>
          </div>
        </form>
      </div>

    </div>
    <!--发帖部分-->

  <!--热帖部分-->
  <div v-if="activeTab === 'HotPosts'">

<div class="news-carousel">
    <div class="news-wrapper">
      <div class="news-item" :style="{ transform: `translateX(${-currentNewsIndex * 100}%)` }"  v-for="(news, index) in newsList" :key="index">
        {{ news }}
      </div>
    </div>
    <!-- Modal for displaying selected news -->
    <div v-if="isModalVisible" class="modal-overlay" @click="hideModal">
      <div class="modal-content" @click.stop>
        <h3>新闻详情</h3>
        <p>{{ selectedNews }}</p>
        <button @click="hideModal">关闭</button>
      </div>
    </div>
  </div>



      <div class="information-box1"> <!--搜索框，重要部分！！！-->
        <input type="text" v-model="searchQuery" placeholder="搜索你喜欢的帖子！" class="information-input1" />
        <button @click="searchDatabase" class="information-button1">搜索</button>
      </div>


      <div class="post-container1">
        <h1 class="post-header1">帖子列表</h1>
        <ul class="post-list1">
          <li v-for="post in paginatedPosts" :key="post.articleId" class="post-item1" @click="showPostDetails(post)">
            <div>
              <h2 class="post-title1">{{ post.title }}</h2>
              <p class="post-summary1">{{ summarizeContent(post.content) }}</p>
            </div>
            <div class="post-info1">
              <span class="post-author1">{{ post.username }}</span>
              <span class="post-time1">{{ post.publishedDate }}</span>
            </div>
          </li>
        </ul>
        <div class="pagination-controls1">
          <button @click="navigateToPrevious" :disabled="currentPage === 1">上一页</button>
          <button @click="navigateToNext" :disabled="currentPage === totalPagesCount">下一页</button>
        </div>

        <!-- Post Details Modal -->
        <div v-if="showModal" class="modal-overlay" @click="closeModal">
          <div class="modal-content" @click.stop>
            <h2 class="modal-title">{{ selectedPost.title }}</h2>
            <p class="modal-content">{{ selectedPost.content }}</p>
            <div class="modal-comments">
              <h3>评论：</h3>
              <ul>
                <li v-for="comment in postComment" :key="comment.commentId">{{ comment.content }}</li>
              </ul>
            </div>
            <br>
            <div class="modal-new-comment">
              <textarea v-model="newComment" placeholder="添加评论..."></textarea>
              <button @click="addComment">提交评论</button>
            </div>

            <button class="modal-close-button" @click="closeModal">关闭</button>
          </div>
        </div>
      </div>



    </div>
    <!--热帖部分-->





    <!-- 用户贡献榜部分 -->
    <div v-if="activeTab === 'ContributorsList'" class="contributors-container">
      <h2 class="contributors-title">用户贡献榜</h2>
      <ul class="contributors-list">
        <li v-for="(score, index) in paginatedScores" :key="score.userId" class="contributor-item">
          <span class="user-rank">{{ index + 1 }}</span> <!-- 使用 index + 1 显示序号 -->
          <span class="user-name">{{ score.username }}</span>
          <span class="user-contribution">贡献分数: {{ score.totalScore }}</span>
        </li>
      </ul>
      <div class="pagination">
        <button @click="navigateToPrevious" :disabled="currentPage === 1">上一页</button>
        <span>页码 {{ currentPage }} / {{ totalPages }}</span>
        <button @click="navigateToNext" :disabled="currentPage === totalPages">下一页</button>
      </div>
    </div>
    <!-- 用户贡献榜部分 -->




  </div>

  <Footer />

</template>

<script>
import { defineComponent } from 'vue';
import { mapGetters, mapActions } from 'vuex';
import Header from '@/home/Header.vue';
import Footer from '@/home/Footer.vue';
export default {
  components: {
    Header,
    Footer,
  },
  name: "Community",
  data() {
    return {
      buttonImageUrl: '../catalog/images/user_example.png',  // 默认图片，后端接入用户头像
      userName: 'Wuhuairline', // 默认用户名,后端接入用户姓名
      activeTab: "HotPosts",
      isUserInfoVisible: false, // 控制用户信息列表的显示与隐藏

      allPosts: [],
      userPosts: [],
      userScore: [],
      postComment: [],

      currentPage: 1,

      scorePerPage: 5,// 每页显示的用户数
      // 发帖
      title: '',
      content: '',
      //帖子列表
      showModal: false,
      selectedPost: null,
      newComment: '',
      currentPage: 1,

      itemsPerPage: 10,
      showDeleteConfirm: false // 添加此行
    };
  },

  mounted() {
    window.scrollTo(0, 0);
    this.startCarousel();
    this.getAllPosts();
    this.getUserPosts();
    this.getUserScore();
  },
  computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
      isAdmin: 'isAdmin',
    }),
    paginatedScores() {
      const start = (this.currentPage - 1) * this.scorePerPage;
      const end = this.currentPage * this.scorePerPage;
      return this.userScore.slice(start, end); // 返回当前页的用户分数列表
    },
    totalPages() {
      return Math.ceil(this.userScore.length / this.scorePerPage); // 计算总页数
    },

    paginatedPosts() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = this.currentPage * this.itemsPerPage;
      return this.allPosts.slice(start, end);
    },
    // 计算总页数
    totalPagesCount() {
      return Math.ceil(this.allPosts.length / this.itemsPerPage);
    }
  },
  methods:{
    ...mapActions(['logout']),
    async getAllPosts() {
      try {
        const result = await this.$store.dispatch('fetchAllPosts');
        console.log('帖子加载成功:', result);
        this.allPosts = result;
      } catch (error) {
        console.error('加载帖子失败:', error);
      }
    },
    async getUserPosts() {
      try {
        const result = await this.$store.dispatch('fetchUserPosts', this.currentUser.userId);
        console.log('帖子加载成功:', result);
        this.userPosts = result;
      } catch (error) {
        console.error('加载帖子失败:', error);
      }
    },
    async getUserScore() {
      try {
        const result = await this.$store.dispatch('fetchScoreRank');
        console.log('分数:', result);
        this.userScore = result;
      } catch (error) {
        console.error('加载帖子失败:', error);
      }
    },
    async fetchPostComments(articleId) {
      try {
        const response = await this.$store.dispatch('fetchCommentsByPost', articleId);
        this.postComment = response; // 将评论数据存储在组件中
      } catch (error) {
        console.error('获取帖子评论失败:', error);
      }
    },
    async searchDatabase() {
      console.log('搜索按钮点击成功');  // 添加调试信息

      if (this.searchQuery.trim() === '') {
        alert('请输入搜索内容');
        this.getAllPosts();
        return;
      }

      try {
        console.log('搜索请求发送中:', this.searchQuery);  // 调试信息
        const response = await this.$store.dispatch('searchPosts', this.searchQuery);
        console.log('搜索成功:', response);  // 调试信息
        if (response) {
          //console.log('搜索成功:', response);  // 调试信息
          this.allPosts = response;
          this.currentPage = 1;  // 搜索后重置为第一页
        }
      } catch (error) {
        console.error('搜索失败:', error);  // 捕获并显示错误
        alert('搜索失败，请稍后再试');
      }
    },

    handleUserAvatarClick() {
      if (this.currentUser.role === 'guest') {
        this.$router.push('/login'); // 如果是guest用户，点击跳转到登录页面
      } else {
        this.goToUserProfile(); // 否则跳转到个人主页
      }
    },
    setActiveTab(tabName) {
      this.activeTab = tabName;
    },  //设置选项卡

    goToDetail(id) {
      // 使用 Vue Router 导航到帖子详情页
      this.$router.push({ name: 'PostDetail', params: { id } });

    },
    startCarousel() {
      setInterval(() => {
        this.currentNewsIndex = (this.currentNewsIndex + 1) % this.newsList.length;
      }, 5000); // 每5秒切换一次新闻
    },

    // 前一页
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    // 下一页
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },

    async submitPost() {
      // 确保标题和内容不是空的
      if (!this.title || !this.content) {
        alert('请填写标题和内容');
        return;
      }

      try {
        // 调用 Vuex 的 addPost action
        const success = await this.$store.dispatch('addPost', {
          title: this.title,
          content: this.content
        });

        if (success) {
          alert('帖子已发布成功!');
          this.clearForm(); // 清空表单
        } else {
          alert('发布失败，请稍后再试。');
        }
      } catch (error) {
        console.error('发布失败:', error);
        alert('发布失败，请稍后再试。');
      }
    },
    discard() {
      this.clearForm();
    },
    clearForm() {
      this.title = '';
      this.content = '';
    },

    calculateContribution() {
      // 这里是函数的逻辑
    },

    Gotouserpage() {
      this.$router.push('/userprofile');
    }, //切换用户页面

    GotoPostDetil() {
      this.$router.push('/postDetail');
    }, //切换用户页面

    GoToMycomment() {
      this.$router.push('/myComment');
    },

    GotoMyfavourite() {
      this.$router.push('/myfavourite');
    }, //切换用户页面
    GoToHome() {
      this.$router.push('/home');
    },//返回主页界面

    async fetchPostsData() {
      // 模拟从后端获取数据
      // this.postsData = await fetch('your-api-endpoint').then(response => response.json());
      this.postsData = [
        { id: 1, title: '帖子标题1', content: '这是帖子内容1...', author: '作者1', date: '2024-09-01' },
        { id: 2, title: '帖子标题2', content: '这是帖子内容2...', author: '作者2', date: '2024-09-02' },
        // 你可以添加更多的测试数据
        { id: 3, title: '帖子标题3', content: '这是帖子内容3...', author: '作者3', date: '2024-09-03' },
        { id: 4, title: '帖子标题4', content: '这是帖子内容4...', author: '作者4', date: '2024-09-04' },
        // 确保数据量超过 itemsPerPage 以测试分页功能
        { id: 5, title: '帖子标题5', content: '这是帖子内容5...', author: '作者5', date: '2024-09-05' },
        { id: 6, title: '帖子标题6', content: '这是帖子内容6...', author: '作者6', date: '2024-09-06' },
        { id: 7, title: '帖子标题7', content: '这是帖子内容7...', author: '作者7', date: '2024-09-07' },
        { id: 8, title: '帖子标题8', content: '这是帖子内容8...', author: '作者8', date: '2024-09-08' },
        { id: 9, title: '帖子标题9', content: '这是帖子内容9...', author: '作者9', date: '2024-09-09' },
        { id: 10, title: '帖子标题10', content: '这是帖子内容10...', author: '作者10', date: '2024-09-10' },
        { id: 11, title: '帖子标题11', content: '这是帖子内容11...', author: '作者11', date: '2024-09-11' },
        // 添加更多测试数据以覆盖多个页面
      ];
    },


  Gotouserpage() {
    this.$router.push('/userprofile');
  }, //切换用户页面

  GotoPostDetil() {
    this.$router.push('/postDetail');
  }, //切换用户页面

  GoToMycomment() {
    this.$router.push('/myComment');
  },

  GotoMyfavourite() {
    this.$router.push('/myfavourite');
  }, //切换用户页面
  GoToHome() {
    this.$router.push('/home');
  },//返回主页界面


  summarizeContent(content) {
    return content.length > 100 ? content.slice(0, 100) + '...' : content;
  },
  showPostDetails(post) {
    this.selectedPost = post;
    this.showModal = true;
    this.fetchPostComments(post.articleId);
  },
  closeModal() {
    this.showModal = false;
    this.selectedPost = null;
  },
  async addComment() {
    if (this.newComment.trim() === '') return;
    try {
      // 调用 Vuex 的 addComment action，将评论添加到后端
      const success = await this.$store.dispatch('addComment', {
        articleId: this.selectedPost.articleId,
        content: this.newComment
      });

      if (success) {
        // 如果后端成功添加评论，将评论加入到前端显示的评论列表中
        this.fetchPostComments(this.selectedPost.articleId)
        this.newComment = ''; // 清空输入框
        console.log('评论添加成功');
      } else {
        console.error('评论添加失败');
      }
    } catch (error) {
      console.error('评论添加失败:', error);
    }
  },
  deletePost() {
    this.showDeleteConfirm = true; // 显示删除确认弹窗
  },
  confirmDelete() {
    const index = this.visiblePosts.findIndex(post => post.id === this.selectedPost.id);
    if (index !== -1) {
      this.visiblePosts.splice(index, 1);
      this.closeModal();
    }
    this.showDeleteConfirm = false; // 关闭删除确认弹窗
  },
  cancelDelete() {
    this.showDeleteConfirm = false; // 关闭删除确认弹窗
  },
  navigateToPrevious() {
    if (this.currentPage > 1) this.currentPage--;
  },
  navigateToNext() {
    if (this.currentPage < this.totalPagesCount) this.currentPage++;
  },





  },

};
</script>




<style scoped>
.book-background {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  position: relative;
}

/* 设置背景图像 */
.book-background {
  background-size: cover;
  height: 100vh;
  /* 使背景覆盖整个视口 */
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: -1;
}


/* 操作界面*/
.user-interfacer {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 80%;
  height: 90%;
  background-image: url('./images/backgroundtry.png');
  background-size: cover;
  background-position: center;
  /* 确保背景图片居中 */
  z-index: 2;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  color: white;
  border: 5px solid rgb(36, 156, 172);
  box-sizing: border-box;
  border-radius: 10px;
  opacity: 0.9;
}


/* 竖条图片格式*/

.side-bar {
  position: absolute;
  top: 16.1%;
  /* 从顶部开始对齐 */
  left: 0;
  /* 从左侧开始对齐 */
  width: 140px;
  /* 自定义宽度 */
  height: fill;
  /* 自定义高度 */
  display: flex;
  flex-direction: column;
  align-items: center;
  z-index: 3;
  /* 确保竖条在其他内容之上 */
  background-color: rgb(73, 192, 206);
  /* 背景颜色，使内容更显眼 */
}


.user-info {
  display: flex;
  flex-direction: column;
  align-items: center;
  color: rgb(17, 213, 244);
  /* 设置文本颜色 */
}

.user-name {
  margin: 20px 0;
  /* 自定义用户名上下间距 */
  font-size: 20px;
  /* 自定义用户名字体大小 */
  text-align: center;
  /* 文本居中对齐 */
}

.tabs {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.tabs button {
  background: none;
  border: none;
  color: white;
  font-size: 18px;
  margin: 10px 0;
  cursor: pointer;
  padding: 10px 20px;
  border-radius: 5px;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.tabs button.active {
  background-color: rgba(19, 49, 169, 0.6);
  /* 选中时背景颜色 */
  color: #18c574;
  /* 选中时文本颜色 */
}

.tabs button:hover {
  background-color: rgba(38, 77, 234, 0.289);
  /* 鼠标悬停效果 */
  color: #18c574;
  /* 鼠标悬停时文本颜色 */
}




/* 横条图片格式*/
.top-banner {
  width: 100%;
  /* 横条图片容器宽度占据整个用户界面宽度 */
  display: flex;
  position: absolute;
  top: 0;
  /* 将横条图片定位到容器顶部 */
  left: 0%;
  /* 从左侧开始对齐 */
  z-index: 1;
  /* 确保横条图片在用户界面内容之上 */
}




.top-banner img {
  width: 870px;
  /* 自定义横条宽度 */
  height: 150px;
  /* 自定义高度 */
}

.user-button {
  z-index: 4;
  border: 5px solid rgb(4, 195, 202);
  /* 绿色边框 */
  box-sizing: border-box;
  transition: transform 0.3s ease, background-color 0.3s ease;
  /*动态平滑*/
  cursor: pointer;
  /*指针变化*/
}

.user-button img {
  width: 120px;
  /* 自定义按钮图片的宽度 */
  height: 135px;
  /* 自定义按钮图片的高度 */
}



.card-container1 {
  display: flex;
  flex-wrap: wrap;
  gap: 80px;
  /* 卡片之间的间距 */
  cursor: pointer;
  /*指针变化*/
  margin-top: -50px;
  /* 调整距离顶部的距离 */
}

.card {
  position: relative;
  left: 5%;
  width: 300px;
  /* 自定义卡片宽度 */
  height: 300px;
  /* 自定义卡片高度 */
  overflow: hidden;
  /* 隐藏超出边界的部分 */
  border-radius: 10px;
  /* 圆角效果 */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  /* 卡片阴影 */
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  /* 动态平滑 */
  border-radius: 12px;
  /* 圆角边框 */
}

.card img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  background-color: aliceblue;
}

.card-info {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgb(28, 127, 13);
  color: white;
  /* 文字颜色 */
  font-size: 28px;
  padding: 10px;
  text-align: center;
  transform: translateY(100%);
  /* 初始隐藏 */
  transition: transform 0.3s ease;
  /* 动态平滑 */
  font-family: '黑体', 'ZhiMangXing-Regular', sans-serif;
}



.card:hover {
  transform: scale(1.05);
  /* 卡片放大效果 */
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.3);
  /* 卡片阴影放大效果 */
}

.card:hover .card-info {
  transform: translateY(0);
  /* 鼠标悬停时显示文字 */
}

@keyframes jianBian {
  to {
    background-position: -2000rem;
  }
}

.love-time-title1 {
  position: absolute;
  top: 15%;
  /* 垂直居中 */
  left: 52%;
  /* 水平居中 */
  transform: translate(-50%, -50%);
  /* 精确居中 */
  font-size: 4rem;
  font-weight: 600;
  letter-spacing: 0.2rem;
  line-height: 4rem;
  text-align: center;
  background: linear-gradient(to right, #ff4500, #ffa500, #ffd700, #90ee90, #00ffff, #1e90ff, #9370db, #ff69b4, #ff4500);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  animation: jianBian 60s linear infinite;
  width: 3000px;
  /* 根据需要调整宽度 */

}

/*  搜索框   */

   .information-box1 
  {
    display: flex;
    position:relative;
    left:8%;
    z-index:2;
}

.information-input1 {

  width: 800px;
  padding: 10px;
  border: 2px solid rgb(32, 197, 209);
  /* 绿色边框 */
  border-radius: 45px;
  outline: none;
  transition: border-color 0.3s ease;
  margin-right: 10px;
  /* 搜索框与按钮之间的间距 */
  font-size: 16px;
}

.information-input1:focus {
  border-color: rgb(61, 173, 205);
  /* 聚焦时的边框颜色 */
}

.information-button1 {
  padding: 5px 20px;
  background-color: rgb(61, 220, 228);
  /* 按钮背景色 */
  border: none;
  border-radius: 45px;
  color: white;
  font-size: 20px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.information-button1:hover {
  background-color: rgb(28, 127, 13);
  /* 悬停时的按钮背景色 */
  transform: scale(1.05);
  /* 悬停时放大效果 */
}

/* 帖子详情 */

.post-container1 {
  position: absolute;
  top: 20%;
  left: 20%;
  width: 900px;
  height: 600px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  border: 2px solid rgb(45, 198, 22);
  color: rgb(45, 198, 22);
}

.post-header1 {
  text-align: center;
  font-size: 35px;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}


.post-list1 {
  padding: 0;
  margin: 0;
  list-style-type: none;
  max-height: 480px;
  /* 设置最大高度 */
  overflow-y: auto;
  /* 启用垂直滚动条 */
  overflow-x: hidden;
}


.post-item1 {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  width: 100%;
  transition: transform 0.3s ease;
  cursor: pointer;
  background-color: #d2f0ed;
  margin: 8px 0;
  border-radius: 5px;
}

.post-item1:hover {
  transform: scale(1.05);
}

.post-title1 {
  font-size: 22px;
  margin: 0;
}

.post-summary1 {
  font-size: 18px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.post-info1 {
  display: flex;
  justify-content: space-between;
  font-size: 16px;
}

.post-author1,
.post-time1 {
  color: rgb(45, 198, 22);
}

.pagination-controls1 {
  text-align: center;
  margin-top: 20px;
}

.pagination-controls1 button {
  margin: 0 5px;
}

/* Modal overlay */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Modal content */
.modal-content {
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  max-width: 800px;
  width: 90%;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  position: relative;
}

/* Modal title */
.modal-title {
  margin-top: 0;
  font-size: 1.5rem;
  font-weight: bold;
}

/* Modal content text */
.modal-content {
  font-size: 1rem;
  line-height: 1.6;
}

/* Modal comments */
.modal-comments {
  margin-top: 20px;
}

.modal-comments h3 {
  margin-top: 0;
}

/* Comment list */
.modal-comments ul {
  list-style-type: none;
  padding: 0;
}

.modal-comments li {
  margin-bottom: 10px;
}

/* New comment section */
.modal-new-comment {
  margin-top: 20px;
}

.modal-new-comment textarea {
  width: 100%;
  height: 80px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  resize: vertical;
  box-sizing: border-box;
}

.modal-new-comment button {
  margin-top: 10px;
  padding: 6px 12px;
  font-size: 1rem;
  color: #fff;
  background-color: #007bff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.modal-new-comment button:hover {
  background-color: #0056b3;
}

/* Delete and Close buttons */
.modal-delete-button,
.modal-close-button {
  display: inline-block;
  padding: 6px 12px;
  font-size: 1rem;
  color: #fff;
  background-color: #dc3545;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 10px;
  transition: background-color 0.3s ease;
}

.modal-delete-button:hover {
  background-color: #c82333;
}

.modal-close-button {
  background-color: #6c757d;
}

.modal-close-button:hover {
  background-color: #5a6268;
}

/* Add margin between buttons */
.modal-content button {
  margin-right: 10px;
}

.modal-content button:last-child {
  margin-right: 0;
}


.delete-confirm-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.delete-confirm-content {
  background: #fff;
  padding: 20px;
  border-radius: 5px;
  text-align: center;
}

.delete-confirm-content h2 {
  color: red;
}

.delete-confirm-content p {
  color: red;
}

/* 用户贡献榜 */
/* 用户贡献榜 */
.contributors-container {

  position: absolute;
  top: 8%;
  left: 20%;
  width: 900px;
  height: 600px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  border: 2px solid rgb(45, 198, 22);
  color: rgb(45, 198, 22);

}

.contributors-title {
  text-align: center;
  /* 居中标题 */
  position: relative;
  top: 0;
  font-size: 35px;
  z-index: 6;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}

.contributors-list {
  padding: 0;
  margin: 0;
  list-style-type: none;
  /* 移除默认的列表样式 */
}

.contributor-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  transition: transform 0.3s ease;
  /* 动态效果 */
  cursor: pointer;
  /* 鼠标放在上面改变指针 */
  background-color: #d2f0ed;
  margin: 8px 0;
  border-radius: 5px;
}

.contributor-item:hover {
  transform: scale(1.05);
  /* 放大效果 */
}

.user-rank {
  font-size: 22px;
  font-weight: bold;
  margin-right: 10px;
}

.user-name {
  font-size: 22px;
  transition: color 0.3s ease;
  /* 动态颜色变化效果 */
}

.user-name:hover {
  color: rgb(37, 210, 226);
  /* 悬停时的文字颜色 */
}

.user-contribution {
  font-size: 18px;
}

.pagination {
  text-align: center;
  /* 水平居中对齐 */
}

.pagination button {
  margin: 0 5px;
  /* 可选：按钮之间的间距 */
}

/* 帖子发布 */
.post-container {
  position: absolute;
  top: 8%;
  left: 20%;
  width: 900px;
  height: 600px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  border: 2px solid rgb(45, 198, 22);
  color: rgb(45, 198, 22);
  display: flex;
  flex-direction: column;
}

.post-title {
  text-align: center;
  font-size: 35px;
  color: rgb(45, 198, 22);
  margin-bottom: 20px;
}

.form-content {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.form-group {
  margin-bottom: 15px;
}

.form-label {
  display: block;
  font-size: 18px;
  margin-bottom: 5px;
}

.form-input,
.form-textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
  box-sizing: border-box;
}

.form-input {
  font-size: 16px;
}

.form-textarea {
  font-size: 16px;
}

.form-buttons {
  display: flex;
  justify-content: center;
  margin-top: 240px;
}

.btn-submit,
.btn-cancel {
  font-size: 18px;
  padding: 10px 20px;
  margin: 0 5px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-submit {
  background-color: rgb(45, 198, 22);
  color: #fff;
}

.btn-submit:hover {
  background-color: rgb(37, 210, 226);
}

.btn-cancel {
  background-color: #d2f0ed;
  color: rgb(45, 198, 22);
}

.btn-cancel:hover {
  background-color: rgb(37, 210, 226);
  color: #fff;
}

/* 公告轮播 */
.news-carousel {
  width: 100%;
  max-width: 600px; /* 控制轮播栏的最大宽度 */
  height: 50px;
  overflow: hidden; /* 限制只显示一个公告 */
  background-color: #f8f9fa; /* 更加柔和的背景颜色 */
  position: relative;
  top: -20px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: -360px;
  margin-left: 200px;
  border-radius: 8px; /* 添加圆角 */
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* 添加阴影使其更加立体 */
  border: 1px solid #e0e0e0; /* 添加边框让其更加突出 */
}

.news-wrapper {
  display: flex;
  transition: transform 0.5s ease-in-out; /* 改善切换时的平滑动画 */
  height: 100%;
  align-items: center; /* 保证内容垂直居中 */
}

.news-item {
  flex-shrink: 0;
  width: 100%; /* 每条新闻的宽度与轮播栏一致，确保一次只显示一条 */
  text-align: center;
  padding: 10px;
  font-size: 18px;
  color: #1cc454; /* 优雅的文本颜色 */
  font-weight: bold; /* 加粗文本 */
  background: linear-gradient(to right, #e0f7fa, #e0f4ff); /* 渐变背景 */
  border-radius: 5px; /* 给每条新闻添加圆角 */
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.1); /* 给新闻条目添加阴影 */
  display: flex;
  align-items: center;
  justify-content: center;
}

/* 鼠标悬停时新闻的高亮效果 */
.news-item:hover {
  background: linear-gradient(to right, #a7ffeb, #e0f4ff);
  transform: scale(1.05); /* 鼠标悬停时轻微放大 */
  transition: transform 0.3s ease;
}

/* 小动画效果 */
@keyframes newsItemSlide {
  0% {
    transform: translateX(100%);
  }
  100% {
    transform: translateX(0);
  }
}




/*  固有写法，显示用户*/
.user-avatar-wrapper {
  position: relative;
  display: inline-block;
}

.user-info-list {
  position: absolute;
  top: 50px;
  left: -200px !important;
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
  font-family: '宋体', 'ZhiMangXing-Regular', sans-serif;
}

.logout-button {
  background-color: #ff4d4d;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.logout-button:hover {
  background-color: #ff1a1a;
}

.common-layout {
  margin: 0;
  padding: 0;
  font-family: Arial, sans-serif;
  background-size: cover;
  display: flex;
  flex-direction: column;
  height: 100vh;
  width: 100vw;
  min-width: 1200px;
  min-height: 800px;
  color: #333;
}


.logo {
  font-family: 'Caveat-VariableFont', 'ZhiMangXing-Regular', sans-serif;
  font-size: 28px;
  font-weight: bold;
}

.nav-user-container {
  display: flex;
  align-items: center;
  gap: 20px;
  /* 按钮与头像之间的间距 */
}

.nav-links {
  display: flex;
  gap: 20px;
  /* 按钮之间的间距 */
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
  color: #ffcc00;
  /* 鼠标悬停时变色 */
}

.user-info {
  position: relative;
}

.user-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
}

.user-avatar-wrapper {
  display: flex;
  align-items: center;
}

.user-info-list {
  z-index: 20;
  /* 提高用户信息列表的层级，确保它显示在 header 之上 */
  position: absolute;
  top: 50px;
  right: 0;
  background-color: white;
  /* 为弹出的内容添加背景色，避免透明度导致内容不清晰 */
  padding: 10px;
  border-radius: 5px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  /* 添加阴影效果，确保弹出层次感 */
}

.login-prompt {
  display: flex;
  justify-content: center;
  /* 水平居中对齐 */
  align-items: center;
  /* 垂直居中对齐 */
  height: 100%;
  /* 让内容充满父容器的高度 */
  color: blue;
  cursor: pointer;
  text-align: center;
}

.login-prompt:hover {
  text-decoration: underline;
}



/*  固有写法，显示用户*/

.footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0px 20px;
  background-color: #46b476cc;
  font-size: 15px;
  color: white;
}

.footer .left {
  flex: 1;
  /* 左侧元素占用剩余空间 */
}

.footer .center {
  flex: 1;
  text-align: center;
  /* 居中对齐 */
}

.footer .right {
  flex: 1;
  /* 右侧占位元素占用剩余空间,保持居中 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  text-decoration: underline;
}
</style>
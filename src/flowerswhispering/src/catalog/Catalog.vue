<template>

  <!--大标题-->
  <Header />

  <div class="book-background">
    <!-- 视频容器 -->
    <div id="videoContainer">
      <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
        <source src="../assets/video/background.mp4" type="video/mp4">
      </video>
    </div>
  </div>


  <!-- 用户操作界面容器 ，所有界面内的按钮，文本都在此改动-->
  <div class="user-interface">
    <!-- 竖条图片容器 -->
    <div class="side-bar">
      <div class="user-info">
        <p class="user-name">{{ userName }}</p> <!--显示用户名-->

        <div class="tabs"> <!--显示选项卡-->
          <button @click="GoToHome">返回主页</button>
        </div>
      </div>
    </div>

    <div class="top-banner">
      <button class="user-button" @click="Gotouserpage"> <!--用户头像-->
        <img :src="currentUser.avatar" alt="User" />
      </button>
    </div>

    <!--功能点按钮设置卡片效果-->
    <div class="card-container">
      <div class="card" @click="GoToBook()">
        <img src="../catalog/images/plant_book.png" alt="Card 1" />
        <div class="card-info">搜寻植物信息</div>
      </div>
      <div class="card" @click="GoToRecognition()">
        <img src="../catalog/images/plant_tool.png" alt="Card 3" />
        <div class="card-info">植物识别工具</div>
      </div>
    </div>
    <!--功能点按钮，设置卡片效果-->

  </div>
  <!-- 底部备案号 -->
  <Footer />


</template>

<script>
import { useRouter } from 'vue-router';
import { ref, onMounted, defineComponent } from 'vue';
import { mapGetters, mapActions, mapState } from 'vuex';
import Header from '@/home/Header.vue';
import Footer from '@/home/Footer.vue';
export default {
  name: "Catalog",
  components: {
    Header,
    Footer,
  },
  computed: {
    ...mapGetters({
      currentUser: 'getUserInfo', // 获取当前用户信息
      isAdmin: 'isAdmin',
    }),
  },
  mounted() {
    window.scrollTo(0, 0);
  },
  methods:
  {
    handleUserAvatarClick() {
      if (this.currentUser.role === 'guest') {
        this.goToLogin();
      } else {
        this.goToUserProfile();
      }
    },
    goToCommunity() {
      this.$router.push('/community');
    },
    goToCatalog() {
      this.$router.push('/catalog');
    },
    goToLogin() {
      this.$router.push('/login');
    },
    goToUserProfile() {
      this.$router.push('/userprofile');;
    },
    GoToFavorites() {

    }, //切换到收藏页面
    GoToHome() {
      this.$router.push('/home');
    },
    GoToSubmissions() {

    },//切换到贡献页面
    goToAdminPanel() {
      this.$router.push('/adminpanel');
    },
    goHome() {
      this.$router.push('/home');
    },//返回主页界面
    GoToBook() {
      this.$router.push('/search');
    }, //切换到百科，待实现-----
    GoToCultivation() {

    }, //切换到养护，待实现-----
    GoToRecognition() {
      this.$router.push('/recognition');
    },//切换到识别工具，待实现------
  }
};
</script>




<style>
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
.user-interface {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 80%;
  height: 90%;
  background-image: url('../catalog/images/background.png');
  /* 背景图片路径 */
  background-size: cover;
  background-position: center;
  z-index: 2;
  /* 层数要大于视频 */
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  color: white;
  /* 设置文本颜色，以便在深色背景上可见 */

  border: 5px solid rgb(28, 127, 13);
  /* 绿色边框 */
  box-sizing: border-box;
  /* 确保边框包含在元素的宽度和高度内 */
  border-radius: 10px;
  /* 添加圆角 */
  opacity: 0.9;
}


/* 竖条图片格式*/

.side-bar {
  position: absolute;
  top: 20%;
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
  background-color: rgb(46, 131, 58);
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

.tabs button:hover {
  background-color: rgba(38, 208, 234, 0.289);
  /* 鼠标悬停效果 */
  color: #11c3e7;
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
  left: 0;
  /* 从左侧开始对齐 */
  z-index: 4;
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

/* 鼠标悬停效果 */
.user-button:hover {
  transform: scale(1.1);
  /* 放大按钮 */
}

/* 卡片按钮格式 */
.card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 4vw;
  cursor: pointer;
}

.card {
  position: relative;
  width: 15vw;
  height: 15vw;
  overflow: hidden;
  border-radius: 1vw;
  box-shadow: 0 0.4vw 0.8vw rgba(0, 0, 0, 0.2);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.card img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card-info {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgb(28, 127, 13);
  color: white;
  font-size: 1.5rem;
  padding: 1vh;
  text-align: center;
  transform: translateY(100%);
  transition: transform 0.3s ease;
}

.card:hover {
  transform: scale(1.05);
  box-shadow: 0 0.6vw 1.2vw rgba(0, 0, 0, 0.3);
}

.card:hover .card-info {
  transform: translateY(0);
}

/*  固有写法，显示用户*/
.user-avatar-wrapper {
  position: relative;
  display: inline-block;
}

.user-info-list {
  position: absolute;
  top: 50px;
  left: -125px;
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

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  background-color: #46b476cc;
  color: white;
  z-index: 3;
  position: relative;
}

.logo {
  font-family: 'Caveat-VariableFont', 'ZhiMangXing-Regular', sans-serif;
  font-size: 28px;
  font-weight: bold;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
}

.user-info img {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
  transition: transform 0.3s ease;
}

.user-info img:hover {
  transform: scale(1.1);

}

.username {
  font-family: 'Caveat-VariableFont', 'ZhiMangXing-Regular', sans-serif;
  font-size: 28px;
  font-weight: bold;
}


/*  固有写法，显示用户*/

.footer {
  display: flex;
  justify-content: space-between;
  /* 左中右均匀分布 */
  align-items: center;
  /* 垂直居中对齐 */
  background-color: rgba(70, 180, 118, 0.8);
  color: white;
  position: relative;
  width: 100%;
  /* 跨满页面 */
  z-index: 10;
}

.footer .left {
  text-align: left;
  margin-left: 10px;
  /* 左边距 */
}

.footer .center {
  text-align: center;
  flex: 1;
  /* 中间内容居中，并占据剩余空间 */
}

.footer .right {
  text-align: right;
  margin-right: 10px;
  /* 右边距 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  color: rgb(24, 212, 209);
  /* 悬停下划线效果 */
}
</style>
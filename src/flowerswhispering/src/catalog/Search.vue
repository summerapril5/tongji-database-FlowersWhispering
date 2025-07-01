<template>

  <!--大标题-->

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
  <div class="search-interface">
    <!-- 竖条图片容器 -->
    <div class="side-bar">
      <div class="user-info">
        <p class="user-name">{{ userName }}</p> <!--显示用户名-->

        <div class="tabs"> <!--显示选项卡-->
          <button :class="{ active: activeTab === 'search' }" @click="setActiveTab('search')">百科搜索</button>
          <button :class="{ active: activeTab === 'Myfavorited' }" @click="setActiveTab('Myfavorited')">我的收藏</button>
          <button :class="{ active: activeTab === 'contribution' }" @click="setActiveTab('contribution')">贡献词条</button>
          <button :class="{ active: activeTab === 'new' }" @click="setActiveTab('new')">最新品种</button>

          <button @click="TestPage">页面测试</button> <!--仅用于测试页面布局，后端接成功后，得删除-->
          <button @click="ErrorPage">错误测试</button> <!--仅用于测试页面布局，后端接成功后，得删除-->

          <button :class="{ active: activeTab === 'home' }" @click="GoToHome">返回上步</button>

        </div>
      </div>
    </div>

    <div class="top-banner">
      <!--
            <button class="user-button" @click="Gotouserpage">  
                <img :src="current" alt="User" />   
            </button>
            -->
    </div>


    <!--搜索引擎部分-->
    <div v-if="activeTab === 'search'">
      <div class="search-logo"> <!--搜索引擎logo-->
      </div>

      <div class="search-box"> <!--搜索框，重要部分！！！-->
        <input type="text" v-model="searchQuery" placeholder="输入植物名称，去一探究竟吧！" class="search-input" />
        <button @click="searchDatabase" class="search-button">搜索</button>
      </div>
    </div>
    <!--搜索引擎部分-->

    <!--我的收藏部分-->
    <div v-if="activeTab === 'Myfavorited'" class="favorite-plants-container">
      <h2 class="favorite-title">我的收藏</h2>
      <ul class="favorite-plants-list">
        <li v-for="(item, index) in paginatedFavoritePlants" :key="index" class="favorite-plant-item">
          <span @click="viewPlantDetail(item.Plantid, item.Plantname)" class="plant-name">{{ item.Plantname }}</span>
          <button @click="removeFromFavorites(item.id)" class="remove-button">取消收藏</button>
        </li>
      </ul>
      <div class="pagination">
        <button @click="prevPage" :disabled="currentPage === 1">上一页</button>
        <span>页码 {{ currentPage }} / {{ totalPages }}</span>
        <button @click="nextPage" :disabled="currentPage === totalPages">下一页</button>
      </div>
    </div>
    <!--我的收藏部分-->


    <!--植物贡献-->
    <div v-if="activeTab === 'contribution'" class="favorite-plants-container">
      <h2 class="search-contribution-title">贡献词条</h2>
      <ul class="search-contribution-list">
        <li v-for="(item, index) in paginatedContributions" :key="index" class="search-contribution-item">
          <span class="search-contribution-plant-name"
            @click="item.status === '已通过' ? viewPlantDetail(item.Plantid, item.Plantname) : null"
            :class="{ clickable: item.status === '已通过' }">
            {{ item.Plantname }}</span>
          <span class="review-status"
            :class="{ 'status-pending': item.status === '未审核', 'status-approved': item.status === '已通过', 'status-rejected': item.status === '未通过' }">
            {{ item.status }}
          </span>
        </li>
      </ul>
      <div class="pagination">
        <button @click="prevContributionPage" :disabled="contributionPage === 1">上一页</button>
        <span>页码 {{ contributionPage }} / {{ contributionTotalPages }}</span>
        <button @click="nextContributionPage" :disabled="contributionPage === contributionTotalPages">下一页</button>
      </div>
      <div class="search-contribution-button-container">
        <button class="search-contribution-button" @click="GoToContributionPage">我要贡献</button>
      </div>
    </div>


    <!--最新品种部分-->
    <div v-if="activeTab === 'new'" class="new-plants-container">
      <h2 class="new-plants-title">最新品种</h2>
      <ul class="new-plants-list">
        <li v-for="(item, index) in paginatedNewPlants" :key="index" class="new-plant-item">
          <div class="new-plant-header">
            <span @click="viewPlantDetail(item.Plantid, item.Plantname)" class="new-plant-name">{{ item.Plantname
              }}</span>
            <span class="update-time">{{ item.updateTime }}</span>
          </div>
        </li>
      </ul>
      <div class="pagination">
        <button @click="prevNewPlantsPage" :disabled="newPlantsPage === 1">上一页</button>
        <span>页码 {{ newPlantsPage }} / {{ newPlantsTotalPages }}</span>
        <button @click="nextNewPlantsPage" :disabled="contributionPage === newPlantsTotalPages">下一页</button>
      </div>
    </div>
    <!--最新品种部分-->

  </div>
  <Footer />
</template>

<script>
import { useRouter } from 'vue-router';
import { ref, onMounted } from 'vue';
import { mapState, mapGetters, mapActions } from 'vuex';
import Header from '@/home/Header.vue';
import Footer from '@/home/Footer.vue';
export default {
  name: "Search",
  components: {
    Header,
    Footer,
  },
  data() {
    return {
      buttonImageUrl: '../catalog/images/user_example.png',  // 默认图片，后端接入用户头像
      userName: 'Wuhuairline',// 默认用户名,后端接入用户姓名
      searchQuery: '',   //对应输入框的内容，重要部分！！！
      activeTab: 'search',  //选项卡选项

      favoritePlants: [], //存储用户收藏的植物
      currentPage: 1, // 收藏页面当前页数
      itemsPerPage: 10, // 每页显示的收藏植物数量

      contributions: [], //存储用户贡献的植物
      contributionPage: 1, //贡献页面当前页数
      contributionItemsPerPage: 8, //贡献页面每页数量

      newPlants: [], // 存储最新品种的植物
      newPlantsPage: 1, // 最新品种页面当前页数
      newPlantsItemsPerPage: 8, // 最新品种页面每页数量

    };
  },
  mounted() {
    window.scrollTo(0, 0);
    this.loadFavorites();
    this.loadContributions();
    this.loadNewPlants();
  },

  computed: {
    ...mapState({
      currentUser: state => state.currentUser, // 从Vuex store中获取 currentUser
      userAvatar: state => state.userAvatar // 确保这里绑定了全局的 userAvatar
    }),
    ...mapGetters(['userAvatar']),  // 使用全局的userAvatar
    totalPages() {
      return Math.ceil(this.favoritePlants.length / this.itemsPerPage);
    },
    paginatedFavoritePlants() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.favoritePlants.slice(start, end);
    }, //收藏页面计算

    contributionTotalPages() {
      return Math.ceil(this.contributions.length / this.contributionItemsPerPage);
    },

    paginatedContributions() {
      const start = (this.contributionPage - 1) * this.contributionItemsPerPage;
      const end = start + this.contributionItemsPerPage;
      return this.contributions.slice(start, end);
    },//贡献页面计算

    newPlantsTotalPages() {
      return Math.ceil(this.newPlants.length / this.newPlantsItemsPerPage);
    },

    paginatedNewPlants() {
      const start = (this.newPlantsPage - 1) * this.newPlantsItemsPerPage;
      const end = start + this.newPlantsItemsPerPage;
      return this.newPlants.slice(start, end);
    },//最新品种页面计算
  },  //页面计算功能
  methods:
  {
    toggleUserMenu() {
      this.showUserMenu = !this.showUserMenu;
    },
    handleUserAvatarClick() {
      if (this.currentUser.role === 'guest') {
        this.$router.push('/login'); // 如果是guest用户，点击跳转到登录页面
      } else {
        this.goToUserProfile(); // 否则跳转到个人主页
      }
    },

    goToUserProfile() {
      this.$router.push('/userprofile');
    }, //切换用户页面
    goToCommunity() {
      this.$router.push('/community');
    },
    goToCatalog() {
      this.$router.push('/catalog');
    },
    goHome() {
      this.$router.push('/home');
    },
    goToAdminPanel() {
      this.$router.push('/adminpanel');
    },
    GoToHome() {
      this.$router.push('/catalog');
    },//返回主页界面

    GoToContributionPage() {
      this.$router.push('/Contribution');
    },//切换贡献页面

    searchDatabase() {
      console.log(`Searching for: ${this.searchQuery}`);
    }, //从搜索框搜索的后端逻辑，重要部分！！！, 从数据库遍历名字与 searchQuery 配对

    setActiveTab(tabName) {
      this.activeTab = tabName;
    },  //设置选项卡

    loadFavorites() {
      this.favoritePlants = [
        { id: 1, Plantname: '蒲公英' },
        { id: 2, Plantname: '玫瑰' },
        { id: 3, Plantname: '百合' },
        { id: 4, Plantname: '竹子' },
        { id: 5, Plantname: '兰花' },
        { id: 6, Plantname: '茉莉花' },
        { id: 7, Plantname: '绿萝' }, // 示例数据
        { id: 8, Plantname: '牡丹' },
        { id: 9, Plantname: '柳树' },
        { id: 10, Plantname: '向日葵' },
        { id: 10, Plantname: '豌豆射手' },
        // 示例数据
      ];

    },  //初始渲染时，读取用户的原有的收藏列表，等待后端接入


    loadContributions() {
      this.contributions = [
        { id: 1, Plantname: '铁线莲', status: '未审核' },
        { id: 2, Plantname: '杜鹃花', status: '已通过' },
        { id: 3, Plantname: '水仙花', status: '未通过' },
        { id: 4, Plantname: '菊花', status: '未通过' },
        { id: 5, Plantname: '紫罗兰', status: '已通过' },
        { id: 6, Plantname: '凤梨', status: '未通过' },
        { id: 7, Plantname: '地瓜', status: '未审核' },
        { id: 8, Plantname: '土豆', status: '未审核' },
        { id: 9, Plantname: '香蕉', status: '未审核' },
        { id: 10, Plantname: '杨桃', status: '已通过' },
        // 更多示例数据...
      ];
    },  //初始渲染时，读取用户的原有的贡献列表，等待后端接入

    loadNewPlants() {
      this.newPlants = [
        { id: 1, Plantname: '铁线莲', updateTime: '2024年9月1日' },
        { id: 2, Plantname: '杜鹃花', updateTime: '2024年8月30日' },
        { id: 3, Plantname: '水仙花', updateTime: '2024年8月25日' },
        { id: 4, Plantname: '菊花', updateTime: '2024年8月20日' },
        { id: 5, Plantname: '紫罗兰', updateTime: '2024年8月15日' },
        { id: 6, Plantname: '凤梨', updateTime: '2024年8月10日' },
        { id: 7, Plantname: '地瓜', updateTime: '2024年8月5日' },
        { id: 8, Plantname: '土豆', updateTime: '2024年8月1日' },
        { id: 9, Plantname: '香蕉', updateTime: '2024年7月28日' },
        { id: 10, Plantname: '杨桃', updateTime: '2024年7月20日' },
        // 更多示例数据...
      ];
    }, // 初始渲染时，读取最新品种的植物列表，等待后端接入


    removeFromFavorites(plantId) {
      const plantToRemove = this.favoritePlants.find(plant => plant.id === plantId); //定位到名字，用于后端
      this.favoritePlants = this.favoritePlants.filter(plant => plant.id !== plantId);
      console.log(`删除植物的名字: ${plantToRemove.Plantname}`);
    },  //取消收藏，等待后端接入


    TestPage() {
      this.$router.push('/information');
    }, //跳转植物信息,后端接入测试完毕后，得删除

    ErrorPage() {
      this.$router.push('/FindError');
    },

    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    }, //上一页跳转

    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    }, //下一页跳转

    prevContributionPage() {
      if (this.contributionPage > 1) {
        this.contributionPage--;
      }
    },
    nextContributionPage() {
      if (this.contributionPage < this.contributionTotalPages) {
        this.contributionPage++;
      }
    },

    prevNewPlantsPage() {
      if (this.newPlantsPage > 1) {
        this.newPlantsPage--;
      }
    },

    nextNewPlantsPage() {
      if (this.newPlantsPage < this.newPlantsTotalPages) {
        this.newPlantsPage++;
      }
    },


    viewPlantDetail(plantId, plantName) {
      this.$router.push(`/plant/${plantId}`);
      console.log(`前往植物的名字: ${plantName}`);
    },     // 页面跳转逻辑，等待后端实现,根据植物的名字跳转
  }


};
</script>




<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  background-color: rgba(70, 180, 118, 0.8);
  /* 使用rgba设置透明度，0.8表示80%的不透明度 */
  color: white;
  z-index: 10;
  /* 提高 z-index，确保 header 在其他内容上层 */
  position: relative;
}

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
.search-interface {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 80%;
  height: 90%;
  background-color: rgb(196, 252, 203);
  background-size: cover;
  background-position: center;
  z-index: 2;
  /* 层数要大于视频 */
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  color: rgb(255, 255, 255);
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
  z-index: 20;
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

.tabs button.active {
  background-color: rgba(38, 208, 234, 0.6);
  color: #1131e7;
  /* 选中状态的文本颜色 */
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



/* 搜索引擎LOGO */
.search-logo {
  position: absolute;
  top: 5%;
  left: 42%;
  background-image: url(../catalog/images/search_logo.png);
  z-index: 5;
  background-size: cover;
  background-position: center;
  width: 300px;
  height: 300px;
}

/* 搜索框 */
.search-box {
  position: absolute;
  left: 20%;
  top: 45%;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 5;
}

.search-input {
  width: 800px;
  padding: 10px;
  border: 2px solid rgb(28, 127, 13);
  border-radius: 5px;
  outline: none;
  transition: border-color 0.3s ease;
  margin-right: 10px;
  font-size: 20px;
}

.search-input:focus {
  border-color: rgb(46, 131, 58);
}

.search-button {
  padding: 10px 20px;
  background-color: rgb(46, 131, 58);
  border: none;
  border-radius: 5px;
  color: white;
  font-size: 20px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.search-button:hover {
  background-color: rgb(28, 127, 13);
  transform: scale(1.05);
}

/* 针对不同屏幕尺寸的调整 */
@media (max-width: 1200px) {
  .search-logo {
    left: 40%;
    width: 250px;
    height: 250px;
  }

  .search-box {
    left: 15%;
  }

  .search-input {
    width: 600px;
    font-size: 18px;
  }

  .search-button {
    font-size: 18px;
  }
}

@media (max-width: 768px) {
  .search-logo {
    left: 35%;
    width: 200px;
    height: 200px;
  }

  .search-box {
    left: 10%;
  }

  .search-input {
    width: 400px;
    font-size: 16px;
  }

  .search-button {
    font-size: 16px;
  }
}

@media (max-width: 480px) {
  .search-logo {
    left: 30%;
    width: 150px;
    height: 150px;
  }

  .search-box {
    left: 5%;
  }

  .search-input {
    width: 300px;
    font-size: 14px;
  }

  .search-button {
    font-size: 14px;
  }
}


/* 我的收藏 */
.favorite-plants-container {
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

.favorite-title {
  position: relative;
  top: 0;
  font-size: 35PX;
  z-index: 6;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}

.favorite-plants-list {
  padding: 0;
  margin: 0;
}

.favorite-plant-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 0;
  transition: transform 0.3s ease;
  /* 点击动态效果 */
  cursor: pointer;
  /* 鼠标放在上面改变指针 */
  background-color: #d2f0ed;
  margin: 8px;
  border-radius: 5px;
}


.favorite-plant-item:hover {
  transform: scale(1.05);
  /* 放大效果 */
}

.plant-name {
  font-size: 22px !important;
  /* 强制修改大标题字体大小 */
  transition: color 0.3s ease;
  /* 动态颜色变化效果 */
}

.plant-name:hover {
  color: rgb(37, 210, 226);
  /* 悬停时的文字颜色 */
}

.remove-button {
  background-color: #e74c3c;
  border: none;
  border-radius: 5px;
  color: white;
  padding: 5px 10px;
  cursor: pointer;
  transition: transform 0.3s ease;
  transition: background-color 0.3s ease;
}




.remove-button:hover {
  transform: scale(1.2);
  background-color: #c0392b;
}

/* 分页 */
.pagination {
  margin-top: 10px;
  text-align: center;
}

.pagination button {
  margin-left: 20px;
  margin-right: 20px;
  padding: 5px 10px;
  background-color: rgb(46, 131, 58);
  border: none;
  border-radius: 5px;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.pagination button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}


/* 贡献词条部分样式 */
.search-contribution-container {
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

.search-contribution-title {
  position: relative;
  top: 0;
  font-size: 35PX;
  z-index: 6;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}

.search-contribution-list {
  padding: 0;
  margin: 0;
}

.search-contribution-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 0;
  transition: transform 0.3s ease;
  /* 点击动态效果 */
  cursor: pointer;
  /* 鼠标放在上面改变指针 */
  background-color: #d2f0ed;
  margin: 8px;
  border-radius: 5px;
}

.search-contribution-plant-name {
  font-size: 22px !important;
  transition: color 0.3s ease;
}

.search-contribution-item:hover {
  transform: scale(1.05);
  color: rgb(17, 213, 244);
}

.review-status {
  font-size: 18px;
  padding: 5px 10px;
  border-radius: 5px;
}

.status-pending {
  background-color: #f39c12;
  color: white;
}

.status-approved {
  background-color: #27ae60;
  color: white;
}

.status-rejected {
  background-color: #e74c3c;
  color: white;
}

.search-contribution-button-container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.search-contribution-button {
  padding: 10px 30px;
  font-size: 25px;
  color: white;
  background-color: rgb(43, 247, 230);
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  transition: transform 0.3s ease;
}

.search-contribution-button:hover {
  background-color: rgb(29, 180, 167);
  transform: scale(1.3);
}


/* 最新品种 */
.new-plants-container {
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

.new-plants-title {
  position: relative;
  top: 0;
  font-size: 35px;
  z-index: 6;
  color: rgb(45, 198, 22);
  margin-bottom: 0;
}

.new-plants-list {
  padding: 0;
  margin: 0;
}

.new-plant-item {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 10px;
  transition: transform 0.3s ease;
  /* 点击动态效果 */
  cursor: pointer;
  /* 鼠标放在上面改变指针 */
  background-color: #d2f0ed;
  margin: 8px;
  border-radius: 5px;
}

.new-plant-header {
  display: flex;
  justify-content: space-between;
  width: 100%;
  margin-bottom: 5px;
}

.new-plant-name {
  font-size: 22px !important;
  font-weight: bold;
  transition: color 0.3s ease;
}

.new-plant-item:hover {
  transform: scale(1.05);
  color: rgb(17, 213, 244);
}

.update-time {
  font-size: 18px;
  color: rgb(47, 162, 188);
}

/*  固有写法，显示用户*/
.user-avatar-wrapper {
  position: relative;
  display: inline-block;
  z-index: 10;
  /* 提高头像的层级 */
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



/* 添加媒体查询，根据不同屏幕大小自适应 */
@media (max-width: 1200px) {

  /* 在屏幕宽度小于1200px时，对布局进行微调 */
  .search-interface {
    width: 90%;
    height: 85%;
  }

  .side-bar {
    width: 120px;
  }

  .search-input {
    width: 600px;
  }
}

@media (max-width: 992px) {

  /* 在屏幕宽度小于992px时，调整内容的布局 */
  .search-interface {
    width: 85%;
    height: 80%;
  }

  .search-input {
    width: 500px;
  }

  .favorite-plants-container,
  .search-contribution-container,
  .new-plants-container {
    width: 80%;
  }
}

@media (max-width: 768px) {

  /* 在屏幕宽度小于768px时，对更小屏幕进行适配 */
  .search-interface {
    width: 100%;
    height: 75%;
  }

  .side-bar {
    display: none;
    /* 隐藏侧边栏，使得页面更简洁 */
  }

  .favorite-plants-container,
  .search-contribution-container,
  .new-plants-container {
    width: 90%;
  }

  .search-input {
    width: 400px;
  }

  .search-logo {
    width: 150px;
    height: 150px;
  }
}

@media (max-width: 480px) {

  /* 在屏幕宽度小于480px时，适配手机端 */
  .search-interface {
    width: 95%;
    height: 70%;
  }

  .search-input {
    width: 280px;
  }

  .search-button {
    font-size: 14px;
  }

  .favorite-title,
  .search-contribution-title,
  .new-plants-title {
    font-size: 28px;
  }
}
</style>
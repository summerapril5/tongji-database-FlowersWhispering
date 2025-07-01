<template>

    <!--大标题-->
  
         <!--大标题-->
    
    
    <div class="book-background">
            <!-- 视频容器 -->
        <div id="videoContainer">
            <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
              <source src="../assets/video/background.mp4" type="video/mp4">
            </video>
        </div>
    </div>
 
    <button class ='return-button' @click="GoBack">  <!--返回按钮-->
        <img src="../catalog/images/return_icon.png" alt="返回" />
    </button>
    
    <!--以下为百科页面-->
    <div class ="information-top">
    
    <div class ="information-logo">    <!--搜索引擎logo-->
            </div>
            <p>叶语百科</p>

            <div class ="information-box">    <!--搜索框，重要部分！！！-->
             <input 
              type="text" 
              v-model="searchQuery"   
              placeholder="输入植物名称，去一探究竟吧！" 
              class="information-input" 
             />
            <button @click="searchDatabase" class="information-button">搜索</button>
           </div>
    </div>


    <div class="information-page">
    <!-- 左边导航栏 -->
    <div class="sidebar">
      <h2>目录</h2>
      <ul>
        <li v-for="(title, index) in subTitles" :key="index">
          <a :href="'#subsection-' + index">{{ title }}</a>
        </li>
      </ul>
    </div>

    <!-- 中间内容 -->
    <div class="content">
      <div class="plant-info" v-if="plant">
        <h1 class = 'plant-name'>{{ plant.name }}</h1>     <!--植物名字大标题-->
        
        <!--收藏功能按钮-->
        <button               
        class ="favorite-button"
        :class= "{'favorited' : isFavorited}"
        @click ="GoToFavorite">
        {{isFavorited ? '取消收藏' : '收藏'}}
        </button>
      </div>
      
      <!-- 小标题内容 -->
      <div v-for="(content, index) in plant.contents" :key="index" :id="'subsection-' + index" class="subsection">
        <h2>{{ subTitles[index] }}</h2> <!--分类小标题-->
        <p>{{ content }}</p>   <!--每个小标题内容-->
      </div>
    </div>



    <!--右边植物图片以及分类-->
          <div class="info-sidebar">
          <div class="info-sidebar-image">
            <img :src="plant.image" alt="植物图片" class="plant-image" />  <!--植物图片-->
          </div>
          <div class="info-sidebar-categories">
            <h3>科学分类</h3>
            <div v-for="(item, index) in classificationList" :key="index" class="classification-item">   <!--分类部分，重要-->
            {{ item.value }}
            </div>
          </div>
        </div>


  </div>
</template>


<script>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { ref, onMounted } from 'vue';

export default {
  name: "Information",
  data() {
  return {
    searchQuery: '',  // 对应输入框的内容
    isFavorited:false, //用于跟踪植物是否被收藏
    plant: {
        name: '绿萝',      //植物名字
        image: require('../catalog/images/plant_example.jpg'), // 植物图片位置
        contents: [
          '绿萝（学名：Epipremnum aureum），又名黄金葛，是天南星科、麒麟叶属的一种多年生木质藤本植物[1]，原产于法属波利尼西亚的莫雷阿岛[2]，随着人类引进而在东南亚、南亚、华南、所罗门群岛、非洲、西印度群岛等地的热带及亚热带森林中归化[2][3]，并在部分地区成为入侵物种，影响一些地方的生态平衡。喜荫蔽潮湿的环境，生命力极强，在黑暗无光的条件下亦能生存[4]。现作为观叶植物而被广泛栽培于人类居住和活动场所，因其枝茎善于攀缘的特性，适宜吊盆栽植或植于图腾柱上。',
          '绿萝在热带地区常攀援生长在雨林的岩石和树干上，可长成巨大的藤本植物。绿萝喜高温、高湿、有明亮散射光的环境。耐阴，但过于阴蔽时，叶片上色斑不明显或消失。忌烈日暴晒，生长适温20~30℃，稍耐寒，5℃以上可安全越冬。要求肥沃、疏松、湿润的土壤。耐水湿，可用水插莳养，稍耐旱 [2]。具有较强的耐尘性，对环境要求低',
          '水分绿萝属于观叶类植物，叶片较多且叶面积较大，体内水分蒸腾较快，所以水分供给要充足。夏季高温干旱时，每天傍晚时浇1次水，中午还要向叶面喷水降温，增大盆花周围环境的湿度，以避免叶片在高温下受到伤害。冬季由于温度低，绿萝生长缓慢，其需水量也减少，不可浇水过多 [14]。肥料绿萝施肥以氮肥为主，辅以磷钾肥，要薄肥勤施。春季绿萝生长期到来前，每隔10d左右施硫酸或0.3%尿素溶液1次，并用 0.5%~1%的尿素溶液进行叶面施肥1次，这样既能满足绿萝生长需要，又能保持叶片绿色光亮。秋冬季节应减少施肥，施肥以叶面喷施为主，通过叶面上气孔吸收肥料，肥效可直接作用于叶面。叶面肥要用专用肥，普通无机肥不易被叶面吸收 [15]。养护管理光照绿萝属阴性植物，忌阳光直射，喜散射光，较耐阴，因此要把绿萝摆在室内光照最好的地方，或在正午搬到密封的阳台上晒太阳。光照不足和阳光直射都会使叶面上美丽的斑纹消失，有损其观赏价值 [15]。温度一般家中室温即可，建议室温控制在20℃左右，温度只要不低于10℃，绿萝即可安全过冬 [8]。土壤以疏松、富含有机质的微酸性和中性沙壤土栽培，其发育最好 [15]。修剪为保持绿萝良好的株型，要截短过长的茎蔓和紊乱的枝条，使其长短有致，疏密适当 [11]。一般将离盆土20cm的枝叶剪掉，长出新叶后再将底部的老叶剪除。盆栽绿萝一般都栽有棕柱，当绿萝的茎蔓爬满棕柱且枝条生长至20cm左右时，需要进行修剪 [8]。',
         ]  //植物具体信息
      },    //植物内容，为一个数组，对应关系见下方
      subTitles: [
        '植物简介',
        '生长环境',
        '栽培方法',
      ],  //此处即为8个大分类，需要后端接入在 contents,此处为位置对应关系，！！！！



      classificationList: [
        { label: '类别', value: '一种观赏植物' }, // 分类
      ],  //此处为分类部分,value值要动态改变,先提前设置初始值
  };
},

mounted()
{
   console.log(this.$route.query.outputName);//检测名字能否传输
   this.getPlantData();//获取植物信息
},

methods: {
    //获取植物信息的函数
    async getPlantData() {
      try {
        const response = await axios.get('/api/PlantFind/findFavors'); // 替换为实际的 API 地址
        const data = response.data;

        if (data.message) {
          console.log(data.message); // 处理未找到相关植物的情况
          return;
        }

        // 将数据赋值到组件的变量
        this.plant.name = data.commonName;
        this.classificationList[0].value = data.category;
        this.plant.contents[0] = data.portrayal;
        this.plant.contents[1] = data.growthEnvironment;
        this.plant.contents[2] = data.careConditions;
      } catch (error) {
        console.error("获取植物数据时出错：", error);
      }
    },

    searchDatabase() {
      console.log(`Searching for: ${this.searchQuery}`);
    },

    GoBack() { 
      this.$router.push('/search');
    },

    getFavoriteStatus() {
      // 从后端获取收藏状态，待后端接入
    },

    GoToFavorite() {
      if (this.isFavorited) {
        this.unfavoritePlant();
      } else {
        this.favoritePlant();
      }
      this.isFavorited = !this.isFavorited; // 切换收藏状态
    },

    unfavoritePlant() {
      console.log("进行取消收藏操作");
    },

    favoritePlant() {
      console.log("进行收藏操作");
    },
  },
};
</script>


<style scoped>
.book-background 
{
    display: flex;
   flex-direction: column;
   min-height: 100vh;
   position: relative;
}
  
  /* 设置背景图像 */
  .book-background 
  {
    background-size: cover;
    height: 100vh; /* 使背景覆盖整个视口 */
    display: flex;
    align-items: center;
    justify-content: center;
    z-index:-1;
  }


  /* LOGO和搜索框的容器*/
  .information-top
  {
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 5%;
  left: 50%;
  transform: translateX(-50%);
  z-index: 5;
  background-color: #62e060;
  border-radius: 15px; /* 圆角效果 */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* 阴影效果 */
  font-size: 28PX;
  color:rgb(11, 135, 27);
  font-family: '黑体','ZhiMangXing-Regular', sans-serif;
  opacity:0.9;
  }


  /* 返回按钮*/
  .return-button
  {
   position:absolute;
   top:10%;
   left:3%;
   background: none;
   padding: 0;
   border: none;
   cursor: pointer;
   transition: background-color 0.3s ease, transform 0.3s ease;
  }

  .return-button img 
  {
  width: 80px; /* 按钮图片宽度 */
  height: 80px; /* 按钮图片高度 */
  object-fit: cover;
 }

 .return-button:hover
 {
    transform: scale(1.2); /* 悬停时放大效果 */
 }
  
  /* 搜索引擎LOGO */
  .information-logo
    {
      background-image: url(../catalog/images/search_logo.png);
      background-size: cover;
      background-position: center;
      width:140px;
      height:140px;
      margin-right: 5px;
    }

    /*  搜索框   */

  .information-box 
  {
    display: flex;
    align-items: center;
}

.information-input 
{
 
  width: 1100px;
  padding: 10px;
  border: 2px solid rgb(28, 127, 13); /* 绿色边框 */
  border-radius: 5px;
  outline: none;
  transition: border-color 0.3s ease;
  margin-right: 10px; /* 搜索框与按钮之间的间距 */
  font-size:16px;
}

.information-input:focus {
  border-color: rgb(46, 131, 58); /* 聚焦时的边框颜色 */
}

.information-button {
  padding: 5px 20px;
  background-color: rgb(46, 131, 58); /* 按钮背景色 */
  border: none;
  border-radius: 5px;
  color: white;
  font-size:16px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.information-button:hover {
  background-color: rgb(28, 127, 13); /* 悬停时的按钮背景色 */
  transform: scale(1.05); /* 悬停时放大效果 */
}



/*重要部分，百科页面部分的样式*/

/* 整个页面布局*/
.information-page 
{
  position:absolute;
  left:10%;
  top:22%;
  display: flex;
  justify-content: space-between;
  margin: 20px auto;
  width: 80%;
}


/* 左边目录 */
.sidebar
 {
  font-size:30px;
  border-radius: 5px;
  width: 200px;
  background-image: url('../catalog/images/sidebar.jpg');
  background-attachment: fixed;
  padding: 20px;
  border-right: 2px solid #ddd;
  text-align: center;
}



.sidebar ul {
  list-style-type: none;
  padding: 10px;
}

.sidebar li {
  margin: 20px 0;
}

.sidebar a {
  text-decoration: none;
  color: #214e31;
  font-weight: bold;
}


/* 中间内容 */
.content {
  flex: 1;
  padding: 20px;
  background-color: #aeddb2; 
  font-size:20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  opacity:0.9;
}

.plant-info {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.information.plant-name
{
    font-size: 70px;
    color:rgb(59, 175, 41);
    border-bottom: 3px solid #1ae389; /* 添加下划线 */
    padding-bottom: 10px; /* 与下划线之间的距离 */
    text-align: center;
}

/* 收藏按钮 */
.favorite-button {
  padding: 8px 18px;
  font-size: 16px;
  border: 2px solid #62e060;
  border-radius: 5px;
  background-color: white;
  color: #62e060;
  cursor: pointer;
  transition: background-color 0.3s ease, color 0.3s ease;
  margin-left: 20px; 
}

.favorite-button.favorited {
  background-color: #62e060;
  color: white;
  border-color: #62e060;
}

.favorite-button:hover {
  background-color: #58d357;
  color: white;
}



/* 分类小标题 */ 
.subsection {
  margin-bottom: 20px;
}

.subsection h2 {
  margin: 10px 0;
  font-size: 40px; /* 调整小标题的字体大小 */
  color: #2d6a4f; /* 调整小标题颜色 */
  border-bottom: 3px solid #2d6a4f; /* 添加下划线 */
  padding-bottom: 10px; /* 与下划线之间的距离 */
}

.subsection p {
  line-height: 1.6;
}



/* 右侧图片以及分类信息*/
.info-sidebar {
  width: 180px; /* 宽度 */
  background-image: url('../catalog/images/sidebar.jpg');
  background-position: center;
  background-attachment: fixed; 
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  text-align: center;
}


.plant-image {
  width: 180px;
  height: 300px;
  object-fit: cover;
  border-radius: 10px; /* 圆角效果 */
}

.info-sidebar-categories 
{
  font-size: 20px;
}

.info-sidebar-categories h3 {
  margin-bottom: 10px;
  font-size: 40px; /* 标题字体大小 */
  color: #2d6a4f; /* 标题颜色 */
  text-align: center;
  background-color: rgb(117, 241, 134);
}

.info-sidebar-categories ul {
  list-style-type: none;
  padding: 0;
}

.info-sidebar-categories li {
  margin: 10px 0;
}




/*重要部分，百科页面部分的样式*/





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
    font-family: '宋体','ZhiMangXing-Regular', sans-serif;
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
    z-index: 1;
    position: relative;
  }



  .logo {
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
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
    font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    font-size: 28px;
    font-weight: bold;
  }


  /*  固有写法，显示用户*/ 


</style>

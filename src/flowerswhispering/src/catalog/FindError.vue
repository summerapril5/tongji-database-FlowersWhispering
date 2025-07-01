<template>

<!--大标题-->
<header class="header">
      <div class="logo">Flowers Whispering</div>
      <div class="user-info" v-if="currentUser">
        <span v-if="currentUser" class="username">{{ currentUser.username }}</span>
        <div class="user-avatar-wrapper">
          <img v-if="currentUser" src="../home/images/user-avatar.jpg" alt="User Avatar" @click="goToUserProfile()">
          <!-- 用户详细信息列表 -->
          <div class="user-info-list">
            <p>用户名: {{ currentUser.username }}</p>
            <p>邮箱: {{ currentUser.email }}</p>
            <p>角色: {{ currentUser.role }}</p>
          </div>
        </div>
        <button class="logout-button" @click="performLogout">{{ currentUser.role === 'guest' ? '登录' : '登出' }}</button>
      </div>
    </header>
     <!--大标题-->


    <div class="error-background">
        <!-- 视频容器 -->
    <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../assets/video/background.mp4" type="video/mp4">
        </video>
    </div>
    </div>

      <!-- 错误信息框 -->
      <div class="info-box">
      <img class="info-image" src="../catalog/images/error_background.jpg" alt="Info Image">
      <p class="info-text">:(</p>
      <p class="info-text">你似乎来到没有植物生长的荒原</p>
      <button class = "return-button" @click ="GoBack" >重新探索</button>
    </div>




</template>


<script scoped>
import { useRouter } from 'vue-router';
import { ref, onMounted } from 'vue';

export default {
  name:'FindError',
  
  methods:
  {
    GoBack()
    {
      this.$router.go(-1);
    },
  }
};


</script>

<style scoped>

/* 设置背景图像 */
.error-background 
{
   display: flex;
   flex-direction: column;
   min-height: 100vh;
   position: relative;
}
      

.error-background 
{
   background-size: cover;
   height: 100vh; /* 使背景覆盖整个视口 */
   display: flex;
   align-items: center;
   justify-content: center;
   z-index:-1;
}


/* 错误信息框样式 */
.info-box {
  position: absolute;
  top: 50%;
  left: 50%;
  width:600px;
  height:700px;
  transform: translate(-50%, -50%);
  background-color: rgba(234, 141, 28, 0.8);
  border-radius: 10px;
  padding:5px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  text-align: center;
  z-index: 2;
}

.info-image 
{
  width:600px;
  height:500px;
  border-radius: 10px;
  margin-bottom: 0  px;
}

.info-text {
  font-size: 25px;
  color: #000000;
  font-family: '宋体', 'ZhiMangXing-Regular', sans-serif;
}

.return-button{
  padding: 10px 30px;
  font-size: 25px;
  color: white;
  background-color: rgb(247, 118, 43);
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  transition: transform 0.3s ease;   
}

.return-button:hover {
  background-color: rgb(41, 254, 59);
  transform:scale(1.3);
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
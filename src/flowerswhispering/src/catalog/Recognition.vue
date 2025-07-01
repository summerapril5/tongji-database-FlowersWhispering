<template>
  <div class="contribution-background">
    <!-- 视频容器 -->
    <div id="videoContainer">
      <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
        <source src="../assets/video/background.mp4" type="video/mp4">
      </video>
    </div>
  </div>

   <!-- 内容表单 -->
   <div class="recognition-content-form">
      <!-- 大标题 -->
      <h1 class="recognition-title">植物识图</h1>

      <!-- 图片上传工具 -->
      <div class="recognition-image-upload">
        <input type="file" @change="onFileChange" accept="image/*" />
      </div>


      <!-- 提交和返回按钮 -->
      <div class="recognition-buttons">
        <button @click="recognize" class="recognition-recognize-button">识别</button>
        <button @click="goBack" class="recognition-return-button">退出</button>
      </div>
    </div>
</template>

<script>
import { ref } from 'vue';

export default {
  name: 'Recognition',
  data() {
    return {
      selectedFile: null, // 用于存储用户选择的文件
      outputName: '萝卜', // 用于显示后端输出名字
    };
  },
  methods: {
    onFileChange(e) {
      this.selectedFile = e.target.files[0];
    },

    async recognize() {
      if (!this.selectedFile) {
        alert('请先上传一张图片');
        return;
      }


      this.$router.push({ 
    path: '/information', 
    query: { outputName: this.outputName }
     });  //重要函数，将植物名字传到路由里面
    },


    goBack() {
      this.$router.go(-1);
    },
  },
};
</script>

<style scoped>
/* 背景视频样式 */
.contribution-background {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  position: relative;
  background-size: cover;
  height: 100vh; /* 使背景覆盖整个视口 */
  align-items: center;
  justify-content: center;
  z-index: -1;
}

/* 内容表单样式 */
.recognition-content-form {
  background-image: url("../catalog/images/contribution-background.jpg");
  background-repeat: no-repeat; /* 防止图片重复 */
  background-position: center; /* 图片居中对齐 */
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  max-width: 900px; /* 根据背景图片的宽度设置最大宽度 */
  height: 600px; /* 根据背景图片的高度设置高度 */
  width: 100%; /* 宽度为100% */
  z-index: 1;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%); /* 使元素在视口中居中对齐 */
  text-align: center;
}

/* 大标题样式 */
.recognition-title {
  font-size: 40px;
  color: rgb(17, 156, 124);
  margin-bottom: 20px;
}

/* 图片上传工具区域 */
.recognition-image-upload {
  position: relative;
  left: 31%;
  margin: 20px 0;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 2px dashed #22a324;
  padding: 20px;
  width: 300px;
  height: 300px;
  text-align: center;
  border-radius: 5px;
  background-color: rgba(255, 255, 255, 0.5);
}

/* 输出框样式 */
.recognition-output-box {
  margin: 20px 0;
  padding: 10px;
  position:relative;
  left:26%;
  max-width: 400px;
  width: 400px;
  border: 2px solid #22a324;
  border-radius: 5px;
  background-color: rgba(255, 255, 255, 0.5);
}

.output-content {
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 24px;
}

.label {
  font-weight: bold;
  margin-right: 10px;
}

.name {
  font-size: 24px;
  color: #22a324;
}

/* 按钮样式 */
.recognition-buttons {
  margin-top: 20px;
}

.recognition-recognize-button {
  background-color: #46b476;
  color: white;
  font-size: 30px;
  border: none;
  border-radius: 5px;
  padding: 10px 20px;
  cursor: pointer;
  margin-right: 100px;
}

.recognition-return-button {
  background-color: #39bbcf;
  color: white;
  font-size: 30px;
  border: none;
  border-radius: 5px;
  padding: 10px 20px;
  cursor: pointer;
}

.recognition-recognize-button:hover {
  background-color: #3a8e63;
}

.recognition-return-button:hover {
  background-color: #3292e6;
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
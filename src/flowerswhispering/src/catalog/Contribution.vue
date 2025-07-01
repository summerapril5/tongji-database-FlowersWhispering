<template>

    <!--大标题-->
    
         <!--大标题-->
    
    
        <div class="contribution-background">
            <!-- 视频容器 -->
        <div id="videoContainer">
            <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
              <source src="../assets/video/background.mp4" type="video/mp4">
            </video>
        </div>
        </div>

  <!-- 内容表单 -->
  <div class="content-form">
    <!-- 植物基本信息输入框 -->
    <div class="basic-info">
      <h2 class="section-title">基本词条</h2>
      <div class="basic-info-inputs">
        <input
          type="text"
          class="plant-name-input"
          v-model="plantData.name"
          placeholder="植物名字"
          required
        />
        <input
          type="text"
          v-model="plantData.kingdom"
          placeholder="界"
          required
        />
        <input
          type="text"
          v-model="plantData.phylum"
          placeholder="门"
          required
        />
        <input
          type="text"
          v-model="plantData.class"
          placeholder="纲"
          required
        />
        <input
          type="text"
          v-model="plantData.order"
          placeholder="目"
          required
        />
        <input
          type="text"
          v-model="plantData.family"
          placeholder="科"
          required
        />
        <input
          type="text"
          v-model="plantData.genus"
          placeholder="属"
          required
        />
        <input
          type="text"
          v-model="plantData.species"
          placeholder="种"
          required
        />
      </div>
      <!-- 图片上传工具 -->
      <div class="image-upload">
        <input type="file" @change="onFileChange" accept="image/*" />
      </div>
    </div>

    <!-- 植物详细信息输入框 -->
    <div class="detailed-info">
      <h2 class="section-title">详细词条</h2>
      <textarea
        v-model="plantData.description"
        placeholder="植物简介"
        required
      ></textarea>
      <textarea
        v-model="plantData.features"
        placeholder="形态特征"
        required
      ></textarea>
      <textarea
        v-model="plantData.distribution"
        placeholder="分布范围"
        required
      ></textarea>
      <textarea
        v-model="plantData.environment"
        placeholder="生长环境"
        required
      ></textarea>
      <textarea
        v-model="plantData.habits"
        placeholder="生长习性"
        required
      ></textarea>
      <textarea
        v-model="plantData.reproduction"
        placeholder="繁殖方式"
        required
      ></textarea>
      <textarea
        v-model="plantData.cultivation"
        placeholder="栽培技术"
        required
      ></textarea>
      <textarea
        v-model="plantData.value"
        placeholder="主要价值"
        required
      ></textarea>
    </div>

    <!-- 提交按钮 -->
    <button @click="submitForm" class="contribution-button">发布贡献</button>
    <button @click="goBack" class="return-button">放弃编辑</button>
  </div>
</template>
    
    
<script>
import { useRouter } from 'vue-router';
import { ref, onMounted } from 'vue';
    
export default {
  name: 'Contribution',
  data() {
    return {
      currentUser: null,
      plantData: {
        name: '',
        kingdom: '',
        phylum: '',
        class: '',
        order: '',
        family: '',
        genus: '',
        species: '',
        description: '',
        features: '',
        distribution: '',
        environment: '',
        habits: '',
        reproduction: '',
        cultivation: '',
        value: '',
        image: null,
      }, //后端要用到的数据在这里
    };
  },
  methods: {
    //返回上一步
    goBack() {
      this.$router.go(-1);
    },


    //图片处理函数，也要放入后端相应位置。
    onFileChange(e) {
      this.plantData.image = e.target.files[0];
    },
    




    //重要提交函数！！！请后端同学将读取的formData,导入对应表中，并完成页面返回，以下部位
    async submitForm() {
      if (Object.values(this.plantData).some((field) => !field)) {
        alert('所有字段均为必填项！');
        return;
       } //别删除！！！，这个用于前端检验是否全部填写。

      try {
        const formData = new FormData();
        for (const key in this.plantData) {
          formData.append(key, this.plantData[key]);
        }//将数据导入到formData中，请传入数据库中



            //调试部分，看数据是否成功接入，后端成功接入后可删除


            
          // 输出每个输入框中的信息到控制台
      console.log('植物名字:', this.plantData.name);
      console.log('界:', this.plantData.kingdom);
      console.log('门:', this.plantData.phylum);
      console.log('纲:', this.plantData.class);
      console.log('目:', this.plantData.order);
      console.log('科:', this.plantData.family);
      console.log('属:', this.plantData.genus);
      console.log('种:', this.plantData.species);
      console.log('植物简介:', this.plantData.description);
      console.log('形态特征:', this.plantData.features);
      console.log('分布范围:', this.plantData.distribution);
      console.log('生长环境:', this.plantData.environment);
      console.log('生长习性:', this.plantData.habits);
      console.log('繁殖方式:', this.plantData.reproduction);
      console.log('栽培技术:', this.plantData.cultivation);
      console.log('主要价值:', this.plantData.value);




        // 以下为前后端对接部分，可以修改
        const response = await fetch('/api/submitPlant', {
          method: 'POST',
          body: formData,
        });

        if (!response.ok) {
          throw new Error('提交失败');
        }

        alert('提交成功！');
        this.$router.push('/success'); // 提交成功后跳转到其他页面
      } catch (error) {
        console.error('提交失败:', error);
        alert('提交失败，请稍后再试');
      }
    },
  },
};
</script>


<style scoped>
    
    .contribution-background {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  position: relative;
  background-size: cover;
  height: 100vh; /* 使背景覆盖整个视口 */
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: -1;
}

/* 填表总框架 */
.content-form {
  background-image: url("../catalog/images/contribution-background.jpg");
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  max-width: 900px;
  width: 100%;
  z-index: 1;
  position: absolute;
  top: 10%;
  left: 20%;
}

/* 标题样式 */
.section-title {
  text-align: center;
  font-size: 50px;
  font-weight: bold;
  margin-bottom: 10px;
}

/* 基本词条 */
.basic-info 
{
  background-color: rgba(119, 185, 71, 0.555);
  display: flex;
  justify-content: space-between;
  border: 2px solid #46b476c7;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
}

/* 基本信息输入框区域 */
.basic-info-inputs {
  display: flex;
  flex-direction: column;
  width: 65%;
}

/* 植物名字输入框 */
.plant-name-input {
  font-weight: bold;
  font-size: 18px;
  border: 2px solid #22a324;
  padding: 10px;
  margin-bottom: 10px;
}

/* 其他基本信息输入框 */
.basic-info-inputs input {
  padding: 8px;
  margin: 5px 0;
  border: 1px solid #ccc;
  border-radius: 5px;
}

/* 图片上传工具区域 */
.image-upload {
  margin-left: 20px;
  width: 30%;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 2px dashed #22a324;
  padding: 20px;
  text-align: center;
  border-radius: 5px;
  background-color: rgba(255, 255, 255, 0.5);
}

/* 详细词条 */
.detailed-info 
{
    background-color: rgba(119, 185, 71, 0.555);
  border: 2px solid #46b476;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
}

/* 详细信息输入框 */
.detailed-info textarea 
{
  width: 100%;
  height: 80px;
  padding: 5px;
  margin: 10px -10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  opacity: 0.8;
  font-size: 20px;
}

/* 提交和返回按钮 */
.contribution-button
{
  background-color: #46b476;
  color: white;
  font-size: 25px;
  border: none;
  border-radius: 5px;
  padding: 10px 20px;
  cursor: pointer;
  margin-left: 200px;
  margin-right: 200px;
  transition: background-color 0.3s ease;
  transition: transform 0.3s ease;
}

.contribution-button:hover
{
    background-color: #3a8e63;
    transform:scale(1.2);
}

.return-button {
  background-color: #39bbcf;
  color: white;
  font-size: 25px;
  border: none;
  border-radius: 5px;
  padding: 10px 30px;
  cursor: pointer;
  margin-right: 10px;
  transition: background-color 0.3s ease;
  transition: transform 0.3s ease;
}
.return-button:hover {
  background-color: #3292e6;
  transform:scale(1.2);
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
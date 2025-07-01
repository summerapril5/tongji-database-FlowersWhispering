<template>
  <footer class="footer">
    <p class="left"><a href="javascript:void(0)" @click="openContactModal">联系我们</a></p>
  <div class="center">
    <span>© 2024 Flowers Whispering&nbsp;&nbsp;&nbsp;&nbsp;</span>
    <a href="https://beian.miit.gov.cn/" target="_blank">豫ICP备2024087175号-1</a>
  </div>
  </footer>

  <!-- 联系我们弹窗 -->
  <div v-if="isContactModalVisible" class="modal-overlay" @click="closeContactModal">
    <div class="modal-content" @click.stop>
      <h3>联系我们</h3>
      <form @submit.prevent="submitContactForm"> <!-- 表单提交 -->
        <textarea id="message" name="message" rows="6" cols="50" v-model="contactForm.message" placeholder="请在这里输入您的反馈..." required></textarea><br><br>
        
        <div class="modal-buttons">
          <button type="submit">提交</button>
          <button type="button" @click="closeContactModal">关闭</button>
        </div>
      </form>
    </div>
  </div>


</template>

<script>
export default {
  name: 'Footer',
   data() {
    return {
      isContactModalVisible: false, // 控制弹窗显示
      contactForm: {
        message: ''
      }
    };
  },
  methods: {
    openContactModal() {
      this.isContactModalVisible = true; // 打开弹窗
    },
    closeContactModal() {
      this.isContactModalVisible = false; // 关闭弹窗
      this.clearForm(); // 关闭时清空表单
    },
    async submitContactForm() {
      // 表单提交逻辑
      console.log('提交的表单数据:', this.contactForm);
      
      // 将表单数据发送给后端的admin账户
      try {
        const response = await axios.post('/api/admin/contact', {
          name: this.contactForm.name,
          email: this.contactForm.email,
          message: this.contactForm.message
        });
        console.log('提交成功:', response.data);
        alert('提交成功！');
      } catch (error) {
        console.error('提交失败:', error);
        alert('提交失败，请稍后再试。');
      }
      
      // 清空表单并关闭弹窗
      this.clearForm();
      this.closeContactModal();
    },
    clearForm() {
      // 清空留言内容
      this.contactForm.message = '';
    }
  },
};
</script>

<style scoped>
.footer {
  display: flex;
  justify-content: space-between;  /* 左中右均匀分布 */
  align-items: center;             /* 垂直居中对齐 */
  background-color: rgba(70, 180, 118, 0.8);
  color: white;
  position: relative;
  bottom: 0;
  width: 100%;                     /* 跨满页面 */
  z-index: 10;
}

.footer .left {
  text-align: left;
  margin-left: 10px;               /* 左边距 */
}

.footer .center {
  text-align: center;
  flex: 1;                         /* 中间内容居中，并占据剩余空间 */
}

.footer .right {
  text-align: right;
  margin-right: 10px;              /* 右边距 */
}

.footer a {
  color: white;
  text-decoration: none;
}

.footer a:hover {
  color: rgb(24, 212, 209); /* 悬停下划线效果 */
}
/* 弹窗的背景覆盖层 */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* 半透明黑色背景 */
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000; /* 确保覆盖在其他内容之上 */
}

/* 弹窗内容 */
.modal-content {
  background-color: white;
  padding: 20px;
  border-radius: 12px; /* 更大一点的圆角 */
  width: 400px;
  text-align: center;
  box-shadow: 0px 6px 15px rgba(0, 0, 0, 0.2); /* 添加深度阴影效果 */
  z-index: 1001;
}

/* 留言框样式 */
textarea {
  width: 94%;
  height: 150px;
  padding: 12px;
  border: 2px solid #007bff;
  border-radius: 8px;
  font-size: 16px;
  resize: none; /* 禁止调整大小 */
  font-family: 'Arial', sans-serif;
  box-shadow: 0px 2px 8px rgba(0, 0, 0, 0.1);
}

textarea:focus {
  outline: none;
  border-color: #0056b3; /* 聚焦时边框颜色变化 */
  box-shadow: 0px 4px 12px rgba(0, 91, 187, 0.3); /* 聚焦时增加阴影 */
}

/* 按钮行样式 */
.modal-buttons {
  display: flex;
  justify-content: space-between; /* 两个按钮分开显示 */
  margin-top: 20px;
}

/* 按钮样式 */
button {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

button:hover {
  background-color: #0056b3;
}
</style>

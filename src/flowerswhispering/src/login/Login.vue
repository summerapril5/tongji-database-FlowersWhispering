<template>
  <div class="body">
    <div id="videoContainer">
        <video class="fullscreenVideo" id="kotoba" playsinline autoplay muted loop>
          <source src="../assets/video/background.mp4" type="video/mp4">
        </video>
      </div>
          <div class="main-box">
            <div :class="['container', 'container-register', { 'is-txl': isLogin }]">
              <form>
                <h2 class="title">注册</h2>
                <div class="input-wrapper">
          <div class="avatar-upload-area" @click="triggerUpload">
            <img v-if="avatarPreview" :src="avatarPreview" alt="头像预览" class="avatar-preview" />
          <div v-else class="avatar-placeholder">点击上传头像</div>
          </div>
            <input id="avatarUpload" type="file" accept="image/*" @change="handleAvatarUpload" class="avatar-upload-input" />
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.username" @input="handleInput('username')" class="form__input" type="username" placeholder="请输入用户名" required />
            <span class="validation-feedback" v-if="registerForm.touched.username" :class="{ 'error': !isUsernameValid, 'success': isUsernameValid }">
              {{ isUsernameValid ? '✓ 用户名格式正确' : '✗ 用户名不可与邮箱格式相同' }}
            </span>
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.email" @input="handleInput('email')" class="form__input" type="email" placeholder="请输入邮箱" required />
            <span class="validation-feedback" v-if="registerForm.touched.email" :class="{ 'error': !isEmailValid, 'success': isEmailValid }">
              {{ isEmailValid ? '✓ 邮箱格式正确' : '✗ 请输入有效的邮箱地址' }}
            </span>
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.password" @input="handleInput('password')" class="form__input" type="password" placeholder="请输入密码" required />
            <span class="validation-feedback" v-if="registerForm.touched.password" :class="{ 'error': !isPasswordValid, 'success': isPasswordValid }">
              {{ isPasswordValid ? '✓ 密码长度有效' : '✗ 密码长度必须在8到20个字符之间' }}
            </span>
          </div>
          <div class="input-wrapper">
            <input v-model="registerForm.confirmPassword" @input="handleInput('confirmPassword')" class="form__input" type="password" placeholder="请再次输入密码" required />
            <span class="validation-feedback" v-if="registerForm.touched.confirmPassword" :class="{ 'error': !arePasswordsMatching, 'success': arePasswordsMatching }">
            {{ arePasswordsMatching ? '✓ 密码匹配' : '✗ 两次输入的密码不一致' }}
            </span>
          </div>
          <div class="form__button" :disabled="isRegisterDisabled" @click="performRegister">
            立即注册
          </div>
        </form>
      </div>
      <div :class="['container', 'container-login', { 'is-txl is-z200': isLogin }]">
        <form>
          <h2 class="title">登录</h2>
          <div class="form__icons">
            <img class="form__icon" src="./images/wechat.png" alt="微信登录">
            <img class="form__icon" src="./images/alipay.png" alt="支付宝登录">
            <img class="form__icon" src="./images/QQ.png" alt="QQ登录">
          </div>
          <span class="text">或使用用户名登录</span>
          <input v-model="loginForm.usernameOrEmail" class="form__input" type="text" placeholder="用户名/邮箱" required />
          <input v-model="loginForm.password" class="form__input" type="password" placeholder="请输入密码" required />
          <div class="form__button" @click="performLogin">立即登录</div>        
           <!-- 游客登录按钮 -->
           <div class="form__button guest-button" @click="performGuestLogin">游客登录</div>
        </form>
      </div>
      <div :class="['switch', { 'login': isLogin }]">
        <div class="switch__circle"></div>
        <div class="switch__circle switch__circle_top"></div>
        <div class="switch__container">
          <h2>{{ isLogin ? '您好 !' : '欢迎回来 !' }}</h2>
          <p>
            {{
              isLogin
                  ? '如果您还没有账号，请点击下方立即注册按钮进行账号注册'
                  : '如果您已经注册过账号，请点击下方立即登录按钮进行登录'
            }}
          </p>
          <div class="form__button" @click="isLogin = !isLogin">
            {{ isLogin ? '立即注册' : '立即登录' }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref ,watch,computed} from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Login',
  setup() {
    const store = useStore();
    const router = useRouter();

    const isLogin = ref(true);
    const avatarPreview = ref<string | null>(null);
    avatarPreview.value = require('@/userprofile/images/user-avatar.jpg');
    const loginForm = ref({
      usernameOrEmail: '',
      password: ''
    });

    const registerForm = ref({
      username: '',
      email: '',
      password: '',
      confirmPassword: '',
      touched: {
        username: false,
        email: false,
        password: false,
        confirmPassword: false
      }
    });
    watch(isLogin, (newValue) => {
      if (newValue) {
        // 重置登录表单
        loginForm.value.usernameOrEmail = '';
        loginForm.value.password = '';
      } else {
        // 重置注册表单
        avatarPreview.value = require('@/userprofile/images/user-avatar.jpg');
        registerForm.value.username = '';
        registerForm.value.email = '';
        registerForm.value.password = '';
        registerForm.value.confirmPassword = '';
        registerForm.value.touched = {
          username: false,
          email: false,
          password: false,
          confirmPassword: false
        };
      }
    });
    const isUsernameValid = computed(() => {
      const nameRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return !nameRegex.test(registerForm.value.username);
    });
    const isEmailValid = computed(() => {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return emailRegex.test(registerForm.value.email);
    });

    const isPasswordValid = computed(() => {
      const password = registerForm.value.password;
      return password.length >= 8 && password.length <= 20;
    });

    const arePasswordsMatching = computed(() => {
      return registerForm.value.password === registerForm.value.confirmPassword;
    });

    const isRegisterDisabled = computed(() => {
      return (
        !isUsernameValid.value ||
        !isEmailValid.value ||
        !isPasswordValid.value ||
        !arePasswordsMatching.value
      );
    });
    const handleInput = (field: keyof typeof registerForm.value.touched) => {
      registerForm.value.touched[field] = true;
    };
    
    const performLogin = async () => {
      const { usernameOrEmail, password } = loginForm.value;

      if (!usernameOrEmail.trim() || !password.trim()) {
        alert('用户名/邮箱和密码不能为空！');
        return;
      }

      try {
        const response = await store.dispatch('login', { usernameOrEmail, password });
        if (response) {
          router.push('/home');
        }
      } catch (error:any) {
        alert(error.message || '登录失败');
      }
    };

    const performGuestLogin = async () => {
      try {
        const response = await store.dispatch('guestLogin');
        if (response) {
          router.push('/home');
        } else {
          alert('游客登录失败，请稍后再试。');
        }
      } catch (error) {
        alert('游客登录失败，请稍后再试。');
      }
    };

    // 触发文件上传选择
    const triggerUpload = () => {
      const uploadInput = document.getElementById('avatarUpload') as HTMLInputElement;
      uploadInput.click(); // 模拟点击文件输入
    };

    const handleAvatarUpload = (event: Event) => {
      const file = (event.target as HTMLInputElement).files?.[0];
      
      if (file) {
        const reader = new FileReader();
        
        // 使用 FileReader 读取文件
        reader.readAsDataURL(file);
        
        // 文件读取成功后，将结果存储到 avatarPreview
        reader.onload = () => {
          avatarPreview.value = reader.result as string; // 确保结果被正确赋值
          console.log('头像预览数据:', avatarPreview.value); //检查是否得到了正确的 Base64 数据
        };

        // 处理文件读取错误
        reader.onerror = () => {
          console.error('文件读取失败');
        };
      }
    };


    const performRegister = async () => {
      if (isRegisterDisabled.value) {
        return; // 如果按钮禁用，直接返回
      }
      const { username, email, password, confirmPassword } = registerForm.value;
      try {
        const response = await store.dispatch('register', {
          username,
          email,
          password,
          avatar: avatarPreview.value
        });
        if (response) {
          alert('注册成功!请登录');
          isLogin.value = true;
        } else {
          alert('注册失败');
        }
      } catch (error:any) {
        alert(error.message || '注册失败');
     }
   };

    return {
      isLogin,
      loginForm,
      registerForm,
      isEmailValid,
      isPasswordValid,
      isUsernameValid,
      arePasswordsMatching,
      isRegisterDisabled,
      handleInput,
      performLogin,
      performGuestLogin,
      performRegister,
      handleAvatarUpload,
      triggerUpload,
      avatarPreview,
    };
  }
});
</script>


<style lang="scss" scoped>
.avatar-upload-area {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  background-color: #f0f0f0;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  transition: box-shadow 0.3s ease;
}

.avatar-upload-area:hover {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
}

.avatar-preview {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.avatar-placeholder {
  font-size: 14px;
  color: #888;
  text-align: center;
}

.avatar-upload-input {
  display: none; /* 隐藏原生文件输入 */
}


.body {
  width: 100%;
  height: 100vh;
  min-width: 1200px;
  min-height: 800px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-family: "Montserrat", sans-serif;
  font-size: 12px;
  color: #a0a5a8;
}
#videoContainer {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    min-width: 1200px;
    min-height: 800px;
    z-index: -1; /* 让视频在背景层 */
    object-fit: cover;
    overflow: hidden;
  }
  
.fullscreenVideo {
  width: 100%;
  height: 100%;
  min-width: 1200px;
  min-height: 800px;
  object-fit: cover;
}
.main-box {
  position: relative;
  width: 1000px;
  min-width: 1000px;
  min-height: 600px;
  height: 600px;
  padding: 25px;
  background-color: #ecf0f3;
  box-shadow: 1px 1px 100px 10PX #ecf0f3;
  border-radius: 12px;
  overflow: hidden;

  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    width: 600px;
    height: 100%;
    padding: 25px;
    background-color: #ecf0f3;
    transition: all 1.25s;

    form {
      display: flex;
      justify-content: center;
      align-items: center;
      flex-direction: column;
      width: 100%;
      height: 100%;
      color: #a0a5a8;

      .form__icon {
        object-fit: contain;
        width: 30px;
        margin: 0 5px;
        opacity: .5;
        transition: .15s;

        &:hover {
          opacity: 1;
          transition: .15s;
          cursor: pointer;

        }
      }

      .title {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        font-size: 50px;
        font-weight: 70;
        line-height: 0;
        color: #181818;
      }

      .text {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        margin-top: 30px;
        margin-bottom: 12px;
        font-size: 14px;
      }

      .form__input {
        width: 250px;
        height: 40px;
        margin: 4px 0;
        padding-left: 25px;
        font-size: 14px;
        letter-spacing: 0.15px;
        border: none;
        outline: none;
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        background-color: #ecf0f3;
        transition: 0.25s ease;
        border-radius: 8px;
        box-shadow: inset 2px 2px 4px #d1d9e6, inset -2px -2px 4px #f9f9f9;

        &::placeholder {
          color: #a0a5a8;
        }
      }
    }
  }

  .container-register {
    z-index: 100;
    left: calc(100% - 650px);
    
  }

  .container-login {
    left: calc(100% - 650px);
    z-index: 0;
  }

  .is-txl {
    left: 0;
    transition: 1.25s;
    transform-origin: right;
  }

  .is-z200 {
    z-index: 200;
    transition: 1.25s;
  }

  .switch {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    width: 400px;
    padding: 0px;
    z-index: 200;
    transition: 1.25s;
    background-color: #ecf0f3;
    overflow: hidden;
    box-shadow: 4px 4px 10px #d1d9e6, -4px -4px 10px #f9f9f9;
    color: #a0a5a8;

    .switch__circle {
      position: absolute;
      width: 500px;
      height: 500px;
      border-radius: 50%;
      background-color: #ecf0f3;
      box-shadow: inset 8px 8px 12px #d1d9e6, inset -8px -8px 12px #f9f9f9;
      bottom: -60%;
      left: -60%;
      transition: 1.25s;
    }

    .switch__circle_top {
      top: -30%;
      left: 60%;
      width: 300px;
      height: 300px;
    }

    .switch__container {
      display: flex;
      justify-content: center;
      align-items: center;
      flex-direction: column;
      position: absolute;
      width: 400px;
      padding: 50px 55px;
      transition: 1.25s;

      h2 {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        font-size: 58px;
        font-weight: 70;
        line-height: 3;
        color: #181818;
      }

      p {
        //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
        font-size: 14px;
        letter-spacing: 0.25px;
        text-align: center;
        line-height: 1.6;
      }
    }
  }

  .login {
    left: calc(100% - 400px);

    .switch__circle {
      left: 0;
    }
  }

  .input-wrapper {
    display: flex;
    flex-direction: column;
    align-items: start;
  }

  .validation-feedback {
    margin-top: 2px;
    margin-left:20px;
    font-size: 12px;
  }

  .success {
    color: green;
  }

  .error {
    color: red;
  }

  .form__button {
    //font-family: 'Caveat-VariableFont','ZhiMangXing-Regular', sans-serif;
    width: 180px;
    height: 50px;
    border-radius: 25px;
    margin-top: 50px;
    text-align: center;
    line-height: 50px;
    font-weight: 0;
    font-size: 18px;
    letter-spacing: 2px;
    background-color: #4b70e2;
    color: #f9f9f9;
    cursor: pointer;
    box-shadow: 8px 8px 16px #d1d9e6, -8px -8px 16px #f9f9f9;

    &:hover {
      box-shadow: 2px 2px 3px 0 rgba(255, 255, 255, 50%),
      -2px -2px 3px 0 rgba(116, 125, 136, 50%),
      inset -2px -2px 3px 0 rgba(255, 255, 255, 20%),
      inset 2px 2px 3px 0 rgba(0, 0, 0, 30%);
    }
  }
  .guest-button {
  margin-top: 10px;
  background-color: #e0e0e0;
  color: #333;
  }

  .guest-button:hover {
  background-color: #ccc;
  }

}
</style>


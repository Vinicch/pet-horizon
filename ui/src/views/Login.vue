<template>
  <div class="login">
    <el-card>
      <h2>Login</h2>
      <el-form ref="form" class="login-form" :model="model" @submit.prevent="login">
        <el-form-item prop="username">
          <el-input v-model="model.email" placeholder="E-mail" prefix-icon="fas fa-user"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="model.password" placeholder="Senha" type="password" prefix-icon="fas fa-lock"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button class="login-button" type="primary" native-type="submit" block>Login</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import axios from 'axios'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const model = ref({
  email: '',
  password: '',
})

const canLogin = () => !!model.value.email && !!model.value.password

const login = async () => {
  if (!canLogin) return

  const response = await axios.post('/auth', model.value)

  localStorage.setItem('user', JSON.stringify(response.data))

  if (response.data.user.ongId) router.push('/dashboard')
  else router.push('/search')
}
</script>

<style scoped>
.login {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-button {
  width: 100%;
  margin-top: 40px;
}

.login-form {
  width: 290px;
}

.forgot-password {
  margin-top: 10px;
}
</style>

<style lang="scss" scoped>
$teal: rgb(0, 124, 137);
.el-button--primary {
  background: $teal;
  border-color: $teal;

  &:hover,
  &.active,
  &:focus {
    background: lighten($teal, 7);
    border-color: lighten($teal, 7);
  }
}

.login .el-input__inner:hover {
  border-color: $teal;
}

.login .el-input__prefix {
  background: rgb(238, 237, 234);
  left: 0;
  height: calc(100% - 2px);
  left: 1px;
  top: 1px;
  border-radius: 3px;
  .el-input__icon {
    width: 30px;
  }
}

.login .el-input input {
  padding-left: 35px;
}

.login .el-card {
  padding-top: 0;
  padding-bottom: 30px;
}

h2 {
  font-family: 'Open Sans';
  letter-spacing: 1px;
  font-family: Roboto, sans-serif;
  padding-bottom: 20px;
}
a {
  color: $teal;
  text-decoration: none;
  &:hover,
  &:active,
  &:focus {
    color: lighten($teal, 7);
  }
}

.login .el-card {
  width: 340px;
  display: flex;
  justify-content: center;
}
</style>

<template>
  <!-- Login -->
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
        <a href="#" @click="isModalOpen = true">Não tem uma conta? Cadastre-se!</a>
      </el-form>
    </el-card>
  </div>

  <!-- Registration -->
  <el-dialog v-model="isModalOpen" title="Cadastro" :before-close="closeModal">
    <el-row><el-input v-model="user.name" placeholder="Nome *" /></el-row>
    <el-row><el-input v-model="user.email" placeholder="E-mail *" /></el-row>
    <el-row><el-input v-if="!user.id" v-model="user.password" placeholder="Senha *" show-password /></el-row>
    <el-row><el-input v-model="user.uin" placeholder="CPF *" /></el-row>
    <el-row><el-input v-model="user.address" placeholder="Endereço *" /></el-row>

    <el-row>
      <el-select v-model="user.residence" placeholder="Residência *">
        <el-option label="Casa" value="Casa"></el-option>
        <el-option label="Kitnet" value="Kitnet"></el-option>
        <el-option label="Apartamento" value="Apartamento"></el-option>
        <el-option label="Sobrado" value="Grande"></el-option>
      </el-select>
      &nbsp; &nbsp;
      <el-select v-model="user.residenceSize" placeholder="Tamanho da Residência *">
        <el-option label="Pequena" value="Pequena"></el-option>
        <el-option label="Média" value="Média"></el-option>
        <el-option label="Grande" value="Grande"></el-option>
      </el-select>
      &nbsp; &nbsp;
      <el-select v-model="user.income" placeholder="Renda *">
        <el-option label="Até R$ 1.800,00" value="Até R$ 1.800,00"></el-option>
        <el-option label="R$ 1.801 até R$ 2.600,00" value="R$ 1.801 até R$ 2.600,00"></el-option>
        <el-option label="R$2.601 até R$ 4.000,00" value="R$2.601 até R$ 4.000,00"></el-option>
        <el-option label="R$4.001 até R$ 9.000,00" value="R$4.001 até R$ 9.000,00"></el-option>
      </el-select>
    </el-row>

    <el-checkbox v-model="user.hasYard" label="Possui Quintal" />
    <el-checkbox v-model="user.hasWindowBars" label="Possui Grades/Telas" />

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import axios from 'axios'
import { defaultUser } from '@/models/user'
import { getLocation } from '@/utils/google-maps'
import { ElMessage } from 'element-plus'
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

//#region Login

const model = ref({
  email: '',
  password: '',
})
const canLogin = computed(() => !!model.value.email && !!model.value.password)

const login = async () => {
  if (!canLogin.value) {
    ElMessage({
      type: 'error',
      message: 'Preencha os campos de usuário e senha',
    })

    return
  }

  try {
    const response = await axios.post('/auth', model.value)

    localStorage.setItem('user', JSON.stringify(response.data))

    if (response.data.user.ongId) router.push('/dashboard')
    else router.push('/search')

    ElMessage({
      type: 'success',
      message: 'Logado com sucesso',
    })
  } catch {
    ElMessage({
      type: 'error',
      message: 'Login inválido',
    })
  }
}

//#endregion Login

//#region Registration

const isModalOpen = ref(false)
const user = ref(defaultUser())

const canSave = computed(() => {
  return (
    !!user.value.name &&
    !!user.value.email &&
    !!user.value.password &&
    !!user.value.uin &&
    !!user.value.address &&
    !!user.value.residence &&
    !!user.value.residenceSize &&
    !!user.value.income
  )
})

const closeModal = () => {
  isModalOpen.value = false
  user.value = defaultUser()
}

const save = async () => {
  if (!canSave.value) {
    ElMessage({
      type: 'error',
      message: 'Preencha todos os campos obrigatórios',
    })

    return
  }

  try {
    const location = await getLocation(user.value.address)

    user.value.lat = location.lat()
    user.value.long = location.lng()

    await axios.post('/users', user.value)

    closeModal()

    ElMessage({
      type: 'success',
      message: 'Cadastrado com sucesso!',
    })
  } catch {
    ElMessage({
      type: 'error',
      message: 'Falha ao registrar',
    })
  }
}

//#endregion Registration
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

.el-row {
  margin-bottom: 1rem;
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

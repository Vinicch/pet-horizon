<template>
  <h1>Usuários</h1>

  <el-button type="primary" @click="openModal()">Criar Usuário</el-button>

  <el-table :data="users" :stripe="true" :highlight-current-row="true" style="width: fit-content">
    <el-table-column align="center">
      <template #default="scope">
        <i class="fas fa-eye hover" @click="openModal(scope.row.id)" />
      </template>
    </el-table-column>
    <el-table-column prop="name" label="Nome" width="180" />
    <el-table-column prop="email" label="E-mail" width="180" />
    <el-table-column prop="uin" label="CPF" width="180" />
    <el-table-column prop="address" label="Endereço" width="180" />
    <el-table-column prop="residence" label="Residência" width="180" />
    <el-table-column prop="residenceSize" label="Tamanho da Residência" width="180" />
    <el-table-column prop="hasYard" label="Possui Quintal" width="180">
      <template #default="scope">{{ scope.row.hasYard ? 'Sim' : 'Não' }}</template>
    </el-table-column>
    <el-table-column prop="hasWindowBars" label="Possui Grades/Telas" width="180">
      <template #default="scope">{{ scope.row.hasWindowBars ? 'Sim' : 'Não' }}</template>
    </el-table-column>
    <el-table-column prop="income" label="Renda" width="180" />
  </el-table>

  <!-- Modal -->
  <el-dialog v-model="isModalVisible" title="Criar usuário" :before-close="closeModal">
    <el-input v-model="user.name" placeholder="Nome" />
    <el-input v-model="user.email" placeholder="E-mail" />
    <el-input v-if="!user.id" v-model="user.password" placeholder="Senha" show-password />
    <el-input v-model="user.uin" placeholder="CPF" />
    <el-input v-model="user.address" placeholder="Endereço" />

    <el-select v-model="user.residence" placeholder="Residência">
      <el-option label="Casa" value="Casa"></el-option>
      <el-option label="Kitnet" value="Kitnet"></el-option>
      <el-option label="Apartamento" value="Apartamento"></el-option>
      <el-option label="Sobrado" value="Grande"></el-option>
    </el-select>

    <el-select v-model="user.residenceSize" placeholder="Tamanho da Residência">
      <el-option label="Pequena" value="Pequena"></el-option>
      <el-option label="Média" value="Média"></el-option>
      <el-option label="Grande" value="Grande"></el-option>
    </el-select>

    <el-checkbox v-model="user.hasYard" label="Possui Quintal" />
    <el-checkbox v-model="user.hasWindowBars" label="Possui Grades/Telas" />
    <br />
    <el-select v-model="user.income" placeholder="Renda">
      <el-option label="Até R$ 1.800,00" value="Até R$ 1.800,00"></el-option>
      <el-option label="R$ 1.801 até R$ 2.600,00" value="R$ 1.801 até R$ 2.600,00"></el-option>
      <el-option label="R$2.601 até R$ 4.000,00" value="R$2.601 até R$ 4.000,00"></el-option>
      <el-option label="R$4.001 até R$ 9.000,00" value="R$4.001 até R$ 9.000,00"></el-option>
    </el-select>

    <el-select v-model="user.ongId" placeholder="ONG associada">
      <el-option :label="'Nenhuma'" :value="null"></el-option>
      <el-option v-for="ong in ongs" :key="ong.id" :label="ong.name" :value="ong.id"></el-option>
    </el-select>

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button v-if="user.id" type="danger" @click="remove()">Remover</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { Ong } from '@/models/ong'
import { defaultUser, User } from '@/models/user'
import { getLocation } from '@/utils/google-maps'
import axios from 'axios'
import { ElMessage } from 'element-plus/lib/components/message'
import { ElMessageBox } from 'element-plus/lib/components/message-box'
import { computed, onMounted, ref } from 'vue'

//#region List

const users = ref<User[]>([])

const getAll = () => {
  axios.get('/users').then((resp) => (users.value = resp.data))
}

onMounted(() => getAll())

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const user = ref(defaultUser())
const ongs = ref<Ong[]>([])
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

const openModal = (id?: number) => {
  axios.get('/ongs').then((resp) => (ongs.value = resp.data))
  user.value = users.value.find((e) => e.id === id) ?? user.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  user.value = defaultUser()
}

const save = async () => {
  if (!canSave.value) return

  const location = await getLocation(user.value.address)

  user.value.lat = location.lat()
  user.value.long = location.lng()

  if (user.value.id) {
    await axios.put('/users', user.value)
    getAll()
  } else {
    await axios.post('/users', user.value)
    getAll()
  }

  closeModal()
}

const remove = async () => {
  const id = user.value.id

  closeModal()

  ElMessageBox.confirm('Você tem certeza que deseja remover esse registro?', 'Warning', {
    confirmButtonText: 'Sim',
    cancelButtonText: 'Cancelar',
    type: 'warning',
  })
    .then(async () => {
      await axios.delete(`/users/${id}`)
      getAll()

      ElMessage({
        type: 'success',
        message: 'Removido com sucesso',
      })
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: 'Deleção cancelada',
      })
    })
}

//#endregion Modal
</script>

<style lang="scss" scoped>
* {
  margin: 1%;
}
</style>

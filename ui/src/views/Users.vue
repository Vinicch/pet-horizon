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
    <el-input v-model="user.uin" placeholder="CPF" />
    <el-input v-model="user.address" placeholder="Endereço" />
    <el-input v-model="user.residence" placeholder="Residência" />
    <el-input v-model="user.residenceSize" placeholder="Tamanho da Residência" />
    <el-input v-model="user.hasYard" placeholder="Possui Quintal" />
    <el-input v-model="user.hasWindowBars" placeholder="Possui Grades/Telas" />
    <el-input v-model="user.income" placeholder="Renda" />

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button v-if="user.id" type="danger" @click="remove()">Remover</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { defaultUser, User } from '@/models/user'
import { getLocation } from '@/utils/google-maps'
import { computed, ref } from 'vue'

//#region List

const users = ref<User[]>([
  {
    id: 1,
    name: 'Vinícius Leme Alves',
    email: 'vibalta@gmail.com',
    password: 'admin@123',
    uin: '41825251835',
    address: 'Rua Marco Rodrigues, 50',
    lat: 32.123123,
    long: 19.465645,
    residence: 'Casa',
    residenceSize: 'Grande',
    hasYard: true,
    hasWindowBars: true,
    income: '1501 a 3000',
  },
])

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const user = ref(defaultUser())
const canSave = computed(
  () =>
    !!user.value.name &&
    !!user.value.email &&
    !!user.value.password &&
    !!user.value.uin &&
    !!user.value.address &&
    !!user.value.lat &&
    !!user.value.long &&
    !!user.value.residence &&
    !!user.value.residenceSize &&
    !!user.value.income,
)

const openModal = (id?: number) => {
  user.value = users.value.find((e) => e.id === id) ?? user.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  user.value = defaultUser()
}

const save = async () => {
  const location = await getLocation(user.value.address)

  user.value.lat = location.lat()
  user.value.long = location.lng()

  console.log(user.value)

  if (!canSave.value) return

  if (user.value.id) {
    const index = users.value.findIndex((e) => e.id === user.value.id)

    users.value[index] = user.value
  } else {
    users.value.push({ ...user.value, id: users.value.length + 1 })
  }

  closeModal()
}

const remove = () => {
  users.value = users.value.filter((e) => e.id !== user.value.id)

  closeModal()
}

//#endregion Modal
</script>

<style lang="scss" scoped>
* {
  margin: 1%;
}
</style>

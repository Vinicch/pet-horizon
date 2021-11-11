<template>
  <h1 class="search">Busque um pet e lhe dê um novo lar</h1>

  <div class="search">
    <el-card class="search-form">
      <el-row>
        <el-input v-model="model.breed" placeholder="Raça"></el-input>
        <el-input v-model="model.color" placeholder="Cor"></el-input>

        <el-select v-model="model.size" placeholder="Porte *">
          <el-option label="Mini" value="Mini"></el-option>
          <el-option label="Pequeno" value="Pequeno"></el-option>
          <el-option label="Médio" value="Médio"></el-option>
          <el-option label="Grande" value="Grande"></el-option>
          <el-option label="Gigante" value="Gigante"></el-option>
        </el-select>

        <el-radio v-model="model.sex" label="F" size="medium">Feminino</el-radio>
        <el-radio v-model="model.sex" label="M" size="medium">Masculino</el-radio>
      </el-row>

      <el-row class="search-button">
        <el-button type="primary" :disabled="!canSearch" @click="search">Buscar</el-button>
      </el-row>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { defaultSearch } from '@/models/search'
import { User } from '@/models/user'
import axios from 'axios'
import { ElMessage } from 'element-plus'
import { computed, ref } from 'vue'

// TODO: create request/cancel flow
const model = ref(defaultSearch())

const canSearch = computed(() => !!model.value.size && !!model.value.sex)

const search = async () => {
  if (!canSearch.value) {
    ElMessage({
      type: 'error',
      message: 'Preencha, no mínimo, os campos de porte e sexo',
    })
  }

  const userData = localStorage.getItem('user') ?? ''
  const user = JSON.parse(userData).user as User

  model.value.userId = user.id

  const response = await axios.post('/pets/search', model.value)

  console.log(response.data)
}
</script>

<style lang="scss" scoped>
* {
  margin: 1%;
}

.search {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.search-form {
  width: 30vw;
}

.search-button {
  display: flex;
  justify-content: flex-end;
}
</style>

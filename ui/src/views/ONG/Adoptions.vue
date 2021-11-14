<template>
  <h1>Processos de Adoção</h1>

  <!-- List -->
  <el-table :data="adoptions" stripe highlight-current-row style="width: fit-content">
    <el-table-column align="center">
      <template #default>
        <i class="fas fa-eye hover" />
      </template>
    </el-table-column>
    <el-table-column prop="user.name" label="Nome do cliente" width="180"></el-table-column>
    <el-table-column prop="user.uin" label="CPF do cliente" width="180"></el-table-column>
    <el-table-column prop="pet.breed" label="Raça do pet" width="180"></el-table-column>
    <el-table-column prop="pet.size" label="Porte do pet" width="180"></el-table-column>
    <el-table-column prop="pet.sex" label="Sexo do pet" width="180">
      <template #default="scope">{{ scope.row.pet.sex === 'F' ? 'Feminino' : 'Masculino' }}</template>
    </el-table-column>
    <el-table-column prop="situation" label="Situação" width="180">
      <template #default="scope">{{ getSituation(scope.row) }}</template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import axios from 'axios'
import { Adoption } from '@/models/adoption'
import { onMounted, ref } from 'vue'

//#region List

const adoptions = ref<Adoption[]>([])

const getAll = () => {
  axios.get('/adoptions').then((resp) => (adoptions.value = resp.data))
}

onMounted(() => getAll())

const getSituation = (row: Adoption) => {
  switch (row.situation) {
    case 'A':
      return 'Aprovado'
    case 'R':
      return 'Reprovado'
    case 'W':
      return 'Pendente'
  }
}

//#endregion List

// TODO: create adoption response flow
</script>

<style scoped lang="scss">
* {
  margin: 1%;
}
</style>

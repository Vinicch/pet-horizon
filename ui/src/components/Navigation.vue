<template>
  <div class="search">
    <el-card class="search-form">
      <img class="photo" :src="pet.photo" alt="Foto do pet" />

      <h3>Informações do animal</h3>

      <span>Raça</span>
      <el-input v-model="pet.breed" disabled></el-input>

      <span>Cor</span>
      <el-input v-model="pet.color" disabled></el-input>

      <span>Porte</span>
      <el-input v-model="pet.size" disabled></el-input>

      <span>Sexo</span>
      <br />
      <el-radio v-model="pet.sex" disabled label="F" size="medium">Feminino</el-radio>
      <el-radio v-model="pet.sex" disabled label="M" size="medium">Masculino</el-radio>

      <h3>Informações da ONG</h3>

      <span>Nome</span>
      <el-input v-model="pet.ong.name" disabled></el-input>

      <span>Endereço</span>
      <el-input v-model="pet.ong.address" disabled></el-input>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { Pet } from '@/models/pet'
import { ref } from 'vue'

interface Props {
  pets: Pet[]
}

const props = defineProps<Props>()
const pet = ref(props.pets[0])
</script>

<style scoped lang="scss">
.photo {
  margin: 1% auto;
  display: block;
}

.el-input {
  margin-top: 0.5rem;
  margin-bottom: 1rem;
}
</style>

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

      <span>Personalidade</span>
      <el-input v-model="pet.personality" disabled></el-input>

      <span>Sexo</span>
      <br />
      <el-radio v-model="pet.sex" disabled label="F" size="medium">Feminino</el-radio>
      <el-radio v-model="pet.sex" disabled label="M" size="medium">Masculino</el-radio>

      <h3>Informações da ONG</h3>

      <span>Nome</span>
      <el-input v-model="pet.ong.name" disabled></el-input>

      <span>CNPJ</span>
      <el-input v-model="pet.ong.uin" disabled></el-input>

      <span>Endereço</span>
      <el-input v-model="pet.ong.address" disabled></el-input>

      <el-row class="buttons-row">
        <el-button type="info" :disabled="isFirstpet" @click="previous">Ver anterior</el-button>

        <div>
          <el-button type="primary" @click="adopt">Adotar</el-button>
          <el-button type="danger" @click="back">Voltar para busca</el-button>
        </div>

        <el-button type="info" :disabled="isLastPet" @click="next">Ver próximo</el-button>
      </el-row>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { defaultAdoption } from '@/models/adoption'
import { defaultPet, Pet } from '@/models/pet'
import { User } from '@/models/user'
import axios from 'axios'
import { computed, ref } from 'vue'

interface Props {
  pets: Pet[]
}

interface Emits {
  (e: 'back'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const pet = ref(props.pets[0])
const adoption = ref(defaultAdoption())

const isFirstpet = computed(() => props.pets[0] === pet.value)
const isLastPet = computed(() => props.pets[props.pets.length - 1] === pet.value)

const previous = () => {
  if (!isFirstpet.value) {
    const index = props.pets.indexOf(pet.value)

    pet.value = props.pets[index - 1]
  }
}

const next = () => {
  if (!isLastPet.value) {
    const index = props.pets.indexOf(pet.value)

    pet.value = props.pets[index + 1]
  }
}

const back = () => {
  adoption.value = defaultAdoption()
  pet.value = defaultPet()

  emit('back')
}

const adopt = async () => {
  const userData = localStorage.getItem('user') ?? ''
  const user = JSON.parse(userData).user as User

  adoption.value.ongId = pet.value.ongId
  adoption.value.petId = pet.value.id
  adoption.value.userId = user.id

  await axios.post('/adoptions', adoption.value)

  back()
}
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

.buttons-row {
  display: flex;
  justify-content: space-between;
}
</style>

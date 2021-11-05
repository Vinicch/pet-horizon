<template>
  <h1>Pets</h1>

  <el-button type="primary" @click="openModal()">Cadastrar Pet</el-button>

  <el-table :data="pets" :stripe="true" :highlight-current-row="true" style="width: fit-content">
    <el-table-column align="center">
      <template #default="scope">
        <i class="fas fa-eye hover" @click="openModal(scope.row.id)" />
      </template>
    </el-table-column>
    <el-table-column prop="breed" label="Raça" width="180" />
    <el-table-column prop="color" label="Cor" width="180" />
    <el-table-column prop="size" label="Porte" width="180" />
    <el-table-column prop="sex" label="Sexo" width="180" />
    <el-table-column prop="personality" label="Personalidade" width="180" />
    <el-table-column prop="wasAdopted" label="Adotado" width="180">
      <template #default="scope">{{ scope.row.wasAdopted ? 'Sim' : 'Não' }}</template>
    </el-table-column>
  </el-table>

  <!-- Modal -->
  <el-dialog v-model="isModalVisible" title="Cadastrar pet" :before-close="closeModal">
    <el-input v-model="pet.breed" placeholder="Raça" />
    <el-input v-model="pet.color" placeholder="Cor" />
    <el-input v-model="pet.size" placeholder="Porte" />
    <el-input v-model="pet.sex" placeholder="Sexo" />
    <el-input v-model="pet.personality" placeholder="Personalidade" />
    <el-input v-model="pet.wasAdopted" placeholder="Foi adotado" />

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button v-if="pet.id" type="danger" @click="remove()">Remover</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { defaultPet, Pet } from '@/models/pet'
import { computed, ref } from 'vue'

//#region List

const pets = ref<Pet[]>([
  {
    id: 1,
    breed: 'Dalmata',
    color: 'Branco',
    size: 'Grande',
    sex: 'F',
    personality: 'Gentil;Carente',
    wasAdopted: false,
  },
])

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const pet = ref(defaultPet())
const canSave = computed(
  () => !!pet.value.breed && !!pet.value.color && !!pet.value.size && !!pet.value.sex && !!pet.value.personality,
)

const openModal = (id?: number) => {
  pet.value = pets.value.find((e) => e.id === id) ?? pet.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  pet.value = defaultPet()
}

const save = () => {
  if (!canSave.value) return

  if (pet.value.id) {
    const index = pets.value.findIndex((e) => e.id === pet.value.id)

    pets.value[index] = pet.value
  } else {
    pets.value.push({ ...pet.value, id: pets.value.length + 1 })
  }

  closeModal()
}

const remove = () => {
  pets.value = pets.value.filter((e) => e.id !== pet.value.id)

  closeModal()
}

//#endregion Modal
</script>

<style lang="scss" scoped>
* {
  margin: 1%;
}
</style>

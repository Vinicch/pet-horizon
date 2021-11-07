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
    <el-table-column prop="sex" label="Sexo" width="180">
      <template #default="scope">{{ scope.row.sex === 'F' ? 'Feminino' : 'Masculino' }}</template>
    </el-table-column>
    <el-table-column prop="personality" label="Personalidade" width="180" />
    <el-table-column prop="wasAdopted" label="Adotado" width="180">
      <template #default="scope">{{ scope.row.wasAdopted ? 'Sim' : 'Não' }}</template>
    </el-table-column>
  </el-table>

  <!-- Modal -->
  <el-dialog v-model="isModalVisible" title="Cadastrar pet" :before-close="closeModal">
    <el-input v-model="pet.breed" placeholder="Raça" />

    <el-input v-model="pet.color" placeholder="Cor" />

    <el-select v-model="pet.size" placeholder="Porte">
      <el-option label="Mini" value="Mini"></el-option>
      <el-option label="Pequeno" value="Pequeno"></el-option>
      <el-option label="Médio" value="Médio"></el-option>
      <el-option label="Grande" value="Grande"></el-option>
      <el-option label="Gigante" value="Gigante"></el-option>
    </el-select>

    <el-radio v-model="pet.sex" label="F" size="medium">Feminino</el-radio>
    <el-radio v-model="pet.sex" label="M" size="medium">Masculino</el-radio>

    <el-input v-model="pet.personality" placeholder="Personalidade" />

    <el-select v-model="pet.ongId" placeholder="ONG associada">
      <el-option :label="'Nenhuma ONG selecionada'" :value="0"></el-option>
      <el-option v-for="ong in ongs" :key="ong.id" :label="ong.name" :value="ong.id"></el-option>
    </el-select>

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button v-if="pet.id" type="danger" @click="remove()">Remover</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { Ong } from '@/models/ong'
import { defaultPet, Pet } from '@/models/pet'
import axios from 'axios'
import { ElMessage } from 'element-plus/lib/components/message'
import { ElMessageBox } from 'element-plus/lib/components/message-box'
import { computed, onMounted, ref } from 'vue'

//#region List

const pets = ref<Pet[]>([])

const getAll = () => {
  axios.get('/pets').then((resp) => (pets.value = resp.data))
}

onMounted(() => getAll())

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const pet = ref(defaultPet())
const ongs = ref<Ong[]>([])
const canSave = computed(() => {
  return (
    !!pet.value.breed &&
    !!pet.value.color &&
    !!pet.value.size &&
    !!pet.value.sex &&
    !!pet.value.personality &&
    !!pet.value.ongId
  )
})

const openModal = (id?: number) => {
  axios.get('/ongs').then((resp) => (ongs.value = resp.data))
  pet.value = pets.value.find((e) => e.id === id) ?? pet.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  pet.value = defaultPet()
}

const save = async () => {
  if (!canSave.value) return

  if (pet.value.id) {
    await axios.put('/pets', pet.value)
    getAll()
  } else {
    await axios.post('/pets', pet.value)
    getAll()
  }

  closeModal()
}

const remove = async () => {
  const id = pet.value.id

  closeModal()

  ElMessageBox.confirm('Você tem certeza que deseja remover esse registro?', 'Warning', {
    confirmButtonText: 'Sim',
    cancelButtonText: 'Cancelar',
    type: 'warning',
  })
    .then(async () => {
      await axios.delete(`/pets/${id}`)
      getAll()

      ElMessage({
        type: 'success',
        message: 'Delete completed',
      })
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: 'Delete canceled',
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

<template>
  <h1>Pets</h1>

  <el-button type="primary" @click="openModal()">Cadastrar Pet</el-button>

  <el-table :data="pets" stripe highlight-current-row style="width: fit-content">
    <el-table-column align="center">
      <template #default="scope">
        <i class="fas fa-eye hover" @click="openModal(scope.row.id)" />
      </template>
    </el-table-column>
    <el-table-column prop="type" label="Tipo" width="180" />
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
  <el-dialog v-model="isModalVisible" :before-close="closeModal">
    <el-row>
      <el-col :span="11">
        <span>Raça</span>
        <el-input v-model="pet.breed" />
      </el-col>

      <el-col :span="11">
        <span>Cor</span>
        <el-input v-model="pet.color" />
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="11">
        <span>Personalidade</span>
        <el-input v-model="pet.personality" />
      </el-col>

      <el-col :span="11">
        <span>Tipo</span>
        <br />
        <el-select v-model="pet.type" style="width: 100%">
          <el-option label="Gato" value="Gato"></el-option>
          <el-option label="Cachorro" value="Cachorro"></el-option>
          <el-option label="Pássaro" value="Pássaro"></el-option>
          <el-option label="Hamster" value="Hamster"></el-option>
        </el-select>
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="11">
        <span>Porte</span>
        <br />
        <el-select v-model="pet.size" style="width: 100%">
          <el-option label="Mini" value="Mini"></el-option>
          <el-option label="Pequeno" value="Pequeno"></el-option>
          <el-option label="Médio" value="Médio"></el-option>
          <el-option label="Grande" value="Grande"></el-option>
          <el-option label="Gigante" value="Gigante"></el-option>
        </el-select>
      </el-col>

      <el-col :span="11">
        <span>ONG associada</span>
        <br />
        <el-select v-model="pet.ongId" style="width: 100%">
          <el-option :label="'Nenhuma ONG selecionada'" :value="0"></el-option>
          <el-option v-for="ong in ongs" :key="ong.id" :label="ong.name" :value="ong.id"></el-option>
        </el-select>
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="11">
        <span>Sexo</span>
        <br />
        <el-radio v-model="pet.sex" label="F" size="medium">Feminino</el-radio>
        <el-radio v-model="pet.sex" label="M" size="medium">Masculino</el-radio>
      </el-col>

      <el-col :span="11">
        <span>Foto do pet</span>
        <br />
        <input type="file" @change="handleSubmit" />
      </el-col>
    </el-row>

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
    !!pet.value.type &&
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

const handleSubmit = (event: Event) => {
  const input = event.target as HTMLInputElement

  if (input.files) {
    const file = input.files[0]
    const reader = new FileReader()

    reader.onloadend = () => {
      if (reader.result) {
        pet.value.photo = reader.result.toString()
      }
    }

    reader.readAsDataURL(file)
  }
}

//#endregion Modal
</script>

<style lang="scss" scoped>
* {
  margin: 1%;
}
</style>

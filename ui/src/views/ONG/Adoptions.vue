<template>
  <h1>Processos de Adoção</h1>

  <!-- List -->
  <el-table :data="adoptions" stripe highlight-current-row style="width: fit-content">
    <el-table-column align="center">
      <template #default="scope">
        <i class="fas fa-eye hover" @click="openModal(scope.row.id)" />
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

  <!-- Modal -->
  <el-dialog
    v-model="isModalVisible"
    :title="`${adoption.user.name} - ${adoption.pet.breed} - ${getSituation(adoption)}`"
    :before-close="closeModal"
  >
    <h3>Informações do cliente</h3>

    <el-row>
      <el-col :span="11">
        <span>Nome</span>
        <el-input v-model="adoption.user.name" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>CPF</span>
        <el-input v-model="adoption.user.uin" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>E-mail</span>
        <el-input v-model="adoption.user.email" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Endereço</span>
        <el-input v-model="adoption.user.address" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Renda</span>
        <el-input v-model="adoption.user.income" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Tipo de residência</span>
        <el-input v-model="adoption.user.residence" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Tamanho da residência</span>
        <el-input v-model="adoption.user.residenceSize" disabled></el-input>
      </el-col>

      <el-checkbox v-model="adoption.user.hasYard" label="Possui Quintal" disabled />
      <el-checkbox v-model="adoption.user.hasWindowBars" label="Possui Grades/Telas" disabled />
    </el-row>

    <h3>Informações do pet</h3>

    <el-row>
      <el-col :span="11">
        <span>Raça</span>
        <el-input v-model="adoption.pet.breed" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Cor</span>
        <el-input v-model="adoption.pet.color" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Porte</span>
        <el-input v-model="adoption.pet.size" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Personalidade</span>
        <el-input v-model="adoption.pet.personality" disabled></el-input>
      </el-col>

      <el-radio v-model="adoption.pet.sex" disabled label="F" size="medium">Feminino</el-radio>
      <el-radio v-model="adoption.pet.sex" disabled label="M" size="medium">Masculino</el-radio>
    </el-row>

    <h3>Informações da ONG</h3>

    <el-row>
      <el-col :span="11">
        <span>Nome</span>
        <el-input v-model="adoption.ong.name" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>CNPJ</span>
        <el-input v-model="adoption.ong.uin" disabled></el-input>
      </el-col>

      <el-col :span="11">
        <span>Endereço</span>
        <el-input v-model="adoption.ong.address" disabled></el-input>
      </el-col>
    </el-row>

    <template #footer>
      <span v-if="adoption.situation === 'W'">
        <el-button type="success" @click="save(true)">Aprovar</el-button>
        <el-button type="danger" @click="save(false)">Reprovar</el-button>
      </span>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import axios from 'axios'
import { Adoption, defaultAdoption } from '@/models/adoption'
import { onMounted, ref } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

//#region List

const adoptions = ref<Adoption[]>([])

const getAll = () => {
  axios.get('/adoptions').then((resp) => (adoptions.value = resp.data))
}

onMounted(() => getAll())

const getSituation = (adoption: Adoption) => {
  switch (adoption.situation) {
    case 'A':
      return 'Aprovado'
    case 'R':
      return 'Reprovado'
    case 'W':
      return 'Pendente'
  }
}

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const adoption = ref(defaultAdoption())

const openModal = (id: number) => {
  adoption.value = adoptions.value.find((e) => e.id === id) ?? adoption.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  adoption.value = defaultAdoption()
}

const save = (isApproved: boolean) => {
  adoption.value.situation = isApproved ? 'A' : 'R'

  if (!isApproved) {
    ElMessageBox.prompt('Descreva a razão da reprovação')
      .then((value) => {
        adoption.value.disapprovalReason = value

        updateAdoption()
      })
      .catch(() => {
        ElMessage({
          type: 'info',
          message: 'Processo cancelado',
        })
      })

    return
  }

  updateAdoption()
}

const updateAdoption = async () => {
  await axios.put('/adoptions', adoption.value)

  getAll()
  closeModal()
}

//#endregion Modal

// TODO: create adoption response flow
</script>

<style scoped lang="scss">
* {
  margin: 1%;
}
</style>

<template>
  <h1>ONGs</h1>

  <el-button type="primary" @click="openModal()">Cadastrar ONG</el-button>

  <!-- List -->
  <el-table :data="ongs" :stripe="true" :highlight-current-row="true" style="width: fit-content">
    <el-table-column align="center">
      <template #default="scope">
        <i class="fas fa-eye hover" @click="openModal(scope.row.id)" />
      </template>
    </el-table-column>
    <el-table-column prop="name" label="Nome" width="180" />
    <el-table-column prop="uin" label="CNPJ" width="180" />
    <el-table-column prop="address" label="Endereço" width="180" />
  </el-table>

  <!-- Modal -->
  <el-dialog v-model="isModalVisible" title="Cadastrar ONG" :before-close="closeModal">
    <el-input v-model="ong.name" placeholder="Nome" />
    <el-input v-model="ong.uin" placeholder="CNPJ" />
    <el-input v-model="ong.address" placeholder="Endereço" />

    <template #footer>
      <el-button @click="closeModal()">Cancelar</el-button>
      <el-button v-if="ong.id" type="danger" @click="remove()">Remover</el-button>
      <el-button type="primary" :disabled="!canSave" @click="save()">Salvar</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { defaultOng, Ong } from '@/models/ong'
import { computed, ref } from 'vue'
import { getLocation } from '@/utils/google-maps'

//#region List

const ongs = ref<Ong[]>([
  {
    id: 1,
    name: 'Doguinho Feliz',
    uin: '12312312313',
    address: 'Rua Marco Rodrigues, 50',
    lat: 32.123123,
    long: 9.465645,
  },
])

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const ong = ref(defaultOng())
const canSave = computed(
  () => !!ong.value.address && !!ong.value.lat && ong.value.long && !!ong.value.name && !!ong.value.uin,
)

const openModal = (id?: number) => {
  ong.value = ongs.value.find((e) => e.id === id) ?? ong.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  ong.value = defaultOng()
}

const save = async () => {
  const location = await getLocation(ong.value.address)

  ong.value.lat = location.lat()
  ong.value.long = location.lng()

  if (!canSave.value) return

  if (ong.value.id) {
    const index = ongs.value.findIndex((e) => e.id === ong.value.id)

    ongs.value[index] = ong.value
  } else {
    ongs.value.push({ ...ong.value, id: ongs.value.length + 1 })
  }

  closeModal()
}

const remove = () => {
  ongs.value = ongs.value.filter((e) => e.id !== ong.value.id)

  closeModal()
}

//#endregion Modal
</script>

<style scoped lang="scss">
* {
  margin: 1%;
}
</style>

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
import { computed, onMounted, ref } from 'vue'
import { getLocation } from '@/utils/google-maps'
import axios from 'axios'
import { useStore } from 'vuex'

const store = useStore()

//#region List

let ongs = ref<Ong[]>([])

const getAll = () => {
  store.commit('loading/startLoading', 'Listando unidades...')
  axios.get('/ongs').then((resp) => (ongs.value = resp.data))
  store.commit('loading/stopLoading')
}

onMounted(() => getAll())

//#endregion List

//#region Modal

const isModalVisible = ref(false)
const ong = ref(defaultOng())
const canSave = computed(() => !!ong.value.address && !!ong.value.name && !!ong.value.uin)

const openModal = (id?: number) => {
  ong.value = ongs.value.find((e) => e.id === id) ?? ong.value
  isModalVisible.value = true
}

const closeModal = () => {
  isModalVisible.value = false
  ong.value = defaultOng()
}

const save = async () => {
  if (!canSave.value) return

  const location = await getLocation(ong.value.address)

  ong.value.lat = location.lat()
  ong.value.long = location.lng()

  if (ong.value.id) {
    store.commit('loading/startLoading', 'Criando unidade...')
    await axios.put('/ongs', ong.value)
    getAll()
  } else {
    store.commit('loading/startLoading', 'Atualizando unidade...')
    await axios.post('/ongs', ong.value)
    getAll()
  }

  store.commit('loading/stopLoading')

  closeModal()
}

const remove = async () => {
  store.commit('loading/startLoading', 'Removendo unidade...')
  await axios.delete(`/ongs/${ong.value.id}`)
  getAll()

  closeModal()
}

//#endregion Modal
</script>

<style scoped lang="scss">
* {
  margin: 1%;
}
</style>

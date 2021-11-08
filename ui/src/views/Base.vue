<template>
  <el-row>
    <el-col :span="3">
      <ONGSideBar v-if="isOng" />
      <ClientSideBar v-else />
    </el-col>
    <el-col :span="20">
      <router-view />
    </el-col>
  </el-row>
</template>

<script setup lang="ts">
import ONGSideBar from '@/components/ONGSideBar.vue'
import ClientSideBar from '@/components/ClientSideBar.vue'
import { computed } from 'vue'
import { User } from '@/models/user'

const isOng = computed(() => {
  const dataString = localStorage.getItem('user') ?? ''
  const user = JSON.parse(dataString).user as User

  return user.ongId
})
</script>

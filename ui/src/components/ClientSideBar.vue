<template>
  <el-menu
    class="side-bar"
    :router="true"
    :default-active="currentRoute"
    background-color="#202020"
    text-color="#8685EF"
    active-text-color="#F6B162"
  >
    <el-menu-item class="logout">
      <el-button type="primary" size="mini" plain @click="logout()">Logout</el-button>
    </el-menu-item>
    <el-menu-item index="/search">
      <i class="fas fa-search" /> &nbsp;
      <span>Busca</span>
    </el-menu-item>
    <el-menu-item index="/requests">
      <i class="fas fa-list-ul" /> &nbsp;
      <span>Solicitações</span>
    </el-menu-item>
  </el-menu>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const currentRoute = computed<string>(() => router.currentRoute.value.path)

const logout = () => {
  localStorage.removeItem('user')
  router.push('/login')
}
</script>

<style scoped lang="scss">
.side-bar {
  min-width: fit-content;
  height: 100vh;

  span {
    font-size: 1rem;
  }
}

.el-menu-item.logout {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>

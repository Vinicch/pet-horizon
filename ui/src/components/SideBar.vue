<template>
  <el-menu
    class="side-bar"
    :router="true"
    :default-active="currentRoute"
    background-color="#202020"
    text-color="#8685EF"
    active-text-color="#F6B162"
  >
    <el-menu-item index="/">
      <i class="fas fa-columns" /> &nbsp;
      <span>Dashboard</span>
    </el-menu-item>
    <el-menu-item index="/ongs">
      <i class="fas fa-paw" /> &nbsp;
      <span>ONGs</span>
    </el-menu-item>
    <el-menu-item index="/pets">
      <i class="fas fa-dog" /> &nbsp;
      <span>Pets</span>
    </el-menu-item>
    <el-menu-item index="/users">
      <i class="fas fa-user" /> &nbsp;
      <span>Usuários</span>
    </el-menu-item>
  </el-menu>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const currentRoute = computed<string>(() => router.currentRoute.value.path)
</script>

<style scoped lang="scss">
.side-bar {
  min-width: fit-content;
  height: 100vh;

  span {
    font-size: 1rem;
  }
}
</style>

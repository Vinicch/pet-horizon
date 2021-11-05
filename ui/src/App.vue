<template>
  <span
    v-loading.fullscreen.lock="isLoading"
    :element-loading-text="loadingMessage"
    element-loading-background="rgba(0, 0, 0, 0.8)"
  />

  <el-row>
    <el-col :span="3">
      <SideBar />
    </el-col>
    <el-col :span="20">
      <router-view />
    </el-col>
  </el-row>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useStore } from 'vuex'
import SideBar from './components/SideBar.vue'

const store = useStore()
const isLoading = computed<boolean>(() => store.state['loading'].isLoading)
const loadingMessage = computed<string>(() => store.state['loading'].message)
</script>

<style lang="scss">
body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

.hover {
  cursor: pointer;
}
</style>

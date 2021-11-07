<template>
  <Suspense>
    <template #default>
      <el-row>
        <el-col :span="3">
          <SideBar />
        </el-col>
        <el-col :span="20">
          <router-view />
        </el-col>
      </el-row>
    </template>

    <template #fallback>
      <span v-loading.fullscreen.lock="true" element-loading-background="rgba(0, 0, 0, 0.8)" />
    </template>
  </Suspense>
</template>

<script setup lang="ts">
import SideBar from './components/SideBar.vue'
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

<template>
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
import SideBar from '@/components/SideBar.vue'
</script>

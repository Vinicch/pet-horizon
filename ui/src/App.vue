<template>
  <Suspense>
    <template #default>
      <router-view />
    </template>

    <template #fallback>
      <span v-loading.fullscreen.lock="true" element-loading-background="rgba(0, 0, 0, 0.8)" />
    </template>
  </Suspense>
</template>

<style lang="scss">
body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

.hover {
  cursor: pointer;
}
</style>

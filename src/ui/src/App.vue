<script setup lang="ts">
import TodoView from './components/TodoView.vue'
</script>

<template>
  <div class="app-container">
    <TodoView data-cy="todo-view" />
  </div>
</template>

<style>
.app-container {
  max-width: 700px;
  margin: 2rem auto;
  padding: 1rem;
}
</style>
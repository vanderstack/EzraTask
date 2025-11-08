<template>
  <div class="todo-view">
    <h1>EzraTask</h1>

    <AddTodoForm @submit="addTodo" />

    <div v-if="isLoading" data-testid="loading-indicator">Loading...</div>
    <div v-else-if="error" class="error" data-testid="error-message">{{ error }}</div>
    <TodoList v-else :todos="todos" @toggle-completion="toggleCompletion" @archive="archive" />
    <div v-if="!isLoading && !error && todos.length === 0" data-testid="empty-state">
      No todos yet. Add one above!
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import AddTodoForm from './AddTodoForm.vue';
import TodoList from './TodoList.vue';
import { useTodos } from '@/composables/useTodos';

const { todos, isLoading, error, fetchTodos, addTodo, toggleCompletion, archive } = useTodos();

onMounted(() => {
  fetchTodos();
});
</script>

<style scoped>
.todo-view {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
.error {
  color: red;
}
</style>

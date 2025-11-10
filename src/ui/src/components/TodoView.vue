<script setup lang="ts">
import { onMounted } from 'vue';
import AddTodoForm from './AddTodoForm.vue';
import TodoList from './TodoList.vue';
import { useTodos } from '@/composables/useTodos';
import type { Todo } from '@/types';

// By not destructuring, we keep the reactive connection to the composable's state.
const todoState = useTodos();

const handleAddTodo = (newTodoData: { description: string }) => {
  const now = new Date().toISOString();
  const newTodo: Omit<Todo, 'id' | 'isCompleted'> = {
    description: newTodoData.description,
    priority: 'None',
    dueDate: null,
    completedAt: null,
    creationTime: now,
    lastModifiedTime: now,
    rowVersion: '', // The server will generate this
  };
  // Call the method on the reactive object.
  todoState.addTodo(newTodo);
};

onMounted(() => {
  // Call the method on the reactive object.
  todoState.fetchTodos();
});
</script>

<template>
  <div class="todo-view">
    <h1>EzraTask</h1>

    <AddTodoForm @submit="handleAddTodo" />

    <!-- Access all state properties through the reactive 'todoState' object. -->
    <div v-if="todoState.isLoading" data-testid="loading-indicator">Loading...</div>
    <div v-else-if="todoState.error" class="error" data-testid="error-message">{{ todoState.error }}</div>
    <TodoList
      v-else
      :todos="todoState.todos as any"
      @toggle-completion="todoState.toggleCompletion"
      @archive="todoState.archive"
    />
    <div v-if="!todoState.isLoading && !todoState.error && (todoState.todos as any).length === 0" data-testid="empty-state">
      No todos yet. Add one above!
    </div>
  </div>
</template>

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

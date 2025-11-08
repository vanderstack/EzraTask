<template>
  <ul class="todo-list" data-testid="todo-list">
    <TodoItem
      v-for="todo in todos"
      :key="todo.id"
      :todo="todo"
      @toggle-completion="$emit('toggle-completion', $event)"
      @archive="$emit('archive', $event)"
    />
  </ul>
</template>

<script setup lang="ts">
import type { PropType } from 'vue';
import type { Todo } from '@/types';
import TodoItem from './TodoItem.vue';

defineProps({
  todos: {
    type: Array as PropType<Todo[]>,
    required: true,
  },
});

defineEmits(['toggle-completion', 'archive']);
</script>

<style scoped>
.todo-list {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}
</style>
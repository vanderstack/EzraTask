<template>
  <li
    class="todo-item"
    :class="{ completed: todo.isCompleted }"
    data-testid="todo-item"
  >
    <input
      type="checkbox"
      :checked="todo.isCompleted"
      @change="$emit('toggle-completion', todo.id)"
      data-testid="todo-checkbox"
    />
    <span>{{ todo.description }}</span>
    <button @click="$emit('archive', todo.id)" data-testid="archive-button">Archive</button>
  </li>
</template>

<script setup lang="ts">
import type { PropType } from 'vue';
import type { Todo } from '@/types';

defineProps({
  todo: {
    type: Object as PropType<Todo>,
    required: true,
  },
});

defineEmits(['toggle-completion', 'archive']);
</script>

<style scoped>
.todo-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}
.completed span {
  text-decoration: line-through;
  color: #888;
}
span {
  flex-grow: 1;
}
</style>
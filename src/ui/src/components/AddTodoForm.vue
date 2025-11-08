<script setup lang="ts">
import { ref } from 'vue';

const description = ref('');

const emit = defineEmits<{
  (e: 'submit', payload: { description: string }): void;
}>();

const handleSubmit = () => {
  if (description.value.trim()) {
    emit('submit', { description: description.value });
    description.value = '';
  }
};
</script>

<template>
  <form @submit.prevent="handleSubmit" data-testid="add-todo-form">
    <input
      type="text"
      v-model="description"
      data-testid="new-todo-input"
      placeholder="What needs to be done?"
    />
    <button type="submit" data-testid="add-todo-button">Add</button>
  </form>
</template>
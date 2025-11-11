const brokenTodoView = `
<script setup lang="ts">
import { onMounted } from 'vue';
import AddTodoForm from './AddTodoForm.vue';
import TodoList from './TodoList.vue';
import { useTodos } from '@/composables/useTodos';
import type { Todo } from '@/types';

console.log('[OBSERVABILITY] TodoView component initializing...');

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
  console.log('[OBSERVABILITY] TodoView component mounted.');
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
`;

const fixedTodoView = `
<script setup lang="ts">
import { onMounted } from 'vue';
import AddTodoForm from './AddTodoForm.vue';
import TodoList from './TodoList.vue';
import { useTodos } from '@/composables/useTodos';
import type { Todo } from '@/types';

// Destructure the refs and methods from the composable to ensure reactivity.
const { todos, isLoading, error, fetchTodos, addTodo, toggleCompletion, archive } = useTodos();

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
  // Call the destructured method.
  addTodo(newTodo);
};

onMounted(() => {
  // Call the destructured method.
  fetchTodos();
});
</script>

<template>
  <div class="todo-view">
    <h1>EzraTask</h1>

    <AddTodoForm @submit="handleAddTodo" />

    <!-- Bind directly to the reactive refs. -->
    <div v-if="isLoading" data-testid="loading-indicator">Loading...</div>
    <div v-else-if="error" class="error" data-testid="error-message">{{ error }}</div>
    <TodoList
      v-else
      :todos="todos"
      @toggle-completion="toggleCompletion"
      @archive="archive"
    />
    <div v-if="!isLoading && !error && todos.length === 0" data-testid="empty-state">
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
`;

describe('Todo Lifecycle - Silent Crash', () => {
  before(() => {
    // Switch to the "fixed" (destructuring) version of the component
    cy.writeFile('src/ui/src/components/TodoView.vue', fixedTodoView);
  });

  after(() => {
    // Revert to the "broken" (todoState) version of the component
    cy.writeFile('src/ui/src/components/TodoView.vue', brokenTodoView);
  });

  beforeEach(() => {
    cy.resetState();
    cy.visit('/');
  });

  it('should fail to make a GET request on initial load (Silent Crash)', () => {
    // This test is expected to fail with a timeout, confirming the "Silent Crash"
    cy.intercept('GET', '/api/v1/todos*').as('getTodos');
    cy.wait('@getTodos', { timeout: 5000 });
  });
});

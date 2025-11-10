import { ref } from 'vue';
import type { Todo } from '@/types';
import apiClient from '@/services/apiClient';

export function useTodos() {
  const todos = ref<Todo[]>([]);
  const isLoading = ref(false);
  const error = ref<string | null>(null);

  const fetchTodos = async (isArchived: boolean = false) => {
    console.log('[OBSERVABILITY] Fetching todos...');
    isLoading.value = true;
    error.value = null;
    try {
      const response = await apiClient.get('/api/v1/todos', { params: { isArchived } });
      todos.value = response.data.items;
      console.log(`[OBSERVABILITY] Fetched ${todos.value.length} todos.`);
    } catch (e: any) {
      error.value = e.message || 'An unknown error occurred.';
      console.error('[OBSERVABILITY] Error fetching todos:', e);
    } finally {
      isLoading.value = false;
    }
  };

  const addTodo = async (newTodoData: Omit<Todo, 'id' | 'isCompleted'>) => {
    error.value = null;
    try {
      // --- FIX START ---
      // 1. Capture the response from the POST request, which contains the created Todo.
      const response = await apiClient.post<Todo>('/api/v1/todos', newTodoData);
      
      // 2. Instead of re-fetching, add the new item from the response directly to the local state.
      //    This is more efficient and eliminates the race condition.
      todos.value.unshift(response.data);
      // --- FIX END ---
    } catch (e: any) {
      error.value = e.message || 'Failed to add todo.';
    }
  };

  const toggleCompletion = async (id: string | number) => {
    const todo = todos.value.find((t) => t.id === id);
    if (todo) {
      try {
        const response = await apiClient.patch(`/api/v1/todos/${id}/toggle-completion`);
        Object.assign(todo, response.data);
      } catch (e: any) {
        error.value = e.message || 'Failed to toggle completion.';
      }
    }
  };

  const archive = async (id: string | number) => {
    try {
      await apiClient.patch(`/api/v1/todos/${id}/archive`);
      todos.value = todos.value.filter((t) => t.id !== id);
    } catch (e: any) {
      error.value = e.message || 'Failed to archive todo.';
    }
  };

  return {
    todos,
    isLoading,
    error,
    fetchTodos,
    addTodo,
    toggleCompletion,
    archive,
  };
}
import { describe, it, expect } from 'vitest';
import { render } from '@testing-library/vue';
import TodoList from './TodoList.vue';
import type { Todo } from '@/types';

describe('TodoList.vue', () => {
  it('renders a list of todos', () => {
    const todos: Todo[] = [
      {
        id: 1,
        description: 'Test Todo 1',
        isCompleted: false,
        completedAt: null,
        priority: 'Medium',
        dueDate: null,
        creationTime: new Date().toISOString(),
        lastModifiedTime: new Date().toISOString(),
        rowVersion: '1',
      },
      {
        id: 2,
        description: 'Test Todo 2',
        isCompleted: true,
        completedAt: new Date().toISOString(),
        priority: 'High',
        dueDate: null,
        creationTime: new Date().toISOString(),
        lastModifiedTime: new Date().toISOString(),
        rowVersion: '1',
      },
    ];
    const { getAllByTestId } = render(TodoList, {
      props: { todos },
    });

    const todoItems = getAllByTestId('todo-item');
    expect(todoItems).toHaveLength(2);
  });
});

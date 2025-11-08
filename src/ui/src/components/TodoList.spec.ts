import { describe, it, expect } from 'vitest';
import { render } from '@testing-library/vue';
import TodoList from './TodoList.vue';

describe('TodoList.vue', () => {
  it('renders a list of todos', () => {
    const todos = [
      { id: 1, description: 'Todo 1', isComplete: false, isArchived: false },
      { id: 2, description: 'Todo 2', isComplete: true, isArchived: false },
    ];
    const { getAllByTestId } = render(TodoList, {
      props: { todos },
    });

    const todoItems = getAllByTestId('todo-item');
    expect(todoItems).toHaveLength(2);
  });
});

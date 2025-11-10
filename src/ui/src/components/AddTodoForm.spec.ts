import { describe, it, expect } from 'vitest';
import { render, fireEvent } from '@testing-library/vue';
import AddTodoForm from './AddTodoForm.vue';

describe('AddTodoForm.vue', () => {
  it('emits a "submit" event with the new todo description', async () => {
    const { getByTestId, emitted } = render(AddTodoForm);

    const input = getByTestId('new-todo-input');
    const form = getByTestId('add-todo-form');
    const newTodoText = 'A brand new todo';

    await fireEvent.update(input, newTodoText);
    await fireEvent.submit(form);

    // Check if the event was emitted
    const submitEvent = emitted().submit;
    expect(submitEvent).toBeTruthy();

    // Check the payload of the event
    if (submitEvent) {
      expect(submitEvent[0]).toEqual([{ description: newTodoText }]);
    }
  });
});

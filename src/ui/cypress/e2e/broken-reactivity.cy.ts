describe('Todo Lifecycle - Broken Reactivity', () => {
  beforeEach(() => {
    cy.resetState();
    cy.visit('/');
  });

  it('should fail to update the UI when adding a new todo', () => {
    const newItem = 'Learn Test-Driven Development';

    // This test confirms the app initializes, but fails to update the UI.
    // It is expected to PASS the cy.wait commands, but FAIL the final assertion.

    // Wait for initial load
    cy.intercept('GET', '/api/v1/todos*').as('getTodos');
    cy.wait('@getTodos');

    // Intercept the POST request
    cy.intercept('POST', '/api/v1/todos').as('addTodo');

    // Add a new item
    cy.get('[data-testid="new-todo-input"]').type(newItem);
    cy.get('[data-testid="add-todo-button"]').click();

    // Wait for the API call to complete
    cy.wait('@addTodo');

    // This assertion is expected to fail, confirming the reactivity bug
    cy.contains('[data-testid="todo-item"]', newItem).should('be.visible');
  });
});


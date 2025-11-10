describe('Todo Lifecycle', () => {
  beforeEach(() => {
    // This custom command resets the API database before each test.
    cy.resetState();

    // Visit the UI. The baseUrl is now set by CYPRESS_BASE_URL.
    cy.visit('/');
  });

  it('should display the main title', () => {
    cy.contains('h1', 'EzraTask');
  });

  it('should add a new todo, complete it, and then archive it', () => {
    const newItem = 'Learn Test-Driven Development';

    // Intercept the initial GET to ensure the page is ready.
    cy.intercept('GET', '/api/v1/todos*').as('getTodos');
    cy.wait('@getTodos');

    // Intercept the POST request we care about.
    cy.intercept('POST', '/api/v1/todos').as('addTodo');

    // Add a new item
    cy.get('[data-testid="new-todo-input"]').type(newItem);
    cy.get('[data-testid="add-todo-button"]').click();

    // Wait ONLY for the API call to complete.
    cy.wait('@addTodo');

    // Because of our improved app logic, the UI is now guaranteed to be updated.
    cy.contains('[data-testid="todo-item"]', newItem).should('be.visible');

    // Mark the item as complete
    cy.contains('[data-testid="todo-item"]', newItem)
      .find('[data-testid="todo-checkbox"]')
      .click();
    cy.contains('[data-testid="todo-item"]', newItem)
      .should('have.class', 'completed');

    // Archive the item
    cy.contains('[data-testid="todo-item"]', newItem)
      .find('[data-testid="archive-button"]')
      .click();
    cy.contains('[data-testid="todo-item"]', newItem).should('not.exist');
  });
});

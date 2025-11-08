describe('Todo Lifecycle', () => {
  beforeEach(() => {
    // For a real app, we would stub API calls here, but for now,
    // we will test against the actual in-memory API.
    cy.visit('/');
  });

  it('should display the main title', () => {
    cy.contains('h1', 'EzraTask');
  });

  it('should add a new todo, complete it, and then archive it', () => {
    const newItem = 'Learn Test-Driven Development';

    // Add a new item
    cy.get('[data-testid="new-todo-input"]').type(newItem);
    cy.get('[data-testid="add-todo-button"]').click();
    cy.contains('[data-testid="todo-item"]', newItem).should('exist');

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

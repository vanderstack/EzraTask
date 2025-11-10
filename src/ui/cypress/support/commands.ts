// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************

Cypress.Commands.add('login', (username, password) => {
  cy.session([username, password], () => {
    cy.visit('/')
    cy.get('[data-testid="username-input"]').type(username)
    cy.get('[data-testid="password-input"]').type(password)
    cy.get('[data-testid="login-button"]').click()
    cy.get('[data-testid="welcome-message"]').should('be.visible')
  })
})

Cypress.Commands.add('resetState', () => {
  // Use the API URL provided by the environment variable in docker-compose
  const apiUrl = Cypress.env('API_URL');
  cy.request('POST', `${apiUrl}/debug/reset-state`);
});

# Swarm Mission Prompt: Vector B (DOM & Component State)

## 1.0 Governing Mandate: The "Log, Act, Synthesize" Loop

Your workflow is the "Log, Act, Synthesize" loop. You must adhere to this protocol strictly. Your first action must be to read `docs/Planning/swarm-genesis.md` to fully understand the context. The 'Radical Observability Protocol' is in effect.

## 2.0 "Act" Objective: Verify DOM Rendering from State

Your primary and only objective is to **verify the component rendering logic** by writing a new suite of Cypress tests that are completely isolated from the backend API.

Your mission is to use `cy.intercept` to provide mock (stubbed) API responses, creating a controlled environment to test the UI's ability to render state. You are **forbidden** from testing the network layer; you must assume the API does not exist. Your entire world is the rendered DOM.

**Your Plan:**

1.  **Create New Test File:** Create a new test file named `cypress/e2e/dom-state.cy.ts`.
2.  **Write DOM-Level Tests:** Populate this file with new, granular tests for the following scenarios:
    *   **Test B-1:** A test that intercepts `GET /api/v1/todos` and responds with a mock array containing two specific todo items. It should then assert that exactly two `[data-testid="todo-item"]` elements are rendered in the DOM with the correct descriptions.
    *   **Test B-2:** A test that intercepts `GET /api/v1/todos` and responds with an empty array (`[]`). It should then assert that the "empty state" message is visible and that no `[data-testid="todo-item"]` elements exist.
    *   **Test B-3:** A test that intercepts `GET /api/v1/todos` and responds with a single todo object where `isCompleted: true`. It should then assert that the corresponding `[data-testid="todo-item"]` element is rendered and has the `.completed` CSS class.
    *   **Test B-4:** A test that intercepts `GET /api/v1/todos` and responds with an error (e.g., `statusCode: 500`). It should then assert that the error message element is visible in the DOM.

## 3.0 "Synthesize" Protocol

You must produce the following artifacts in the `docs/Planning/` directory:

1.  **`session-B.log`:** All shell command output (`stdout` and `stderr`) must be redirected to this file.
2.  **`retrospective-B.md`:** You must document your key decisions and observations in this file, following the `Decision X.XX` format.
3.  **`context-B.md`:** You must provide a final summary of the mission outcome in this file.

## 4.0 Success Condition

Your session is complete when the new `dom-state.cy.ts` test suite is fully implemented and you have run it to completion, regardless of whether the tests pass or fail. Your deliverable is the test suite itself and the corresponding diagnostic artifacts.

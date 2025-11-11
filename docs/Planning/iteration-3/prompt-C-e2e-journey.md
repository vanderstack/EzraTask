# Swarm Mission Prompt: Vector C (End-to-End User Journey)

## 1.0 Governing Mandate: The "Log, Act, Synthesize" Loop

Your workflow is the "Log, Act, Synthesize" loop. You must adhere to this protocol strictly. Your first action must be to read `docs/Planning/swarm-genesis.md` to fully understand the context. The 'Radical Observability Protocol' is in effect.

## 2.0 "Act" Objective: Verify the Request-Render Loop

Your primary and only objective is to **verify the complete request-response-render loop** for individual user actions by writing a new suite of granular, end-to-end Cypress tests.

Your mission is to test the full "cause and effect" of a user's interaction, from the initial action to the final UI update. You will validate that the network requests and the resulting DOM changes happen correctly in sequence for discrete features.

**Your Plan:**

1.  **Create New Test File:** Create a new test file named `cypress/e2e/e2e-journey.cy.ts`.
2.  **Write Focused E2E Tests:** Populate this file with new, granular tests for the following scenarios:
    *   **Test C-1 (Focus: Add):** A test that focuses *only* on the "add" functionality. It should:
        1.  Wait for the initial `GET` to complete.
        2.  Type a new item description into the input.
        3.  Click the "Add" button.
        4.  Wait for the `POST /api/v1/todos` request to complete.
        5.  Assert that the new todo item now appears in the DOM.
    *   **Test C-2 (Focus: Complete):** A test that focuses *only* on the "complete" functionality. It should:
        1.  Wait for the initial `GET` to complete.
        2.  Find the first todo item in the list.
        3.  Click its "Complete" checkbox.
        4.  Wait for the `PATCH /api/v1/todos/{id}/toggle-completion` request to complete.
        5.  Assert that the item now has the `.completed` CSS class.
    *   **Test C-3 (Focus: Archive):** A test that focuses *only* on the "archive" functionality. It should:
        1.  Wait for the initial `GET` to complete.
        2.  Find the first todo item in the list.
        3.  Click its "Archive" button.
        4.  Wait for the `PATCH /api/v1/todos/{id}/archive` request to complete.
        5.  Assert that the item is no longer present in the DOM.

## 3.0 "Synthesize" Protocol

You must produce the following artifacts in the `docs/Planning/` directory:

1.  **`session-C.log`:** All shell command output (`stdout` and `stderr`) must be redirected to this file.
2.  **`retrospective-C.md`:** You must document your key decisions and observations in this file, following the `Decision X.XX` format.
3.  **`context-C.md`:** You must provide a final summary of the mission outcome in this file.

## 4.0 Success Condition

Your session is complete when the new `e2e-journey.cy.ts` test suite is fully implemented and you have run it to completion, regardless of whether the tests pass or fail. Your deliverable is the test suite itself and the corresponding diagnostic artifacts.

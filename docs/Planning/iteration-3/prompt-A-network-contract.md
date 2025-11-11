# Swarm Mission Prompt: Vector A (Network & API Contract)

## 1.0 Governing Mandate: The "Log, Act, Synthesize" Loop

Your workflow is the "Log, Act, Synthesize" loop. You must adhere to this protocol strictly. Your first action must be to read `docs/Planning/swarm-genesis.md` to fully understand the context. The 'Radical Observability Protocol' is in effect.

## 2.0 "Act" Objective: Verify the API Contract

Your primary and only objective is to **verify the API contract** by writing a new suite of Cypress tests.

Your mission is to treat the UI as a black box and test its communication with the backend API. You are **forbidden** from testing UI component state or DOM rendering beyond what is necessary to trigger a network request. Your entire world is the browser's network tab, which you will interact with via `cy.intercept`.

**Your Plan:**

1.  **Create New Test File:** Create a new test file named `cypress/e2e/network-contract.cy.ts`.
2.  **Write Network-Level Tests:** Populate this file with new, granular tests for the following scenarios:
    *   **Test A-1:** A test that confirms `cy.visit('/')` triggers a `GET /api/v1/todos` request.
    *   **Test A-2:** A test that confirms typing into the input and clicking the "Add" button triggers a `POST /api/v1/todos` request with a JSON body that includes the correct `description`.
    *   **Test A-3:** A test that loads a list with one item, finds that item in the DOM, and confirms that clicking its "Complete" checkbox triggers a `PATCH /api/v1/todos/{id}/toggle-completion` request to the correct URL.
    *   **Test A-4:** A test that loads a list with one item, finds that item in the DOM, and confirms that clicking its "Archive" button triggers a `PATCH /api/v1/todos/{id}/archive` request to the correct URL.

## 3.0 "Synthesize" Protocol

You must produce the following artifacts in the `docs/Planning/` directory:

1.  **`session-A.log`:** All shell command output (`stdout` and `stderr`) must be redirected to this file.
2.  **`retrospective-A.md`:** You must document your key decisions and observations in this file, following the `Decision X.XX` format.
3.  **`context-A.md`:** You must provide a final summary of the mission outcome in this file.

## 4.0 Success Condition

Your session is complete when the new `network-contract.cy.ts` test suite is fully implemented and you have run it to completion, regardless of whether the tests pass or fail. Your deliverable is the test suite itself and the corresponding diagnostic artifacts.

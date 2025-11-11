# Swarm Genesis: TDT-22 Test Remediation

**Objective:** To provide the swarm with a stable, well-documented, and reproducible environment for diagnosing and fixing two critical, interacting bugs in the E2E test suite.

---

## 1.0 Current State of the System

The E2E test environment has been stabilized. The Docker containers build reliably, and the networking and proxy configuration is correct. However, the application contains at least two distinct bugs that prevent the E2E tests from passing.

This repository is now in a state designed to clearly demonstrate both bugs through a dedicated set of Cypress tests.

---

## 2.0 The Two Bugs

### Bug A: The "Broken Reactivity" Bug

*   **Description:** The application initializes correctly and can fetch and display the initial list of todos. However, when a new todo is added, the UI does not automatically update to show the new item, even though the underlying data is being updated correctly.
*   **Demonstrated by:** The `broken-reactivity.cy.ts` test file.
*   **Expected Test Outcome:** When you run the E2E tests, this test file will show **1 passing test** and **1 failing test**. The failing test will be `should fail to update the UI...`, and the error will be an `AssertionError` because the new todo item is never found in the DOM.

### Bug B: The "Silent Crash" Bug

*   **Description:** A proposed fix for the reactivity bug (using a standard Vue 3 destructuring pattern in `TodoView.vue`) causes a more severe bug. The application fails to initialize its JavaScript correctly, resulting in a "silent crash" where no API calls are ever made.
*   **Demonstrated by:** The `silent-crash.cy.ts` test file.
*   **How it Works:** This test file programmatically modifies the `src/ui/src/components/TodoView.vue` file in its `before()` hook to apply the "fix". It then runs a test that is expected to fail because the initial `GET /api/v1/todos` call never happens. The `after()` hook automatically reverts the file to its original state.
*   **Expected Test Outcome:** When you run the E2E tests, this test file will show **1 failing test**. The error will be `CypressError: cy.wait() timed out waiting for the 1st request to the route: 'getTodos'`.

---

## 3.0 Your Mission

Your mission is to design and implement solutions that make **all tests in both `broken-reactivity.cy.ts` and `silent-crash.cy.ts` pass.**

This will require you to:

1.  **Solve the Reactivity Bug:** Find a way to make the UI reactive to changes in the `useTodos` composable without introducing the "Silent Crash".
2.  **Solve the Silent Crash Bug:** Understand why the idiomatic Vue 3 destructuring pattern causes the application to fail to initialize in this specific containerized environment.

You are encouraged to:

*   **Add more tests:** Create new, even more granular tests to help you isolate the problems.
*   **Add observability:** Use `console.log`, `cy.log`, and other tools to understand the application's behavior.
*   **Do not trust the old retrospective:** It contains outdated and misleading information. Trust only the results of the tests you run yourself.

**To run the full diagnostic test suite, execute the following command from the root of the repository:**

```bash
docker compose -f docker-compose.e2e.yml up --build --abort-on-container-exit
```

Good luck.

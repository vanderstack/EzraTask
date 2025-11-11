# 006: SOP for Test-Driven Swarm Orchestration

*   **Status:** Proposed
*   **Date:** 2025-11-10

## Context and Problem Statement

The previous Standard Operating Procedure (SOP) for Agent Swarm Orchestration was based on assigning high-level, abstract goals to different agents (e.g., "Stabilize," "Diagnose," "Rebuild"). This approach proved inadequate for complex, multi-layered software bugs.

The primary challenges were:
1.  **Vague Prompts:** AI agents struggled to translate abstract goals into concrete, effective actions, often leading to repetitive or unproductive debugging loops.
2.  **Difficult Analysis:** The resulting artifacts (raw logs, free-form retrospectives) were unstructured and difficult for human auditors to synthesize into a single, coherent picture of the problem. The signal-to-noise ratio was too low.
3.  **Lack of Durable Artifacts:** The investigation was ephemeral. Discoveries were limited to the context of a single session and had to be manually re-discovered later.

A new strategy is required that guides the swarm into a "pit of success," where their actions naturally produce clear, durable, and machine-readable diagnostic artifacts.

## Decision

We will adopt a new SOP: **Test-Driven Swarm Orchestration**.

The core principle is to shift the swarm's primary objective from *fixing the bug* to *describing the bug with tests*. The primary deliverable of a swarm mission is no longer a fix, but a comprehensive suite of granular, automated, and self-documenting diagnostic tests.

Instead of giving agents abstract goals, we will give them concrete missions to write tests that prove or disprove specific hypotheses about the system's behavior at different layers of the application stack.

## The New Three-Vector Swarm Missions

The default swarm strategy will now be based on the following three test-driven missions:

### 1. Mission A: The "Network & API Contract" Vector

*   **Core Objective:** To write tests that verify the integrity of the application's network communication layer. The agent assigned to this mission treats the UI as a black box and is concerned only with the network requests and responses.
*   **Mandate:** "Your mission is to verify the API contract. You are forbidden from testing UI component state or DOM rendering. Your entire world is the browser's network tab. Create tests for the following scenarios."
*   **Example Test Scenarios:**
    *   A test confirming that visiting the page (`/`) triggers a `GET /api/v1/todos` request.
    *   A test confirming that submitting the "Add" form triggers a `POST /api/v1/todos` request with a correctly structured JSON body.
    *   A test confirming that clicking a "Complete" checkbox triggers a `PATCH /api/v1/todos/{id}/toggle-completion` request to the correct endpoint.
*   **Outcome:** This mission produces a suite of tests that acts as a regression harness for the application's API communication, immediately identifying if the frontend is failing to send the correct requests.

### 2. Mission B: The "DOM & Component State" Vector

*   **Core Objective:** To write tests that verify the integrity of the application's rendering layer, completely isolated from the backend. The agent assigned to this mission will use `cy.intercept` to provide mock (stubbed) API responses.
*   **Mandate:** "Your mission is to verify the component rendering logic. You must stub all API requests to disconnect the frontend from the backend. Your entire world is the rendered DOM. Create tests for the following scenarios."
*   **Example Test Scenarios:**
    *   A test that stubs the `GET /api/v1/todos` response with a specific array of todo objects and asserts that the correct number of `[data-testid="todo-item"]` elements are rendered in the DOM.
    *   A test that stubs the `GET` response with an empty array and asserts that the "empty state" message is visible.
    *   A test that stubs the `GET` response with a todo marked as `isCompleted: true` and asserts that the corresponding DOM element has the `.completed` class.
*   **Outcome:** This mission produces a suite of tests that validates the application's ability to render state correctly, proving that the components work as expected when given perfect, predictable data.

### 3. Mission C: The "End-to-End User Journey" Vector

*   **Core Objective:** To write granular tests that verify the complete request-response-render loop for individual user actions. This vector connects the work of the other two, testing the full "cause and effect" of a user's interaction.
*   **Mandate:** "Your mission is to verify the complete user journey for individual features. You will test the full cycle: a user takes an action, a network request is made, and the UI updates as a result. Create focused tests for the following scenarios."
*   **Example Test Scenarios:**
    *   A test focused *only* on adding an item: it types, clicks "Add", waits for the `POST` to complete, and asserts the new item appears in the list.
    *   A test focused *only* on completing an item: it waits for the initial load, clicks a checkbox, waits for the `PATCH` to complete, and asserts the item's class changes to `.completed`.
    *   A test focused *only* on archiving an item.
*   **Outcome:** This mission produces a suite of tests that pinpoint failures in the application's reactivity. By comparing the results of these tests with the results from Missions A and B, the root cause can be precisely located (e.g., "The network call succeeds (Mission A) and the component can render the state (Mission B), but the end-to-end test fails (Mission C), therefore the bug is in the reactive glue that connects the network response to the component's state.").

## Consequences

This new SOP has the following benefits:
*   **Clarity:** The mission prompts are concrete and actionable for the AI agents.
*   **Automated Diagnostics:** The test suite becomes a powerful, automated diagnostic tool. Analyzing the results is as simple as seeing which tests pass and which fail.
*   **Durable Artifacts:** The tests themselves are durable, valuable artifacts that become part of the project's regression suite, providing long-term value beyond the immediate debugging session.
*   **High-Signal Output:** The final output is not a verbose log of one agent's meandering journey, but a structured, machine-readable test report that clearly communicates the exact state of the system.

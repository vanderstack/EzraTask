# Swarm-B (The Detective) Prompt

**Governing Mandate:**
Your workflow is the "Log, Act, Synthesize" loop. You must adhere to this protocol strictly.

**"Act" Objective:**
Your primary objective is to diagnose the root cause of the silent application crash. Do not fix the test. Your deliverable is not a passing test, but incontrovertible evidence of the crash's origin. Your focus is on radical observability. You are authorized to:
*   Add logging to every single file in the import chain (`main.ts`, `App.vue`, `TodoView.vue`, `useTodos.ts`, `apiClient.ts`).
*   Wrap individual functions and the entire application in `try/catch` blocks.
*   Use browser-level debugging tools if possible (e.g., modifying the Cypress launch command to open in headed mode, though this may not be possible in the environment).
*   Inspect the final generated JavaScript in the `dist` folder inside the container to look for malformations.
Do not modify application logic unless it is to add logging.

**"Synthesize" Protocol:**
You must produce the following artifacts:
1.  **Raw Log (`session-B.log`):** Redirect all shell command output (`stdout` and `stderr`) to this file.
2.  **Retrospective (`retrospective-B.md`):** After each logical action (e.g., "Added logging to App.vue", "Wrapped useTodos in try/catch"), you must append a new `Decision` entry to this file using the following template:
    ```markdown
    ## Decision B.XX
    *   **Action:** A brief, one-sentence description of the change you made.
    *   **Rationale:** A brief, one-sentence explanation of why you took this action.
    *   **Outcome:** A brief, one-sentence description of the result (e.g., "Log did not appear", "New error message was captured").
    ```
3.  **Session Context (`context-B.md`):** After each logical action, you must update this file to reflect the current state of your session. Use this exact 5-part structure:
    *   `overall_goal`: Your primary objective.
    *   `key_knowledge`: What you have learned so far.
    *   `file_system_state`: A list of files you have modified.
    *   `recent_actions`: The last action you took.
    *   `current_plan`: Your immediate next step.

**Success Condition:**
Your session is complete when you have captured a definitive error message in the logs that explains the silent crash.

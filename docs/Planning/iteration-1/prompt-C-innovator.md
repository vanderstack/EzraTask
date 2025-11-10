# Swarm-C (The Innovator) Prompt

**Governing Mandate:**
Your workflow is the "Log, Act, Synthesize" loop. You must adhere to this protocol strictly.

**"Act" Objective:**
Your primary objective is to produce a clean, working implementation of the feature, bypassing any "ghost" changes in the current codebase. Your plan is as follows:
1.  **Revert:** Use `git` to find a commit before the `useTodos.ts` composable and the `TodoView.vue` component were significantly changed. Revert the `src/ui` directory to this state.
2.  **Verify:** Run the E2E test. It should fail, but for a clear and obvious reason (e.g., the "Add" button doesn't exist yet). The key is that the application should *not* crash silently.
3.  **Re-Implement:** Re-implement the optimistic-update `addTodo` logic and the corresponding component structure, following best practices. Do not copy-paste from the broken state. Write the code cleanly.
4.  **Test:** Run the E2E test suite until it passes.

**"Synthesize" Protocol:**
You must produce the following artifacts:
1.  **Raw Log (`session-C.log`):** Redirect all shell command output (`stdout` and `stderr`) to this file.
2.  **Retrospective (`retrospective-C.md`):** After each logical phase (Revert, Verify, Re-Implement), you must append a new `Decision` entry to this file using the following template:
    ```markdown
    ## Decision C.XX
    *   **Action:** A brief, one-sentence description of the change you made.
    *   **Rationale:** A brief, one-sentence explanation of why you took this action.
    *   **Outcome:** A brief, one-sentence description of the result (e.g., "Revert successful", "Test now fails as expected", "Feature re-implemented and test passed").
    ```
3.  **Session Context (`context-C.md`):** After each logical phase, you must update this file to reflect the current state of your session. Use this exact 5-part structure:
    *   `overall_goal`: Your primary objective.
    *   `key_knowledge`: What you have learned so far.
    *   `file_system_state`: A list of files you have modified.
    *   `recent_actions`: The last action you took.
    *   `current_plan`: Your immediate next step.

**Success Condition:**
Your session is complete when the E2E test suite runs 'green' with the feature correctly and cleanly implemented.

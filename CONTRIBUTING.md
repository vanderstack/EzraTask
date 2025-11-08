# Contributing to EzraTask

Thank you for your interest in contributing to EzraTask! This document outlines our development process and guidelines.

## Development Workflow

We use a feature-branch workflow with pull requests to the `internal/remediation` branch.

1.  **Create a Branch:** Start by creating a new branch for your feature or bug fix from the `internal/remediation` branch.
2.  **Develop:** Make your code changes, following the existing patterns and conventions.
3.  **Test:** Run all local tests (`dotnet test`, `npm run test:unit --prefix src/ui`) to ensure your changes haven't introduced regressions.
4.  **Open a Pull Request:** When your changes are ready, open a pull request against the `internal/remediation` branch.

## Pull Requests & Preview Environments

When you open a pull request, our CI/CD system will automatically perform several actions:

1.  **Run CI Checks:** The main CI pipeline will run all tests and security scans against your changes.
2.  **Deploy a Preview Environment:** A separate workflow will deploy your branch to a unique, temporary URL.
3.  **Post a Comment:** A bot will post a comment on your pull request with a direct link to the live preview environment.

This allows reviewers and stakeholders to interactively test your changes in a real environment, leading to faster and higher-quality feedback.

### Cleanup

To manage costs and resources, these preview environments are ephemeral. When your pull request is merged or closed, a cleanup workflow will automatically run to delete all associated Azure resources, including the res
ource group and the container image from the registry.

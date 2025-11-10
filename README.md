# EzraTask

A production-grade, full-stack to-do application built to showcase best practices in software engineering, from architecture to developer experience.

## The Premise: A Tale of Unexpected Success

This project was approached as a real-world product simulation. It tells the story of "EzraTask," a simple internal tool built for a fast-growing startup.

**The "Internal POC"** began by fulfilling the core request for a simple to-do app. **The "Product-Market Fit" Moment** imagines the tool's popularity exploding, creating an urgent need to evolve it into the **Production-Grade Application** you see here. This narrative is the "why" behind every architectural decision and feature.

## Features

### Functional
*   **Create, View, and Complete Todos:** Core task management functionality.
*   **Rich Data Models:** Todos can be assigned a **Priority** (`None`, `Low`, `Medium`, `High`) and an optional **Due Date**.
*   **Soft Delete (Archiving):** Tasks can be archived to de-clutter the main view instead of being permanently deleted.
*   **View Archived Tasks:** Users can toggle between viewing active and archived tasks.

### Engineering & Non-Functional
*   **Clean Architecture:** A simple, effective Service Layer pattern in the .NET API avoids over-engineering.
*   **Robust Validation:** DTO-based validation and global exception handling in the backend ensure data integrity.
*   **Polished UX:** The frontend handles loading, error, and empty states to provide a smooth user experience.
*   **Comprehensive Testing:** The project includes unit tests (xUnit for backend, Vitest for frontend) and a full end-to-end test suite (Cypress) that validates the entire user lifecycle.
*   **One-Command Setup:** The entire development environment can be launched with a single `docker-compose up` command.

## Local Development

This project is fully containerized for a seamless local development experience. The only prerequisite is to have Docker installed.

### One-Command Setup

To get the entire full-stack application running locally, simply run the following command from the repository root:

```bash
docker-compose up --build
```

This will:
1.  Build the Docker images for both the frontend and backend services.
2.  Start the containers.
3.  The frontend will be available at `http://localhost:5173`.
4.  The backend API will be available at `http://localhost:8080`.

## Running Tests

To ensure consistency with the CI environment and produce reliable results, all tests should be run within their respective Docker containers.

### Backend Tests (xUnit)

```bash
docker-compose run --rm api dotnet test
```

### Frontend Unit Tests (Vitest)

```bash
docker-compose run --rm ui npm run _test:unit:local
```

### End-to-End Tests (Cypress)

This command uses a dedicated, self-contained environment to run the full E2E test suite against production-like builds of the frontend and backend. This is the definitive way to validate the application.

```bash
docker-compose -f docker-compose.e2e.yml up --build --abort-on-container-exit
```

## Architecture

The application follows a classic, scalable client-server architecture. For a detailed breakdown of the technology stack, components, and guiding principles, please see the **Architectural Decision Records**.

### Architectural Decision Records (ADRs)

This project uses Architectural Decision Records (ADRs) to document significant architectural choices, their context, and their consequences.

You can find all ADRs in the `/docs/adr` directory.

## Contributing

Please see our [**Contribution Guide**](./CONTRIBUTING.md) for details on our development process and code review standards.

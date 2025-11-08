# 3. Use of In-Memory Database for POC/MVP

*   **Status:** Accepted
*   **Date:** 2025-11-4

## Context

The take-home assessment explicitly allows for the use of "SQL Lite or EF Core in memory." The "EzraTask" project narrative begins with a rapidly developed internal Proof of Concept (POC) where speed of delivery and a 
frictionless developer setup are the highest priorities. A decision must be made on the initial data storage mechanism that best serves this narrative.

## Decision

We will use the **EF Core In-Memory Database provider** for the initial development phases (POC and MVP) of the application. This is a deliberate, strategic decision to prioritize development velocity and is formally a
cknowledged as **intentional technical debt**.

## Consequences

### Positive:

*   **Maximum Development Velocity:** Requires zero setup, configuration, or external dependencies. A developer can clone the repository and run the application instantly.
*   **Frictionless Testing:** Unit and integration tests can run at maximum speed without the overhead of managing a test database container or service.
*   **Fulfills Assessment Requirements:** Directly satisfies one of the options explicitly permitted in the assessment prompt.
*   **Narrative Alignment:** Perfectly aligns with the story of building a quick internal POC where persistence is not an initial concern.

### Negative:

*   **Non-Persistent Data:** All data is lost when the application restarts.
*   **Not Scalable:** The data is tied to a single application instance, making it unsuitable for a multi-instance, horizontally-scaled production environment.
*   **Technical Debt:** This choice is not viable for the "Production-Grade" phase of the application's story and must be remediated.

### Mitigation and Remediation Plan

This technical debt will be formally addressed in a future story (`TDT-21: Document Data Persistence and Migration Strategy`). That story will outline the plan to migrate to a persistent, production-grade database like
 PostgreSQL on Azure, including the introduction of a formal data access layer and database migration scripts.

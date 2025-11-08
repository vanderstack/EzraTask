# 2. Choice of Azure Container Apps for Hosting

*   **Status:** Accepted
*   **Date:** 2025-11-4

## Context

The application is containerized using Docker and requires a hosting platform on Microsoft Azure. We need a solution that is simple to manage, cost-effective for a small-to-medium scale application, and provides modern
 features like scale-to-zero and easy integration with CI/CD.

### Options Considered:

1.  **Azure Virtual Machines (VMs):** Traditional IaaS. Provides full control but requires manual setup, patching, and management of the OS and container runtime.
2.  **Azure Kubernetes Service (AKS):** A powerful, full-featured container orchestrator. Extremely scalable but introduces significant operational complexity for a single-service application.
3.  **Azure Container Apps (ACA):** A serverless container platform built on Kubernetes. It abstracts away the complexity of the underlying orchestrator while providing features like scaling, custom domains, and manage
d ingress.

## Decision

We will use **Azure Container Apps (ACA)** as the primary hosting platform for this application.

## Consequences

### Positive:

*   **Simplified Operations:** ACA manages the underlying infrastructure, eliminating the need for us to patch servers or manage a Kubernetes cluster.
*   **Cost-Effectiveness (FinOps):** ACA offers a serverless consumption plan with a generous free tier and the ability to scale to zero, meaning we only pay when the application is actively processing requests. This i
s ideal for applications with variable traffic.
*   **Modern Feature Set:** Provides built-in features like HTTPS ingress, custom domains, and integration with other Azure services out of the box.
*   **Excellent CI/CD Integration:** Has first-party GitHub Actions for seamless, automated deployments.

### Negative:

*   **Less Control than AKS:** We do not have access to the underlying Kubernetes API, which limits advanced customization. This is an acceptable trade-off for this project's scope.

### Simplified Cost Model (Illustrative)

This model compares running our container 24/7 on ACA vs. a minimal Azure VM.

*   **Azure Container Apps (Consumption Plan):**
    *   Assumes 1 vCPU, 2 GiB Memory.
    *   Idle Time (scale-to-zero): **~ $0.00**
    *   Active Processing Time (e.g., 2 hours/day): The cost is calculated per second of usage. For a low-traffic application, this would likely fall within the monthly free grant (180,000 vCPU-seconds, 360,000 GiB-sec
onds).
    *   **Estimated Monthly Cost:** **$0 - $5** (extremely low for a small app).

*   **Azure VM (e.g., B1s General Purpose):**
    *   1 vCPU, 1 GiB Memory.
    *   Must run 24/7 to be available.
    *   **Estimated Monthly Cost (Pay-as-you-go):** **~ $10 - $15**

**Conclusion:** For this project, ACA is significantly more cost-effective due to its serverless, consumption-based pricing model.

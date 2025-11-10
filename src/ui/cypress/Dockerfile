# Use a Debian-based Node.js image for binary compatibility with Cypress
# See: https://on.cypress.io/required-dependencies
FROM node:20-bookworm

# Install the full suite of Cypress dependencies for Debian/Ubuntu
RUN apt-get update && apt-get install -y \
    libgtk2.0-0 libgtk-3-0 libgbm-dev libnotify-dev libgconf-2-4 \
    libnss3 libxss1 libasound2 libxtst6 xauth xvfb \
    --no-install-recommends \
    && rm -rf /var/lib/apt/lists/*

# Set working directory inside the UI source folder
WORKDIR /app/src/ui

# Copy package files and install dependencies
COPY src/ui/package.json src/ui/package-lock.json* ./
COPY src/ui/tsconfig.json ./
COPY src/ui/tsconfig.app.json ./
COPY src/ui/tsconfig.node.json ./
COPY src/ui/tsconfig.vitest.json ./
RUN npm ci

# Copy the Cypress configuration and test files
COPY src/ui/cypress.config.ts .
COPY src/ui/cypress ./cypress

# The entrypoint is simply running cypress.
# The actual command will be provided by docker-compose.
ENTRYPOINT ["npx", "cypress", "run"]

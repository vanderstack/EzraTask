# Stage 1: Build the Vue.js frontend
FROM public.ecr.aws/docker/library/node:20-alpine AS build-ui
WORKDIR /app
COPY src/ui/package*.json ./
RUN npm ci
COPY src/ui/ ./
# Add an argument to receive the API URL at build time
ARG VITE_API_BASE_URL
# Set it as an environment variable so the build process can see it
ENV VITE_API_BASE_URL=${VITE_API_BASE_URL}
RUN echo "VITE_API_BASE_URL is set to: $VITE_API_BASE_URL" && npm run build

# Stage 2: Create a self-contained UI server for E2E tests using Vite Preview
FROM public.ecr.aws/docker/library/node:20-alpine AS ui-e2e
RUN apk add curl
WORKDIR /app
# Copy only what's needed to run the preview server
COPY src/ui/package*.json ./
COPY src/ui/vite.config.ts ./
# Install production dependencies + vite itself
RUN npm ci --omit=dev && npm install vite
# Copy the built static assets from the build stage
COPY --from=build-ui /app/dist ./dist
EXPOSE 5173
# The command to run the production preview server
CMD ["npx", "vite", "preview", "--host", "--port", "5173"]

# Stage 3: Build and publish the backend (for production)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src
COPY ["EzraTask.sln", "./"]
COPY ["src/api/EzraTask.Api.csproj", "src/api/"]
COPY ["tests/EzraTask.Api.Tests/EzraTask.Api.Tests.csproj", "tests/EzraTask.Api.Tests/"]
RUN dotnet restore "EzraTask.sln"
COPY src ./src
COPY tests ./tests
WORKDIR "/src"
RUN dotnet publish "src/api/EzraTask.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR "/src/api"

# Stage 4: Build and publish the backend (for E2E tests)
FROM build-api AS build-api-e2e
WORKDIR "/src"
# This stage builds in Debug mode to include the /debug/reset-state endpoint
RUN dotnet publish "src/api/EzraTask.Api.csproj" -c Debug -o /app/publish-e2e /p:UseAppHost=false
WORKDIR "/src/api"

# Stage 5: Create the final production image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
COPY --from=build-api --chown=appuser:appgroup /app/publish .
COPY --from=build-ui --chown=appuser:appgroup /app/dist ./wwwroot
USER appuser
ENTRYPOINT ["dotnet", "EzraTask.Api.dll"]

# Stage 6: Create the final E2E test image for the API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS api-e2e
RUN apt-get update && apt-get install -y curl
WORKDIR /app
EXPOSE 8080
COPY --from=build-api-e2e /app/publish-e2e .
ENTRYPOINT ["dotnet", "EzraTask.Api.dll"]

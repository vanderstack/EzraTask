# Stage 1: Build the Vue.js frontend
FROM node:20-alpine@sha256:02c342a17c5b4b1a1170b22896f5b080517f308a385157a4e58b16f1250100f7 AS build-ui
WORKDIR /app
COPY src/ui/package*.json ./
RUN npm ci
COPY src/ui/ ./
RUN npm run build

# Stage 2: Build and publish the backend
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:511e0b1b52c3c13e51510a78964036e492fb860533317e076a6b8b0e79143642 AS build-api
WORKDIR /src

# Copy project files and restore dependencies to leverage layer caching
COPY ["EzraTask.sln", "./"]
COPY ["src/api/EzraTask.Api.csproj", "src/api/"]
RUN dotnet restore "EzraTask.sln"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/api"
RUN dotnet publish "EzraTask.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final production image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:7314725345751b75240d998185a8a1836173b1e33c46a6f68e986a7071c7b8d4 AS final
WORKDIR /app
EXPOSE 8080

# Create a non-root user for security
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Copy backend build output from the 'publish' stage and set ownership
COPY --from=build-api --chown=appuser:appgroup /app/publish .

# Copy frontend build output from the 'build-ui' stage to the wwwroot folder and set ownership
COPY --from=build-ui --chown=appuser:appgroup /app/dist ./wwwroot

# Switch to the non-root user
USER appuser

ENTRYPOINT ["dotnet", "EzraTask.Api.dll"]

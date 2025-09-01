# ──────────────────────────────────────────────────────────
#  Multi-Stage Dockerfile – Phone Directory API (.NET 8)
# ──────────────────────────────────────────────────────────
# 1️⃣ Stage: Build
# ---------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie a solução e restaure dependências 
COPY PhoneDirectoryAPI.sln ./
COPY src ./src
RUN dotnet restore "src/PhoneDirectory.API/PhoneDirectory.API.csproj"

# Compile em Release
RUN dotnet publish "src/PhoneDirectory.API/PhoneDirectory.API.csproj" \
    -c Release -o /app/publish --no-restore

# 2️⃣ Stage: Runtime
# ---------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copie artefatos da fase de build
COPY --from=build /app/publish .

# Defina variáveis de ambiente (override no docker run)
ENV ASPNETCORE_URLS=http://+:80 \
    ASPNETCORE_ENVIRONMENT=Production

# Exponha porta HTTP padrão
EXPOSE 80

# Ponto de entrada
ENTRYPOINT ["dotnet", "PhoneDirectory.API.dll"]
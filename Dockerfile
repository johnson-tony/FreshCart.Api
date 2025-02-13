# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy the project file and restore dependencies
COPY ["FreshCart.Api.csproj", "./"]
RUN dotnet restore "FreshCart.Api.csproj"

# Copy the rest of the code
COPY . .
# Build the project
RUN dotnet build "FreshCart.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "FreshCart.Api.csproj" -c Release -o /app/publish

# Final stage: create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreshCart.Api.dll"]

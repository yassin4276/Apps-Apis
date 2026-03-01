# -------------------- BUILD STAGE --------------------
    FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
    WORKDIR /src
    
    COPY Apps-Apis.csproj .
    RUN dotnet restore
    
    COPY . .
    RUN dotnet publish -c Release -o /app/publish
    
    # -------------------- RUNTIME STAGE --------------------
    FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
    WORKDIR /app
    
    # Copy published output
    COPY --from=build /app/publish .
    
    # Expose container port
    EXPOSE 8080
    
    # Environment configuration
    ENV ASPNETCORE_URLS=http://+:8080
    ENV ASPNETCORE_ENVIRONMENT=Production
    
    ENTRYPOINT ["dotnet", "Apps-Apis.dll"]
# Build stage with dotnet sdk to build the code.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /source

# Restore layer
COPY *.sln .
COPY MinGlass.API/*.csproj ./MinGlass.API/
COPY MinGlass.Models/*.csproj ./MinGlass.Models/
COPY MinGlass.Repository/*.csproj ./MinGlass.Repository/
COPY MinGlass.Repository.Context/*.csproj ./MinGlass.Repository.Context/
RUN dotnet restore

# Build and publish layer
COPY MinGlass.API/. ./MinGlass.API/
COPY MinGlass.Models/. ./MinGlass.Models/
COPY MinGlass.Repository/. ./MinGlass.Repository/
COPY MinGlass.Repository.Context/. ./MinGlass.Repository.Context/
RUN dotnet publish -c Release -o publish

# Runtime stage with dotnet runtime to run the build.
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /source/publish .
EXPOSE 80
ENTRYPOINT [ "dotnet", "MinGlass.API.dll" ]
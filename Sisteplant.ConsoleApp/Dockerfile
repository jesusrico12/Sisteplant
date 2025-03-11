# 1. Use an official .NET image for building and running the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# 2. Copy the solution and project files
COPY ./src/*.sln ./  # Copy the solution file
COPY ./src/Sisteplant.Domain/*.csproj ./Sisteplant.Domain/
COPY ./src/Sisteplant.Application/*.csproj ./Sisteplant.Application/
COPY ./src/Sisteplant.Infrastructure/*.csproj ./Sisteplant.Infrastructure/
COPY ./src/Sisteplant.ConsoleApp/*.csproj ./Sisteplant.ConsoleApp/

# 3. Restore the dependencies (cache layer)
RUN dotnet restore ./Sisteplant.ConsoleApp/Sisteplant.ConsoleApp.csproj

# 4. Copy the rest of the source code
COPY ./src/Sisteplant.Domain/ ./Sisteplant.Domain/
COPY ./src/Sisteplant.Application/ ./Sisteplant.Application/
COPY ./src/Sisteplant.Infrastructure/ ./Sisteplant.Infrastructure/
COPY ./src/Sisteplant.ConsoleApp/ ./Sisteplant.ConsoleApp/

# 5. Publish the application (optimized build for production)
RUN dotnet publish ./Sisteplant.ConsoleApp/Sisteplant.ConsoleApp.csproj -c Release -o out

# 6. Runtime image. Use a lighter image just for running (without SDK)
FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# 7. Define the entry point
ENTRYPOINT ["dotnet", "Sisteplant.ConsoleApp.dll"]
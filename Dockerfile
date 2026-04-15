# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy all projects
COPY ["QuantityMeasurementApp.WebApi/QuantityMeasurementApp.WebApi.csproj", "QuantityMeasurementApp.WebApi/"]
COPY ["QuantityMeasurementApp.Repository/QuantityMeasurementApp.Repository.csproj", "QuantityMeasurementApp.Repository/"]
COPY ["QuantityMeasurementApp.Entity/QuantityMeasurementApp.Entity.csproj", "QuantityMeasurementApp.Entity/"]
COPY ["QuantityMeasurementApp.Service/QuantityMeasurementApp.Service.csproj", "QuantityMeasurementApp.Service/"]
COPY ["QuantityMeasurementApp.Controller/QuantityMeasurementApp.Controller.csproj", "QuantityMeasurementApp.Controller/"]

# Restore dependencies
RUN dotnet restore "QuantityMeasurementApp.WebApi/QuantityMeasurementApp.WebApi.csproj"

# Copy the rest of the code
COPY . .

# Build and publish
WORKDIR "/src/QuantityMeasurementApp.WebApi"
RUN dotnet publish "QuantityMeasurementApp.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port Render uses
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "QuantityMeasurementApp.WebApi.dll"]

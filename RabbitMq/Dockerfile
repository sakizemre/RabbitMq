FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitMq/RabbitMq.csproj", "RabbitMq/"]
COPY ["RabbitMq.Application/RabbitMq.Application.csproj", "RabbitMq.Application/"]
COPY ["RabbitMq.Domain/RabbitMq.Domain.csproj", "RabbitMq.Domain/"]
COPY ["RabbitMq.Infrastructure/RabbitMq.Infrastructure.csproj", "RabbitMq.Infrastructure/"]
RUN dotnet restore "RabbitMq/RabbitMq.csproj"
COPY . .
WORKDIR "/src/RabbitMq"
RUN dotnet build "RabbitMq.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMq.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMq.dll"]
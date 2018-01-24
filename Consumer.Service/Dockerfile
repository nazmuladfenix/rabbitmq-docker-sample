FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Consumer.Service/Consumer.Service.csproj Consumer.Service/
COPY RabbitMQ.Shared/RabbitMQ.Shared.csproj RabbitMQ.Shared/RabbitMQ.Shared/
COPY RabbitMQ.Domain/RabbitMQ.Domain.csproj RabbitMQ.Domain/
RUN dotnet restore
COPY . .
WORKDIR /src/Consumer.Service
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Consumer.Service.dll"]

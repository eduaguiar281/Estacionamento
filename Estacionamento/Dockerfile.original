FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Estacionamento/Estacionamento.csproj", "Estacionamento/"]
RUN dotnet restore "Estacionamento/Estacionamento.csproj"
COPY . .
WORKDIR "/src/Estacionamento"
RUN dotnet build "Estacionamento.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Estacionamento.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Estacionamento.dll"]
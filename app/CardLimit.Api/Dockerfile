#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["app/CardLimit.Api/CardLimit.Api.csproj", "app/CardLimit.Api/"]
RUN dotnet restore "app/CardLimit.Api/CardLimit.Api.csproj"
COPY . .
WORKDIR "/src/app/CardLimit.Api"
RUN dotnet build "CardLimit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CardLimit.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardLimit.Api.dll"]
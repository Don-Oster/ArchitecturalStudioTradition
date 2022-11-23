#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ArchitecturalStudioTradition.FileStorage.WebApi/ArchitecturalStudioTradition.FileStorage.WebApi.csproj", "ArchitecturalStudioTradition.FileStorage.WebApi/"]
RUN dotnet restore "ArchitecturalStudioTradition.FileStorage.WebApi/ArchitecturalStudioTradition.FileStorage.WebApi.csproj"
COPY . .
WORKDIR "/src/ArchitecturalStudioTradition.FileStorage.WebApi"
RUN dotnet build "ArchitecturalStudioTradition.FileStorage.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchitecturalStudioTradition.FileStorage.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchitecturalStudioTradition.FileStorage.WebApi.dll"]
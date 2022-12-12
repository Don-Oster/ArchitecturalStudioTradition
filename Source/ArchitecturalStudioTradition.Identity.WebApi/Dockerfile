#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ArchitecturalStudioTradition.Identity.WebApi/ArchitecturalStudioTradition.Identity.WebApi.csproj", "ArchitecturalStudioTradition.Identity.WebApi/"]
RUN dotnet restore "ArchitecturalStudioTradition.Identity.WebApi/ArchitecturalStudioTradition.Identity.WebApi.csproj"
COPY . .
WORKDIR "/src/ArchitecturalStudioTradition.Identity.WebApi"
RUN dotnet build "ArchitecturalStudioTradition.Identity.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchitecturalStudioTradition.Identity.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchitecturalStudioTradition.Identity.WebApi.dll"]
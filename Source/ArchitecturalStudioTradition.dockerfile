####################################
###### BUILD CONTEXT ######
####################################
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY [".", "."]

####################################
###### BUILD ######
####################################
FROM src as build
RUN --mount=type=cache,target=/root/.nuget dotnet build "ArchitecturalStudioTradition.sln" -c Release

####################################
###### TEST ######
####################################
FROM build as publish
RUN --mount=type=cache,target=/root/.nuget dotnet test -c Release --no-build --collect:"Code Coverage"

####################################
###### PUBLISH ######
####################################
RUN --mount=type=cache,target=/root/.nuget dotnet publish "ArchitecturalStudioTradition.WebApi/ArchitecturalStudioTradition.WebApi.csproj" -c Release -o /publish/ArchitecturalStudioTradition.WebApi --no-build
RUN --mount=type=cache,target=/root/.nuget dotnet publish "ArchitecturalStudioTradition.FileStorage.WebApi/ArchitecturalStudioTradition.FileStorage.WebApi.csproj" -c Release -o /publish/ArchitecturalStudioTradition.FileStorage.WebApi --no-build

####################################
###### BUILD ArchitecturalStudioTradition.WebApi RUNTIME IMAGE ######
####################################
FROM base as ArchitecturalStudioTradition.WebApi
LABEL maintainer "dev@archtradition.com"
WORKDIR /app
COPY --from=publish /publish/ArchitecturalStudioTradition.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "ArchitecturalStudioTradition.WebApi.dll"]

####################################
###### BUILD ArchitecturalStudioTradition.FileStorage.WebApi RUNTIME IMAGE ######
####################################
FROM base as ArchitecturalStudioTradition.FileStorage.WebApi
LABEL maintainer "dev@archtradition.com"
WORKDIR /app
COPY --from=publish /publish/ArchitecturalStudioTradition.FileStorage.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "ArchitecturalStudioTradition.FileStorage.WebApi.dll"]

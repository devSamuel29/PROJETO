FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

COPY PROJETO.sln .
COPY PROJETO.Api/*.csproj PROJETO.Api/
COPY PROJETO.Infra/*.csproj PROJETO.Infra/
COPY PROJETO.Domain/*.csproj PROJETO.Domain/
COPY PROJETO.Test/*.csproj PROJETO.Test/

RUN dotnet restore

COPY . .
WORKDIR /source

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PROJETO.Api.dll"]

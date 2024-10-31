FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/ChatApp.Api/ChatApp.Api.csproj", "ChatApp.Api/"]
RUN dotnet restore "ChatApp.Api/ChatApp.Api.csproj"

COPY src/ ./

WORKDIR "/src/ChatApp.Api"
RUN dotnet build "ChatApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApp.Api.dll"]

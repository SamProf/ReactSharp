FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReactSharp.Demo.ServerApp/ReactSharp.Demo.ServerApp.csproj", "ReactSharp.Demo.ServerApp/"]
COPY ["ReactSharp.Blazor/ReactSharp.Blazor.csproj", "ReactSharp.Blazor/"]
COPY ["ReactSharp/ReactSharp.csproj", "ReactSharp/"]
COPY ["ReactSharp.Demo/ReactSharp.Demo.csproj", "ReactSharp.Demo/"]
RUN dotnet restore "ReactSharp.Demo.ServerApp/ReactSharp.Demo.ServerApp.csproj"
COPY . .
WORKDIR "/src/ReactSharp.Demo.ServerApp"
RUN dotnet build "ReactSharp.Demo.ServerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReactSharp.Demo.ServerApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReactSharp.Demo.ServerApp.dll"]

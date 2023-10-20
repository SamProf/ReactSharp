FROM mcr.microsoft.com/dotnet/core/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS build
WORKDIR /src
COPY ["ReactSharp.Blazor.Demo.ServerApp/ReactSharp.Blazor.Demo.ServerApp.csproj", "ReactSharp.Blazor.Demo.ServerApp/"]
COPY ["ReactSharp.Blazor/ReactSharp.Blazor.csproj", "ReactSharp.Blazor/"]
COPY ["ReactSharp/ReactSharp.csproj", "ReactSharp/"]
COPY ["ReactSharp.Demo/ReactSharp.Demo.csproj", "ReactSharp.Demo/"]
RUN dotnet restore "ReactSharp.Blazor.Demo.ServerApp/ReactSharp.Blazor.Demo.ServerApp.csproj"
COPY . .
WORKDIR "/src/ReactSharp.Blazor.Demo.ServerApp"
RUN dotnet build "ReactSharp.Blazor.Demo.ServerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReactSharp.Blazor.Demo.ServerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReactSharp.Blazor.Demo.ServerApp.dll"]

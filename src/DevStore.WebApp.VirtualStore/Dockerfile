FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DevStore.WebApp.VirtualStore/DevStore.WebApp.VirtualStore.csproj", "src/DevStore.WebApp.VirtualStore/"]
COPY ["src/DevStore.Catalog.Application/DevStore.Catalog.Application.csproj", "src/DevStore.Catalog.Application/"]
COPY ["src/DevStore.Catalog.Data/DevStore.Catalog.Data.csproj", "src/DevStore.Catalog.Data/"]
COPY ["src/DevStore.Catalog.Domain/DevStore.Catalog.Domain.csproj", "src/DevStore.Catalog.Domain/"]
COPY ["src/DevStore.Common/DevStore.Common.csproj", "src/DevStore.Common/"]
COPY ["src/DevStore.Payment.AntiCorruption/DevStore.Payment.AntiCorruption.csproj", "src/DevStore.Payment.AntiCorruption/"]
COPY ["src/DevStore.Payment.Business/DevStore.Payment.Business.csproj", "src/DevStore.Payment.Business/"]
COPY ["src/DevStore.Payment.Data/DevStore.Payment.Data.csproj", "src/DevStore.Payment.Data/"]
COPY ["src/DevStore.Sales.Application/DevStore.Sales.Application.csproj", "src/DevStore.Sales.Application/"]
COPY ["src/DevStore.Sales.Domain/DevStore.Sales.Domain.csproj", "src/DevStore.Sales.Domain/"]
COPY ["src/DevStore.Sales.Data/DevStore.Sales.Data.csproj", "src/DevStore.Sales.Data/"]
COPY ["src/EventSourcing/EventSourcing.csproj", "src/EventSourcing/"]
RUN dotnet restore "./src/DevStore.WebApp.VirtualStore/DevStore.WebApp.VirtualStore.csproj"
COPY ./src .
WORKDIR "/src/DevStore.WebApp.VirtualStore"
RUN dotnet build "./DevStore.WebApp.VirtualStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevStore.WebApp.VirtualStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV LC_ALL en_US.UTF-8
ENTRYPOINT ["dotnet", "DevStore.WebApp.VirtualStore.dll"]
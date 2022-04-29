#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DigitalThreadServer.csproj", "."]
RUN dotnet restore "./DigitalThreadServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DigitalThreadServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigitalThreadServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalThreadServer.dll"]


# az acr login --name iobtassets
# docker build -t dts -f Dockerfile .
# docker tag dts iobtassets.azurecr.io/digital-thread-server:v1.0.2
# docker push iobtassets.azurecr.io/digital-thread-server:v1.0.2

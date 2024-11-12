#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM registry.access.redhat.com/ubi8/dotnet-80-runtime AS base
WORKDIR /app
USER 0
EXPOSE 8080

FROM registry.access.redhat.com/ubi8/dotnet-80 AS build
USER 0
WORKDIR /src
COPY . .
RUN dotnet restore "./Tasks/Notification/Notification.Worker.csproj"
WORKDIR "/src/Tasks/Notification"
RUN dotnet build "Notification.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["Tasks/Notification/appsettings.*", "."]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sisfv.Notification.Worker.dll"]
USER 1001


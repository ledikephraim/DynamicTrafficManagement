#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM devdotnetorg/libgpiod:1.6.3 AS sourcelibgpiod

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim-arm32v7 AS base
WORKDIR /app
ARG ARCH=arm32v7/
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrafficLightUnitClient.csproj", ""]
RUN dotnet restore "./TrafficLightUnitClient.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TrafficLightUnitClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrafficLightUnitClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Get libgpiod
COPY --from=sourcelibgpiod /artifacts.zip /

# Add Libgpiod
RUN apt-get update \
	&& apt-get -y install zip \
	&& unzip -o /artifacts.zip -d / \
	&& apt-get -y remove zip 

ENTRYPOINT ["dotnet", "TrafficLightUnitClient.dll"]
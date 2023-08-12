#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


COPY /src/*/*.csproj ./
RUN find . -type f
RUN for file in $(ls *.csproj); do \
	echo $file \
	&& mkdir -p ${file%.*}/ \
	&& mv $file ${file%.*}/; \
	done

RUN dotnet restore "./Geometry.Api/Geometry.Api.csproj"
COPY ./src .
WORKDIR "/src/Geometry.Api/."
RUN dotnet build "Geometry.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geometry.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geometry.Api.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ./src/Users.Domain/Users.Domain.csproj ./src/Users.Domain/
COPY ./src/Users.Application/Users.Application.csproj ./src/Users.Application/
COPY ./src/Users.Infrastructure/Users.Infrastructure.csproj ./src/Users.Infrastructure/
COPY ./src/Users.Api/Users.Api.csproj ./src/Users.Api/
RUN dotnet restore ./src/Users.Api/Users.Api.csproj


COPY ./src ./src
RUN dotnet publish ./src/Users.Api/Users.Api.csproj -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Users.Api.dll"]
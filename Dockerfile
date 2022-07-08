FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Build Project
COPY . ./

RUN dotnet restore L3Projet.sln --ignore-failed-sources /p:EnableDefaultItems=false
RUN dotnet publish L3Projet.sln --no-restore -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

RUN apt-get update
RUN apt-get install -y curl
HEALTHCHECK --interval=2m --timeout=3s CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["dotnet", "L3Projet.WebAPI.dll"]
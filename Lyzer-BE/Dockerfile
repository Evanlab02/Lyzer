FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

COPY Lyzer-BE.csproj /src/Lyzer-BE.csproj
COPY Program.cs /src/Program.cs
COPY appsettings.json /src/appsettings.json
COPY API/ /src/API/
COPY Database/ /src/Database/
COPY Schedulers/ /src/Schedulers/

WORKDIR /src

RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build-env /app .

EXPOSE 80

CMD ["./Lyzer-BE"]
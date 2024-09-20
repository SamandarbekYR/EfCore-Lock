FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /

COPY ./ ./

WORKDIR /MyProj.WebApi

RUN dotnet restore

RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS serve
WORKDIR /app
COPY --from=build /MyProj.WebApi/output .

EXPOSE 8080
EXPOSE 443

ENTRYPOINT [ "dotnet", "MyProj.WebApi.dll" ]
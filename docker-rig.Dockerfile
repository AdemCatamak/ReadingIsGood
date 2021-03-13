FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish ./Src/RIG.WebApi/RIG.WebApi.csproj -c Release -r alpine-x64 --self-contained true -o out

# Build runtime image
FROM alpine:3.9.4

# Add some libs required by .NET runtime 
RUN apk add --no-cache libstdc++ libintl icu

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80
HEALTHCHECK --interval=5s --timeout=3s --retries=3 CMD curl -f / http://localhost:80/health-check || exit 1 

ENTRYPOINT ["./RIG.WebApi"]

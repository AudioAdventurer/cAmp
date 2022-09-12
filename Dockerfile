# Build react app
FROM node:18.9-alpine AS react-build
RUN mkdir /app
WORKDIR /app
COPY ./source/browser_apps/camp/package.json /app
RUN npm install
COPY ./source/browser_apps/camp/. /app
COPY ./source/browser_apps/camp_prod_env.js /app/src/env.js
RUN npm run build

# Build .net app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-build
RUN mkdir /src
WORKDIR /src
COPY ./source .
RUN dotnet publish apps/cAmp.Server.Console/cAmp.Server.Console.sln --output /build_output
COPY --from=react-build /app/dist /build_output/wwwroot

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS dotnet-runtime
RUN mkdir /app
WORKDIR /app
COPY --from=dotnet-build /build_output .
EXPOSE 8000/tcp
ENTRYPOINT ["dotnet", "cAmp.Server.Console.dll"]

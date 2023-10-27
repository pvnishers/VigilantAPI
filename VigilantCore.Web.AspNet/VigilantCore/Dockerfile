# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY VigilantCore.Web.AspNet.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /build

# Publish stage  
FROM build AS publish
RUN dotnet publish -c Release -o /publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "VigilantCore.Web.AspNet.dll"]
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app
COPY . ./

WORKDIR /app/RecipeProject.API
RUN dotnet restore
RUN dotnet publish -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "RecipeProject.API.dll"]




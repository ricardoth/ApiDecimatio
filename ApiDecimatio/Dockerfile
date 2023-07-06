FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR ApiDecimatio

COPY ./*.csproj ./
RUN dotnet restore

#COPY ALL
COPY ../ ../
RUN dotnet publish -c Release -o out

#BUILD IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /ApiDecimatio
COPY --from=build /ApiDecimatio/out .
ENTRYPOINT ["dotnet", "Decimatio.WebApi.dll"]

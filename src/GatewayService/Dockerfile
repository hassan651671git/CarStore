FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80



COPY carstore.sln carstore.sln
COPY src/auctionservices/auctionservices.csproj src/auctionservices/auctionservices.csproj
COPY src/contracts/contracts.csproj src/contracts/contracts.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/Biddingservice-controllers/Biddingservice-controllers.csproj src/Biddingservice-controllers/Biddingservice-controllers.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
#restore package dependencies

RUN dotnet restore carstore.sln

#copy the app folders over
COPY src/GatewayService src/GatewayService
WORKDIR /app/src/GatewayService
RUN dotnet publish -c release -o /app/src/out

#build the run time image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet","GatewayService.dll"]







FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NTL_Book.csproj", "./"]
RUN dotnet restore "NTL_Book.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "NTL_Book.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NTL_Book.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NTL_Book.dll"]

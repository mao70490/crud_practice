# 使用官方 ASP.NET Core 執行階段映像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 使用 SDK 建立階段（用於編譯）
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 複製 sln 檔並還原
COPY *.sln .
COPY CrudPractice1/*.csproj ./CrudPractice1/
RUN dotnet restore

# 複製其他程式碼並編譯
COPY CrudPractice1/. ./CrudPractice1/
WORKDIR /src/CrudPractice1
RUN dotnet publish -c Release -o /app/publish

# 執行階段（從 base 開始）
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# 啟動網站
ENTRYPOINT ["dotnet", "CrudPractice1.dll"]

# Встановлюємо базовий образ SDK .NET
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копіюємо проект до контейнера
COPY . ./
RUN dotnet publish -c Release -o out

# Встановлюємо базовий образ для виконання
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Вказуємо команду для запуску програми
ENTRYPOINT ["dotnet", "Habbit_Api.dll"]

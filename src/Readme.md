# Chat Application API

## Описание

Это REST API для чата, который позволяет пользователям взаимодействовать в реальном времени. API включает в себя функциональность для создания чатов, отправки и получения сообщений, а также управления пользователями.

## Установка

1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/your-repo/chat-app.git
   cd chat-app
   ```

2. Установите зависимости:
   ```bash
   dotnet restore
   ```

3. Запустите приложение:
   ```bash
   dotnet run
   ```

## Конфигурация

### Переменные окружения

- `FrontendOrigin` - URL вашего фронтенда, который будет использовать API.
- `Redis` - строка подключения к Redis.
- `PostgresConnection` - строка подключения к PostgreSQL.

## Эндпоинты

### Чаты

#### Создать чат

```http
POST /chat
Content-Type: application/json

{
    "name": "Название чата"
}
```

##### Ответ

- **200 OK**
```json
{
    "isSuccess": true,
    "data": {
        "chatId": "guid-чат"
    }
}
```

#### Получить чат по ID

```http
GET /chat/{id}
```

##### Ответ

- **200 OK**
```json
{
    "isSuccess": true,
    "data": {
        "chatId": "guid-чат",
        "name": "Название чата"
    }
}
```

### Сообщения

#### Обновить сообщение

```http
PATCH /message
Content-Type: application/json

{
    "messageId": "guid-сообщения",
    "newContent": "Обновленное содержимое сообщения"
}
```

##### Ответ

- **204 No Content**

#### Удалить сообщение

```http
DELETE /message
```

##### Ответ

- **204 No Content**

### Пользователи

#### Создать пользователя

```http
POST /user/create
Content-Type: application/json

{
    "username": "Имя пользователя",
    "password": "Пароль"
}
```

##### Ответ

- **200 OK**
```json
{
    "isSuccess": true,
    "data": {
        "userId": "guid-пользователя"
    }
}
```

## Логирование

Логирование осуществляется с помощью Serilog. Для настройки логирования вы можете изменить конфигурацию в `appsettings.json`.

## Зависимости

- .NET 6.0
- ASP.NET Core
- Entity Framework Core
- Redis
- PostgreSQL
- Swagger для документации API
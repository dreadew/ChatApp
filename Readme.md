# Чат-приложение

## Описание

Это проект чат-приложения, состоящий из бэкенда и фронтенда, который позволяет пользователям общаться в реальном времени. Проект построен с использованием .NET для бэкенда и React для фронтенда.

## Структура проекта

- **backend/**: Содержит серверную часть приложения, реализованную на .NET.
- **frontend/**: Содержит клиентскую часть приложения, разработанную на React.

## Запуск проекта

Для запуска приложения с использованием Docker и Docker Compose выполните следующие шаги:

1. Убедитесь, что у вас установлены [Docker](https://www.docker.com/get-started) и [Docker Compose](https://docs.docker.com/compose/install/).

2. Перейдите в корневую папку проекта:

   ```bash
   cd path/to/your/project
   ```

3. Запустите приложение с помощью Docker Compose:

   ```bash
   docker-compose up --build
   ```

4. Доступ к фронтенду будет предоставлен по адресу [http://localhost](http://localhost), а к бэкенду — по адресу [http://localhost:8000](http://localhost:8000).

## Функциональность

- **Чаты**: Пользователи могут создавать и участвовать в чатах.
- **Сообщения**: Возможность отправлять и получать сообщения в реальном времени.
- **Пользователи**: Регистрация и аутентификация пользователей.

## Зависимости

- Docker
- Docker Compose

### Backend
- .NET 6.0
- ASP.NET Core
- Entity Framework
- AutoMapper
- FluentValidation


### Frontend
- Node.js
- React.js
- Zustand
- React hook form
- Zod
- Tailwind CSS
- Axios
- React Router
# About

This app consists of two apps: desktop client represented by WPF application, and server API which is built upon ASP NET Core framework. The main thing is that the desktop app makes calls to API and retrieves cards that consist of any type of title and image. By interacting with desktop app, client can perform CRUD operations with cards.

# How to run

Both desktop and server apps must run.

Run the server app with CMD:

1. cd card-api
2. dotnet run

Run the desktop app:

1. cd card-client
2. dotnet build
3. Go to ./bin/Debug folder
4. Start card-client.exe

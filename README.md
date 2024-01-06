# MAUI Blazor Hybrid App with API Integration for Secure Registration and Login Flow

## Project Overview

This is a MAUI Blazor Hybrid app that integrates with a custom API and a MySQL database to handle user registration, login and token-based authentication. I plan to use this registration and login UI to build out a full app in the future. 

The repo for the API it uses is here: https://github.com/jozef-allen/database_api.

## Goals

- To build a MAUI Blazor Hybrid app.
- To integrate the three elements successfully (DB, API, app).
- To understand a complete login and authentication process in .NET.

## Technologies Used

- .NET 6
- MAUI
- Blazor (C# and HTML)
- MySQL
- Pomelo.EntityFrameworkCore.MySql
- JWT for token-based authentication
- HttpClient for API communication

## Useful resources

- [.NET MAUI Blazor app + API series](https://www.youtube.com/watch?v=paPe68vT2Mg&list=PLn-SpzWnVxDeSS5EHIsmQwU7iv_pU49K8&index=1)
- [Connecting to local web services from Android emulators](https://www.youtube.com/watch?v=kvNhLKuAySA)
- [JWT encoder and key generator](https://dinochiesa.github.io/jwt/)
- [Salting and hashing passwords](https://www.youtube.com/watch?v=qgpsIBLvrGY)

## Process

### Starting point

I used tutorials from [@mistrypragnesh40](https://github.com/mistrypragnesh40/) to form the basis of this project. However, my eventual system diverged from these guides in a few ways:
- I used MySQL instead of SQLite.
- I used Pomelo instead of EntityFrameworkCore for migrations.
- I added error handling that wasn't present in the guides.

### MySQL

I set up a MySQL database to hold the user account information. My eventual goal with the app will require a centralised database, so I opted for MySQL rather than a SQLite (which is local to the app and the device it sits on).

### API

I set up a basic CRUD API that could handle some user registration functions without authentication. This allowed me to test the connection from the API to the database.

### App and Android emulator

I then linked up the app so that information from the database could be represented in the app, via the API. At this point I was trying to iron out any kinks in general. 

It took a while to address issues with communication between the Android emulator and the API. With the help of this video (https://www.youtube.com/watch?v=kvNhLKuAySA) and the Microsoft article it mentions (https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services?view=net-maui-8.0), adjustments were made to use the second localhost port for the API (HTTP instead of HTTPS). This would not be ideal in a live environment but is OK during development. The app was then configured to target '10.0.2.2' instead of 'localhost' for Android emulator access. I also configured cross-origin resource sharing (CORS) in the API so that it could be contactable from the app.

### Login flow

Now that things were up-and-running, I implemented the registration and login system in the API and app code, and migrated this all across to the database (code-first approach).

I created a JWT key and implemented access and refresh tokens. Currently the project is set to create access tokens that last for 5 seconds so the refresh functionality can easily be checked. This can easily be changed in RegistrationController.cs in the API. Upon receiving tokens, the app stores them in SecureStorage and they can be retrieved from there every time the root page (AppLaunch.razor) is loaded.

## Wins & challenges

### Wins

- I learnt about the hashing and salting of passwords to provide two layers of security when stored in the database, and saw this in action.
- I implemented access and refresh tokens.
- I built a successful MAUI Blazor hybrid app that functions as intended with API and database.

### Challenges 

- A time-consuming obstacle was figuring out how to get the Android emulator working instead of running the app in Windows Machine.
- It also took a while to get Pomelo.EntityFrameworkCore.MySql rather than the more standard, MySql.Data.EntityFrameworkCore working.
- Implementing error handling - for instance, for if the API is uncontactable - meant using a custom class that diverged from the tutorial. This then impacted all methods and meant quite a few changes.

## Going forward

I intend to use this as a foundation for future app projects and am confident I know this system well now.
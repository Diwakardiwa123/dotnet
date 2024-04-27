# Scheduler

## Tech stack

1. C#
2. .Net Core web API
   1. Entity Framework Core
   2. JWT based Auth
   3. Custom middlewares
   4. Cors
3. Angular CLI
   1. Services
   2. Components
   3. Angular materials
4. HTML/CSS

## Project

### Details

The Scheduler is a basic CRUD operation application where each appointment is added to the database using EntityFramework Core. JWT token authentication is employed, and the tokens are validated for each API call using custom middleware. The UI is built using Angular Materials.

### Issues and Fix

**Cors error after published**

This caused by http and https difference issue. Once we fix both angular application and WebAPI application are runs in either https or http.

### Github link

[Github link](https://github.com/Diwakardiwa123/dotnet)

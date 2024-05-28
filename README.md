dotnet new sln

dotnet new web -o Financial.Api

dotnet new blazorwasm -o Financial.App --pwa

dotnet new classlib -o Financial.Core

dotnet sln add ./Financial.Api

dotnet sln add ./Financial.Core

dotnet sln add ./Financial.App

cd Financial.Api
dotnet add reference ../Financial.Core

cd Financial.App
dotnet add reference ../Financial.Core
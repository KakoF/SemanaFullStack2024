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



Vamos trabalhar com entity apenas na api

cd Financial.Api
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design (Permite criar tabelas pelo proprio projeto codefirst)


Infra
https://balta.io/blog/sql-server-docker
docker run -v ~/docker --name sqlserver_fullstack -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server



Instalação de ferramentas, para gerenciamento das migrations
dotnet tool install ef-tool
dotnet tool update --global dotnet-ef


Rodar migrations

Criar migrations
dotnet ef migrations add v1

Executar migrations no banco
dotnet ef database update






Pacotes para aplicacao WEB
dotnet add package MudBlazor
https://mudblazor.com/getting-started/installation#manual-install-add-font-and-style-references
https://fonts.google.com/selection?query=Rale

dotnet add package Microsoft.Extensions.Http
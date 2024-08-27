# ChallengeRPA

## Instalation
I used the SQL Server database. Modify the ConnectionString (appSettings.json) in the Infra.Data and Infra.Migration projects, then run the "Update-Database" command in the Package Manager Console (to create the tables in the database).
Finally, start the Presentation.API ​​project. 

## Usage

https://localhost:7188/search?q={term} : Perform a search on the website "https://www.alura.com.br/" for the term indicated in the "q" parameter. Ex: https://localhost:7188/search?q=RPA

https://localhost:7188/courses : Lists the courses in the database

## Libraries
.NetCore 8

Dapper - ORM application

Entity Framework - Migration 

Selenium 

WebDriver - ChromeDriver

Sql Server 

## Layers

Presentation - Web Api

Domain - Entities, Interfaces

Infra - Repository, Migration

DI - Dependency Injection

RPA - Web Crawler








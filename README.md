# csharp-blogpost
post of a blog, data model in csharp .net


## ER Model

~~~mermaid
erDiagram
    POST ||--o{ PostComment : places
    PostComment ||--o{ COMMENTER : places
    POST ||--o{ PostAuthor : places
    PostAuthor ||--o{ AUTHOR : places
    AUTHOR ||--o{ CITY : places
    POST ||--o{ PostCategory : places
    PostCategory ||--o{ Category : places
~~~


### Schema from Dbeaver

![ER Diagram1](./assets/sqliteDiagram_v1.jpg "Entity Relationship Diagram")

### Schema from MS SQL Server

![ER Diagram2](./assets/sqliteDiagram_v2.jpg "Entity Relationship Diagram from MS SQL Server")


## Misc

- ef core [dotnet add package Microsoft.EntityFrameworkCore --version 7.0.3]
- ef design [dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.3]
- ef sqlite [dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.3]
    - [https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/7.0.3]
- ef SQL Server [dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.3]
- DTO documentation
    - [https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5]

### Commands EF

- `dotnet ef migrations add InitialCreate1`
- `dotnet ef database update`





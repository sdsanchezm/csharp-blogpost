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

## Misc

- 
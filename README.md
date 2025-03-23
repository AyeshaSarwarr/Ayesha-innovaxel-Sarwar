# Frontend of this task in in Blazor web assemly.

# Backened of this task is using Asp.net core web api.

# Used HttpClient: Used to make API calls from the Blazor frontend to the backend to manage URL data.

# I used Entity Framework Core instead of typical sql queries in my task.

I am using these technologies because i am familiar with these technologies due to my university work. I have use My sql database.

If you want to download my project then database structure is:

CREATE TABLE [dbo].[UrlEntities] (
[Id] INT IDENTITY (1, 1) NOT NULL,
[Url] NVARCHAR (MAX) NOT NULL,
[ShortCode] NVARCHAR (6) NULL,
[CreatedAt] DATETIME2 (7) NULL,
[UpdatedAt] DATETIME2 (7) NULL,
[UrlVisitedCount] INT NOT NULL,
CONSTRAINT [PK_UrlEntities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

and there is packages to use EF Core like:
-> microsoft.EntityframeworkCore
-> microsoft.entityframeworkCore.SqlServer
-> microsoft.entityframeworkCore.tools

I have created different pages to showcase working of api like creating short url, updating, deleting short url etc.

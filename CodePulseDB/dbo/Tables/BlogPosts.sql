CREATE TABLE [dbo].[BlogPosts] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Title]            NVARCHAR (MAX)   NOT NULL,
    [ShortDescription] NVARCHAR (MAX)   NOT NULL,
    [FeaturedImageUrl] NVARCHAR (MAX)   NOT NULL,
    [UrlHandle]        NVARCHAR (MAX)   NOT NULL,
    [PublishedDate]    DATETIME2 (7)    NULL,
    [Author]           NVARCHAR (MAX)   NOT NULL,
    [Isvisible]        BIT              NOT NULL,
    [Content]          NVARCHAR (MAX)   DEFAULT (N'') NOT NULL,
    CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


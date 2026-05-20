CREATE TABLE [dbo].[Categories] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (MAX)   NOT NULL,
    [UrlHandel] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[BlogImages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [FileName]      NVARCHAR (MAX)   NOT NULL,
    [FileExtension] NVARCHAR (MAX)   NOT NULL,
    [Title]         NVARCHAR (MAX)   NOT NULL,
    [Url]           NVARCHAR (MAX)   NOT NULL,
    [DateCreated]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_BlogImages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


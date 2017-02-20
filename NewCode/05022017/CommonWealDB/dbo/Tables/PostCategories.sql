CREATE TABLE [dbo].[PostCategories] (
    [PostcategoryID] INT      IDENTITY (1, 1) NOT NULL,
    [PostID]         INT      NOT NULL,
    [CategoryID]     INT      NOT NULL,
    [createdOn]      DATETIME NULL,
    [ModifiedOn]     DATETIME NULL,
    CONSTRAINT [PK_PostCategories] PRIMARY KEY CLUSTERED ([PostcategoryID] ASC),
    CONSTRAINT [FK_PostCategories_AreaOfInterest] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[AreaOfInterest] ([CategoryID]),
    CONSTRAINT [FK_PostCategories_NGOPost1] FOREIGN KEY ([PostID]) REFERENCES [dbo].[NGOPost] ([PostID])
);


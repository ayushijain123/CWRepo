CREATE TABLE [dbo].[AreaOfInterest] (
    [CategoryID]   INT          IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AreaOfInterest] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);


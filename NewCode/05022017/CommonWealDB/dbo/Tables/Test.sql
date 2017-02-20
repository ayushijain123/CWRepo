CREATE TABLE [dbo].[Test] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Content] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED ([ID] ASC)
);


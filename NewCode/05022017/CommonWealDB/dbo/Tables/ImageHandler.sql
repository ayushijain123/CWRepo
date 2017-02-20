CREATE TABLE [dbo].[ImageHandler] (
    [ID]       INT          IDENTITY (1, 1) NOT NULL,
    [Image]    IMAGE        NULL,
    [Filetype] VARCHAR (50) NULL,
    CONSTRAINT [PK_ImageHandler] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[WorkingArea] (
    [WorkingAreaID] INT IDENTITY (1, 1) NOT NULL,
    [LoginID]       INT NOT NULL,
    [CategoryID]    INT NULL,
    CONSTRAINT [PK_WorkingArea] PRIMARY KEY CLUSTERED ([WorkingAreaID] ASC)
);


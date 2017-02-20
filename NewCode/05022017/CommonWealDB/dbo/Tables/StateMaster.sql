CREATE TABLE [dbo].[StateMaster] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [CountryID] INT           NULL,
    CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StateMaster_CountryMaster] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[CountryMaster] ([ID])
);


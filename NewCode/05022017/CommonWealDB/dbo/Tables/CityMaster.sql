CREATE TABLE [dbo].[CityMaster] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NULL,
    [StateID] INT           NULL,
    CONSTRAINT [PK_CityMaster] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CityMaster_StateMaster] FOREIGN KEY ([StateID]) REFERENCES [dbo].[StateMaster] ([ID])
);


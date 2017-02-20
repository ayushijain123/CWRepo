CREATE TABLE [dbo].[CountryMaster] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [CountryCode] VARCHAR (5)   NULL,
    CONSTRAINT [PK_CountryMaster] PRIMARY KEY CLUSTERED ([ID] ASC)
);


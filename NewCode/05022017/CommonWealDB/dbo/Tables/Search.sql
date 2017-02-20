CREATE TABLE [dbo].[Search] (
    [ID]        INT          IDENTITY (1, 1) NOT NULL,
    [StateName] VARCHAR (50) NOT NULL,
    [CityName]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Search] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[ChatUser] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [LoginID]   INT            NOT NULL,
    [ConnectID] NVARCHAR (MAX) NULL,
    [Status]    BIT            NULL
);


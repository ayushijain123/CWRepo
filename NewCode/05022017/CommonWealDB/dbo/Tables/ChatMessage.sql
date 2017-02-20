CREATE TABLE [dbo].[ChatMessage] (
    [ChatID]    INT            IDENTITY (1, 1) NOT NULL,
    [LoginID]   INT            NOT NULL,
    [Message]   NVARCHAR (MAX) NULL,
    [SenderID]  INT            NULL,
    [CreatedOn] DATETIME       NULL
);


CREATE TABLE [dbo].[SpamUser] (
    [SpamUserId]      INT           IDENTITY (1, 1) NOT NULL,
    [LoginId]         INT           NOT NULL,
    [CommentContent]  VARCHAR (MAX) NULL,
    [ReportedBy]      VARCHAR (50)  NULL,
    [ModifiedOn]      DATETIME      NULL,
    [AbuseedUserName] VARCHAR (50)  NULL,
    [CommentID]       INT           NULL,
    CONSTRAINT [PK_SpamUser] PRIMARY KEY CLUSTERED ([SpamUserId] ASC)
);


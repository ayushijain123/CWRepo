CREATE TABLE [dbo].[PostComment] (
    [CommentID]   INT           IDENTITY (1, 1) NOT NULL,
    [LoginID]     INT           NULL,
    [PostID]      INT           NULL,
    [CommentText] VARCHAR (MAX) NULL,
    [CreatedOn]   DATETIME      NULL,
    [ModifiedOn]  DATETIME      NULL,
    [CreatedBy]   VARCHAR (100) NULL,
    [IsDelete]    BIT           NULL,
    CONSTRAINT [PK_PostComment] PRIMARY KEY CLUSTERED ([CommentID] ASC)
);


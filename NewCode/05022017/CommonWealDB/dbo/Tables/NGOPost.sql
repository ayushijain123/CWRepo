CREATE TABLE [dbo].[NGOPost] (
    [PostID]           INT           IDENTITY (1, 1) NOT NULL,
    [LoginID]          INT           NULL,
    [PostDateTime]     DATETIME      NULL,
    [PostType]         VARCHAR (10)  NULL,
    [PostContent]      VARCHAR (MAX) NULL,
    [PostUrl]          VARCHAR (250) NULL,
    [PostLikeCount]    INT           CONSTRAINT [DF_NGOPost_PostLikeCount] DEFAULT ((0)) NULL,
    [PostCommentCount] INT           CONSTRAINT [DF_NGOPost_PostCommentCount] DEFAULT ((0)) NULL,
    [CreatedOn]        DATETIME      NULL,
    [ModifiedOn]       DATETIME      NULL,
    [CreatedBy]        VARCHAR (100) NULL,
    [Isdelete]         BIT           NULL,
    [RequestID]        INT           NULL,
    [IsRequest]        BIT           NULL,
    CONSTRAINT [PK_NGOPost] PRIMARY KEY CLUSTERED ([PostID] ASC)
);


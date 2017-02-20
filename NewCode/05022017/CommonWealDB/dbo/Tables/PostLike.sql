CREATE TABLE [dbo].[PostLike] (
    [LikeID]     INT      IDENTITY (1, 1) NOT NULL,
    [LoginID]    INT      NOT NULL,
    [IsLike]     BIT      NULL,
    [CreatedOn]  DATETIME NULL,
    [ModifiedOn] DATETIME NULL,
    [PostID]     INT      NULL,
    CONSTRAINT [PK_PostLike] PRIMARY KEY CLUSTERED ([LikeID] ASC),
    CONSTRAINT [FK_PostLike_NGOPost] FOREIGN KEY ([PostID]) REFERENCES [dbo].[NGOPost] ([PostID])
);


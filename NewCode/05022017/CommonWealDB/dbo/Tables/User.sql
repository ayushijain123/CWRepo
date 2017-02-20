CREATE TABLE [dbo].[User] (
    [LoginID]       INT          IDENTITY (1, 1) NOT NULL,
    [LoginPassword] VARCHAR (50) NOT NULL,
    [LoginUserType] INT          NOT NULL,
    [LoginEmailID]  VARCHAR (50) NULL,
    [IsActive]      BIT          CONSTRAINT [DF_UserLogin_IsActive] DEFAULT ((0)) NOT NULL,
    [IsBlock]       BIT          CONSTRAINT [DF_UserLogin_IsBlock] DEFAULT ((0)) NOT NULL,
    [CreatedOn]     DATETIME     NULL,
    [ModifiedOn]    DATETIME     NULL,
    [IsWarn]        BIT          NULL,
    [IsSpam]        BIT          NULL,
    [IsDecline]     BIT          NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([LoginID] ASC)
);


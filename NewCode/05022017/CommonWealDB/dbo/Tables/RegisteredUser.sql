CREATE TABLE [dbo].[RegisteredUser] (
    [UserID]        INT           IDENTITY (1, 1) NOT NULL,
    [UserEmail]     VARCHAR (100) NOT NULL,
    [UserPassword]  VARCHAR (50)  NOT NULL,
    [FirstName]     VARCHAR (50)  NOT NULL,
    [LastName]      VARCHAR (50)  NULL,
    [LoginUserType] INT           NULL,
    [UserKey]       VARCHAR (50)  NULL,
    [Mobile]        VARCHAR (50)  NOT NULL,
    [LoginID]       INT           NOT NULL,
    [CreatedOn]     DATETIME      NULL,
    [ModifiedOn]    DATETIME      NULL,
    [CreatedBy]     VARCHAR (100) NULL,
    CONSTRAINT [PK_RegisteredUser] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_RegisteredUser_UserLogin] FOREIGN KEY ([LoginID]) REFERENCES [dbo].[User] ([LoginID])
);


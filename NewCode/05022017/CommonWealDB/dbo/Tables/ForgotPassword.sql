CREATE TABLE [dbo].[ForgotPassword] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [EmailId]      VARCHAR (50)  NOT NULL,
    [OTP]          VARCHAR (50)  NOT NULL,
    [DateInserted] DATETIME      NULL,
    [CreatedOn]    DATETIME      NULL,
    [ModifiedOn]   DATETIME      NULL,
    [CreatedBy]    VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


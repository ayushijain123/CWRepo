CREATE TABLE [dbo].[RoleType] (
    [RoleID]     INT          IDENTITY (1, 1) NOT NULL,
    [RoleName]   VARCHAR (50) NULL,
    [CreatedOn]  DATETIME     NULL,
    [ModifiedOn] DATETIME     NULL,
    CONSTRAINT [PK_RoleType] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


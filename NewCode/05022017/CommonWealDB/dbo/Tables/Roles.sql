CREATE TABLE [dbo].[Roles] (
    [RoleID]      INT              IDENTITY (1, 1) NOT NULL,
    [RoleName]    NVARCHAR (256)   NOT NULL,
    [IsActive]    BIT              NOT NULL,
    [CreatedBy]   INT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   INT              NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [RoleGuid]    UNIQUEIDENTIFIER CONSTRAINT [DF_Roles_RoleGuid] DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


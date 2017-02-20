CREATE TABLE [dbo].[UserRole] (
    [UserRoleID]  INT              IDENTITY (1, 1) NOT NULL,
    [UserKey]     INT              NOT NULL,
    [RoleID]      INT              NOT NULL,
    [IsActive]    BIT              NOT NULL,
    [CreatedBy]   INT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   INT              NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [Guid]        UNIQUEIDENTIFIER CONSTRAINT [DF_UserRole_Guid] DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    CONSTRAINT [FK_UserRolesRoleID_RoleRoleID] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID])
);


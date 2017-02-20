CREATE TABLE [dbo].[DonationDetails] (
    [ItemID]       INT          IDENTITY (1, 1) NOT NULL,
    [RequestID]    INT          NOT NULL,
    [ItemName]     VARCHAR (50) NULL,
    [ItemCount]    INT          NULL,
    [DonatedCount] INT          NULL,
    [ItemRequire]  INT          NULL,
    [DonatedCost]  INT          NULL,
    [createdOn]    DATETIME     NULL,
    [UnitPrice]    INT          NULL,
    CONSTRAINT [PK_DonationDetails] PRIMARY KEY CLUSTERED ([ItemID] ASC),
    CONSTRAINT [FK_DonationDetails_DonationRequest] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[DonationRequest] ([RequestID])
);


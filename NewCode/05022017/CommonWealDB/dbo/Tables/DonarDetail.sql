CREATE TABLE [dbo].[DonarDetail] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [ItemID]       INT      NOT NULL,
    [DonarLoginID] INT      NULL,
    [createdOn]    DATETIME NULL,
    [RequestId]    INT      NULL,
    [Donatecount]  INT      NULL,
    CONSTRAINT [PK_DonarDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DonarDetail_DonationDetails] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[DonationDetails] ([ItemID])
);


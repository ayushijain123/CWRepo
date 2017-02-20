CREATE TABLE [dbo].[DonationRequest] (
    [RequestID]    INT           IDENTITY (1, 1) NOT NULL,
    [ImgeUrl]      VARCHAR (150) NULL,
    [Description]  VARCHAR (MAX) NULL,
    [RequestNGOId] INT           NOT NULL,
    [createdOn]    DATETIME      NULL,
    [Status]       BIT           NULL,
    [ItemCost]     INT           NULL,
    CONSTRAINT [PK_DonationRequest] PRIMARY KEY CLUSTERED ([RequestID] ASC),
    CONSTRAINT [FK_DonationRequest_DonationRequest] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[DonationRequest] ([RequestID])
);


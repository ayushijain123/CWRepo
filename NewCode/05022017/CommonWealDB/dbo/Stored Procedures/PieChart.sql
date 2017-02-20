CREATE PROCEDURE [dbo].[PieChart] 
	-- Add the parameters for the stored procedure here
 @NGOName varchar = Null out, 
	@TotalDonationAmount int = 0 out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT sum([DonationRequest].[ItemCost]) As TotalDonationAmount, [NGOUser].[NGOName] 
 from [DonationRequest]
 Inner Join [NGOUser] 
 On [DonationRequest].[RequestNGOId]=[NGOUser].[LoginID]
 group by [NGOUser].[NGOName],[NGOUser].[LoginID]

END


exec PieChart
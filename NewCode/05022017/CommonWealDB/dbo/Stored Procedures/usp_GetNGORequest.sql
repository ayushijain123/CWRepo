-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_GetNGORequest
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT NGOName,NGOEmailID,WebsiteUrl,IsActive FROM NGOUser 
	WHERE IsActive=0 AND IsBlock=0
END
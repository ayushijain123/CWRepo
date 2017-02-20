CREATE PROCEDURE [dbo].[usp_GetAllData]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @TotalActive int
	declare @TotalBlock int
	declare @TotalWarn int

    -- Insert statements for procedure here
	SET @TotalActive = (SELECT cast(count(IsActive) as int)	
						from NGOUser
						where IsActive=1
						group by IsActive)

	SET @TotalBlock = (SELECT cast(Count(IsBlock) as int)
						from NGOUser
						where IsBlock=1
						group by IsBlock)


	SET @TotalWarn = (SELECT cast (count(IsWarn) as int) 
					from NGOUser
					where IsWarn=1
					group by IsWarn)

	--SELECT (SELECT CAST(@TotalActive AS VARCHAR(50)) + ',' + CAST(@TotalBlock AS VARCHAR(50)) +',' + CAST(@TotalWarn AS VARCHAR(50))) AS 'Total'
	SELECT CAST(@TotalActive AS VARCHAR(50)) AS'Total'
	UNION 
	SELECT CAST(@TotalBlock AS VARCHAR(50)) AS 'Total Block'
	UNION
	SELECT CAST(@TotalWarn AS VARCHAR(50)) AS 'Total Warn'


END
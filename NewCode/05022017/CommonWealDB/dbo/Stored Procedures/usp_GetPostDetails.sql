CREATE PROCEDURE usp_GetPostDetails
@PostID int
AS
BEGIN
	SELECT PostContent FROM NGOPost WHERE PostID = @PostID
END
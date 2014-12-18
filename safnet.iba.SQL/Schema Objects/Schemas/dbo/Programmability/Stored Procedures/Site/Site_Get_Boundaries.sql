CREATE PROCEDURE [dbo].[Site_Get_Boundaries]
	@SiteId UNIQUEIDENTIFIER = null
AS
	SET NOCOUNT ON;

	SELECT Latitude, Longitude
	FROM dbo.SiteBoundary
	WHERE SiteId = @SiteId
	ORDER BY VertexSequence;

RETURN 0
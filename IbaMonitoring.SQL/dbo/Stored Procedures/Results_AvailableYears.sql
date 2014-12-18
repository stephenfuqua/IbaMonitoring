CREATE PROCEDURE [dbo].[Results_AvailableYears]
AS
	SELECT DISTINCT StartYear
	FROM dbo.SiteVisit
	ORDER BY 1;
RETURN 0

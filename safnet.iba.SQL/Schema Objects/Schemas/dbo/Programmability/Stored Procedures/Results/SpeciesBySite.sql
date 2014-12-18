CREATE PROCEDURE [dbo].[Results_SpeciesBySite]
(
	@SiteId UNIQUEIDENTIFIER,
	@Year INT
)
AS
	SET NOCOUNT ON;

	SELECT s.CommonName, DATEPART(wk,sv.StartTime) as [Week], COUNT(1) as SpeciesCount
	FROM dbo.Species s
	INNER JOIN dbo.Observation o ON s.SpeciesId = o.SpeciesId
	INNER JOIN dbo.PointSurvey ps ON o.EventId = ps.EventId
	INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
	INNER JOIN dbo.Site si ON sv.LocationId = si.LocationId
	WHERE si.LocationId = @SiteId
		AND sv.IsDataEntryComplete = 1
		AND o.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
		AND sv.StartYear = @Year
	GROUP BY s.CommonName, DATEPART(wk,sv.StartTime);
RETURN 0
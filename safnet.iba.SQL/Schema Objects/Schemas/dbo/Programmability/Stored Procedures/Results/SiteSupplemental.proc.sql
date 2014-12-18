CREATE PROCEDURE [dbo].[Results_SiteSupplemental]
(
	@SiteId UNIQUEIDENTIFIER,
	@Year INT
)
AS
	SET NOCOUNT ON;

	SELECT DISTINCT s.CommonName
	FROM dbo.Species s
	INNER JOIN dbo.Observation o ON s.SpeciesId = o.SpeciesId
	INNER JOIN dbo.SiteVisit sv ON o.EventId = sv.EventId
	INNER JOIN dbo.Site si ON sv.LocationId = si.LocationId
	WHERE si.LocationId = @SiteId
		AND sv.IsDataEntryComplete = 1
		AND o.ObservationTypeId = 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
		AND NOT EXISTS (SELECT 1 FROM Observation o2
						INNER JOIN dbo.PointSurvey ps2 ON o2.EventId = ps2.EventId
						INNER JOIN dbo.SiteVisit sv2 ON ps2.SiteVisitId = sv2.EventId
						INNER JOIN dbo.Location l2 ON sv2.LocationId = l2.LocationId
						WHERE o2.SpeciesId = o.SpeciesId)
		AND sv.StartYear = @Year
	ORDER BY s.CommonName;

RETURN 0
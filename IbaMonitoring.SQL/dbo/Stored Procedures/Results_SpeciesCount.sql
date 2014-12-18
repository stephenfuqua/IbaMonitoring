CREATE PROCEDURE [dbo].[Results_SpeciesCount]
(
	@Year INT
)
AS
	SELECT s.SpeciesId, s.CommonName, s.ScientificName, COUNT(1) as SpeciesCount
	FROM dbo.Species s
	INNER JOIN dbo.Observation o ON s.SpeciesId = o.SpeciesId
	INNER JOIN dbo.PointSurvey ps ON o.EventId = ps.EventId
	INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
	WHERE sv.IsDataEntryComplete = 1
		AND o.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
		AND sv.StartYear = @Year
	GROUP BY s.SpeciesId, s.CommonName, s.ScientificName
	ORDER BY s.CommonName;

RETURN 0;

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Results_SpeciesCount] TO [IbaAccounts]
    AS [dbo];


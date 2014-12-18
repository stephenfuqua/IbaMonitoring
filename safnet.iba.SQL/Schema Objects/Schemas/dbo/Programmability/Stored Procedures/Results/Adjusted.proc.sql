CREATE PROCEDURE [dbo].[Results_Adjusted]
(
	@SpeciesId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@Year INT
)
AS
	SET NOCOUNT ON;

	WITH effort as (
		SELECT site.LocationId, COUNT(1) as TotalCounts
		FROM dbo.Location site
		INNER JOIN dbo.SiteVisit sv ON site.LocationId = sv.LocationId
		INNER JOIN dbo.PointSurvey ps ON sv.EventId = ps.SiteVisitId
		WHERE sv.IsDataEntryComplete = 1
		AND sv.StartYear = @Year
		GROUP BY site.LocationId
	),
	avgeffort as (
		SELECT AVG(TotalCounts) as AvgEffort
		FROM effort
	),
	adjeffort as (
		SELECT effort.LocationId, CAST(effort.TotalCounts as DECIMAL(6,0)) / avgeffort.AvgEffort as AdjustedSamplingEffort
		FROM effort, avgeffort
	)
	SELECT ste.LocationName as SiteName, s.CommonName, CAST(COUNT(1) as DECIMAL(6,0)) / ae.AdjustedSamplingEffort as RelativeAbundance
	FROM dbo.Observation ob
	INNER JOIN dbo.PointSurvey ps ON ob.EventId = ps.EventId
	INNER JOIN dbo.Species s ON ob.SpeciesId = s.SpeciesId
	INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
	INNER JOIN dbo.Location ste ON sv.LocationId = ste.LocationId
	INNER JOIN adjeffort ae ON sv.LocationId = ae.LocationId
	WHERE ob.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
		AND sv.IsDataEntryComplete = 1
		AND (@SpeciesId = '00000000-0000-0000-0000-000000000000' OR ob.SpeciesId = @SpeciesId)
		AND sv.StartYear = @Year
	GROUP BY ste.LocationName, s.CommonName, ae.AdjustedSamplingEffort
RETURN 0
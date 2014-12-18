CREATE PROCEDURE dbo.[Results_SiteList]
AS
	SET NOCOUNT ON;

	WITH effort as (
		SELECT site.LocationId, COUNT(1) as TotalCounts
		FROM dbo.Location site
		INNER JOIN dbo.SiteVisit sv ON site.LocationId = sv.LocationId
		INNER JOIN dbo.PointSurvey ps ON sv.EventId = ps.SiteVisitId
		WHERE sv.IsDataEntryComplete = 1
		GROUP BY site.LocationId
	),
	avgeffort as (
		SELECT AVG(TotalCounts) as AvgEffort
		FROM effort
	),
	adjeffort as (
		SELECT effort.LocationId, CAST(effort.TotalCounts as DECIMAL(6,0)) / avgeffort.AvgEffort as AdjustedSamplingEffort
		FROM effort, avgeffort
	),
	adjcount as (
		SELECT sv.LocationId, s.SpeciesId, 
			CAST(COUNT(1) as DECIMAL(6,0)) / ae.AdjustedSamplingEffort as RelativeAbundance
		FROM dbo.Observation ob
		INNER JOIN dbo.PointSurvey ps ON ob.EventId = ps.EventId
		INNER JOIN dbo.Species s ON ob.SpeciesId = s.SpeciesId
		INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
		INNER JOIN adjeffort ae ON sv.LocationId = ae.LocationId
		WHERE ob.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
			AND sv.IsDataEntryComplete = 1
		GROUP BY sv.LocationId, s.SpeciesId, ae.AdjustedSamplingEffort
	),
	adjindiv as (
		SELECT LocationId, SUM(RelativeAbundance) as AdjustCountIndividuals
		FROM adjcount
		GROUP BY LocationId
	),
	PsI as ( -- PsI = Pi because "Pi" is a reserved word
		SELECT ai.LocationId, AdjustCountIndividuals, SpeciesId, RelativeAbundance / AdjustCountIndividuals * LOG(RelativeAbundance / AdjustCountIndividuals) as LogAdjRelAbund
		FROM adjindiv ai
		INNER JOIN adjcount ac ON ai.LocationId = ac.LocationId
	),
	richness as (
		SELECT LocationId, COUNT(DISTINCT SpeciesId) as Richness
		FROM adjcount
		GROUP BY LocationId
	),
	shannon as (
		SELECT DISTINCT LocationId, -SUM(LogAdjRelAbund)  OVER (PARTITION BY LocationId) as DiversityIndex, AdjustCountIndividuals
		FROM PsI
	)
	SELECT  s.LocationId, l.LocationName, r.Richness, 
	CAST(s.DiversityIndex  - ( (r.Richness - 1 ) / (2*s.AdjustCountIndividuals) ) as DECIMAL(5,4) ) as AdjustedDiversityIndex,
	CAST(( s.DiversityIndex  - ( (r.Richness - 1 ) / (2*s.AdjustCountIndividuals) ) ) / LOG(s.AdjustCountIndividuals)  as DECIMAL(5,4) ) as Evenness
	FROM shannon s
	INNER JOIN dbo.Location l ON s.LocationId = l.LocationId
	INNER JOIN richness r ON s.LocationId = r.LocationId

RETURN 0
CREATE PROCEDURE dbo.[Results_SiteMeasures_Migration]
(
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
			AND MONTH(sv.StartTime) < 6
		AND sv.StartYear = @Year
		GROUP BY site.LocationId
	),
	avgeffort as (
		SELECT AVG(CAST(TotalCounts as DECIMAL(16,10))) as AvgEffort
		FROM effort
	),
	adjeffort as (
		SELECT effort.LocationId, CAST(effort.TotalCounts as DECIMAL(16,10)) / avgeffort.AvgEffort as AdjustedSamplingEffort
		FROM effort, avgeffort
	),
	adjcount as (
		SELECT sv.LocationId, s.SpeciesId, 
			CAST(SUM(CASE WHEN ob.ObservationTypeId = '8E4055BC-7644-4670-8C1D-648BF81519D8' THEN 1 ELSE 0 END) as DECIMAL(16,10)) / ae.AdjustedSamplingEffort as AdjustLess50,
			CAST(SUM(CASE WHEN ob.ObservationTypeId = 'F7D5E189-233E-4B9E-B15A-4094640C3880' THEN 1 ELSE 0 END) as DECIMAL(16,10)) / ae.AdjustedSamplingEffort as AdjustGreater50,
			CAST(COUNT(1) as DECIMAL(16,10)) / ae.AdjustedSamplingEffort as Adjusted
		FROM dbo.Observation ob
		INNER JOIN dbo.PointSurvey ps ON ob.EventId = ps.EventId
		INNER JOIN dbo.Species s ON ob.SpeciesId = s.SpeciesId
		INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
		INNER JOIN adjeffort ae ON sv.LocationId = ae.LocationId
		WHERE ob.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
			AND sv.IsDataEntryComplete = 1
			AND s.UseForCommunityMeasures = 1
			AND MONTH(sv.StartTime) < 6
		AND sv.StartYear = @Year
		GROUP BY sv.LocationId, s.SpeciesId, ae.AdjustedSamplingEffort
	), 
	adjindiv as (
		SELECT LocationId, SUM(Adjusted) as AdjustedCountAllSpecies
		FROM adjcount
		GROUP BY LocationId
	),
	dropout as (-- Drop out the counts of any species that represents less than half a percent of all the observations at a particular site
		SELECT ac.LocationId, ac.SpeciesId, ac.Adjusted, ac.AdjustLess50, ac.AdjustGreater50
		FROM adjcount ac 
		INNER JOIN adjindiv ai ON ac.LocationId = ai.LocationId
		-- no more dropout, but leave this part of the query so nothing else has to change
	),
	probability as (
		SELECT SpeciesId, LOG(SUM(Adjusted)/SUM(AdjustGreater50)) as Term1
		FROM dropout
		GROUP BY SpeciesId
		HAVING SUM(AdjustGreater50) > 0
	),
	densities as (
		SELECT d.SpeciesId, d.LocationId, 
			/* in next part, multiply by 10,000 to convert m^2 to hectare */
			p.Term1 * ( d.Adjusted / (e.TotalCounts * PI() * POWER(50.0,2.0)) ) * 10000 as Density
		FROM dropout d
		INNER JOIN effort e ON d.LocationId = e.LocationId
		INNER JOIN probability p ON d.SpeciesId = p.SpeciesId
	),
	newadj as (	-- New count of individuals, without those that were dropped
		SELECT LocationId, SUM(Density) as TotalDensity
		FROM densities
		GROUP BY LocationId
	),
	PsI as ( /* PsI = Pi because "Pi" is a reserved word */
		SELECT d.LocationId, ac.TotalDensity, d.SpeciesId,
			 (d.Density / ac.TotalDensity) * LOG(d.Density / ac.TotalDensity) as LogAdjRelAbund
		FROM densities d
		INNER JOIN newadj ac ON d.LocationId = ac.LocationId
		WHERE d.Density > 0
	),
	richness as (
		SELECT LocationId, COUNT(DISTINCT SpeciesId) as Richness
		FROM densities
		WHERE Density > 0
		GROUP BY LocationId
	),
	shannon as (
		SELECT DISTINCT LocationId, -SUM(LogAdjRelAbund)  OVER (PARTITION BY LocationId) as DiversityIndex, TotalDensity
		FROM PsI
	)
	SELECT  s.LocationId, l.LocationName, r.Richness, 
	CAST (s.DiversityIndex as DECIMAL(5,4)) as DiversityIndex,
	CAST( s.DiversityIndex / LOG(r.Richness) as DECIMAL(5,4) ) as Evenness
	FROM shannon s
	INNER JOIN dbo.Location l ON s.LocationId = l.LocationId
	INNER JOIN richness r ON s.LocationId = r.LocationId
	ORDER BY l.LocationName;

RETURN 0

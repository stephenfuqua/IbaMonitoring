CREATE PROCEDURE [dbo].[Results_BMDE]
AS
	WITH o as (
		SELECT ps.EventId, s.SpeciesId, ob.ObservationTypeId, COUNT(1) as ObservationCount
		FROM dbo.Observation ob
		INNER JOIN dbo.PointSurvey ps ON ob.EventId = ps.EventId
		INNER JOIN dbo.Species s ON ob.SpeciesId = s.SpeciesId
		WHERE ob.ObservationTypeId <> 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF'
		GROUP BY ps.EventId, s.SpeciesId, ob.ObservationTypeId
	)	
	SELECT 	
		'observation' as BasisOfRecord,
		CONVERT(varchar(42), hashbytes('sha1', cast(o.SpeciesId as varchar(40)) + cast(o.EventId as varchar(40))), 1) as CatalogNumber,
		SUBSTRING(s.ScientificName, 1, CHARINDEX(' ', s.ScientificName) - 1) as Genus,
		SUBSTRING(s.ScientificName, CHARINDEX(' ', s.ScientificName) + 1, LEN(s.ScientificName) - CHARINDEX(' ', s.ScientificName)+1) as SpecificEpithet,
		s.ScientificName,
		'North America' as Continent,
		'United States of America' as Country,
		'Minnesota' as StateProvince,
		sp.Latitude as DecimalLatitude,
		sp.Longitude as DecimalLongitude,
		'null' as CoordinateUncertainty,
		YEAR(ps.StartTime) as YearCollected,
		MONTH(ps.StartTime) as MonthCollected,
		DAY(ps.StartTime) as DayCollected,
		CAST((CAST(DATEPART(hh,ps.StartTime) as DECIMAL(4,2)) + CAST(DATEPART(N,ps.StartTime) as DECIMAL(4,2))/60) as DECIMAL(4,2)) as TimeCollected,
		DATEPART(dy,ps.StartTime) as  JulianDay,
		p.FirstName + ' ' + p.LastName as Collector,
		'decimal degrees' as OriginalCoordinateSystem,
		'Point Count' as ProtocolType,
		'Homayoun, T. Z. and R. B. Blair. 2009. Citizen-Science Monitoring of Landbirds in the Mississippi River Twin Cities Important Bird Area. Pages 607-616 in T.D., Rich, C. Arizmendi, D. Demarest and C. Thompson, editors. Tundra to Tropics: Connecting Birds, Habitats and People. Proceedings of the 4th International Partners in Flight Conference, 13-16 February 2008, McAllen, TX. Partners in Flight.' as ProtocolReference,
		'http://www.partnersinflight.org/pubs/McAllenProc/articles/PIF09_Monitoring/Homayoun_PIF09.pdf' as ProtocolURL,
		sp.LocationId as SurveyAreaIdentifier,
		sp.LocationName  as Locality,
		sv.LocationId as RouteIdentifier,
		ps.EventId as SamplingEventIdentifier,
		CAST((CAST(DATEPART(hh,ps.StartTime) as DECIMAL(4,2)) + CAST(DATEPART(N,ps.StartTime) as DECIMAL(4,2))/60) as DECIMAL(4,2)) as TimeObservationsStarted,
		CAST((CAST(DATEPART(hh,ps.EndTime) as DECIMAL(4,2)) + CAST(DATEPART(N,ps.EndTime) as DECIMAL(4,2))/60) as DECIMAL(4,2)) as TimeObservationsEnded,
		CAST(CAST(DATEDIFF(n, ps.StartTime, ps.EndTime) as DECIMAL(4,2))/60 as DECIMAL(4,2))  as DurationInHours,
		1 as NumberOfObservers,
		CASE o.ObservationTypeId WHEN '8E4055BC-7644-4670-8C1D-648BF81519D8' THEN '0 m' WHEN 'F7D5E189-233E-4B9E-B15A-4094640C3880' THEN '50 m' END as DistanceFromObserverMin,
		CASE o.ObservationTypeId WHEN '8E4055BC-7644-4670-8C1D-648BF81519D8' THEN '50 m' WHEN 'F7D5E189-233E-4B9E-B15A-4094640C3880' THEN 'Unlimited' END as DistanceFromObserverMax,
		o.ObservationCount,
		CONVERT(VARCHAR(10), ps.StartTime, 101) as ObservationDate,		
		'Yes' as AllIndividualsReported,
		s.CommonName as CommonName,
		'Restricted' as RecordPermissions,
		s.AlphaCode as SpeciesCode
	FROM o
	INNER JOIN dbo.PointSurvey ps ON o.EventId = ps.EventId
	INNER JOIN dbo.Location sp ON ps.LocationId = sp.LocationId
	INNER JOIN dbo.Species s ON o.SpeciesId = s.SpeciesId
	INNER JOIN dbo.SiteVisit sv ON ps.SiteVisitId = sv.EventId
	INNER JOIN dbo.Person p ON sv.ObserverId = p.PersonId
	WHERE sv.IsDataEntryComplete = 1
	AND s.ScientificName <> ''
	FOR XML RAW ('Observation'), ROOT('ObservationList'), ELEMENTS
RETURN 0

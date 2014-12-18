CREATE VIEW [dbo].[SamplingPoint] AS 
	SELECT LocationId,
		LocationName,
		Longitude,
		Latitude,
		ParentLocationId
	FROM Location 
	WHERE LocationTypeId = '501888ac-26af-43bd-a47d-ec7d61d42a7d';

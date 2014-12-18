CREATE VIEW [dbo].[Site] AS 
	SELECT LocationId,
		LocationName,
		Longitude,
		Latitude,
		CodeName
	FROM Location 
	WHERE LocationTypeId = '265f303c-f442-4c3b-a35c-0ec54aa65b70';

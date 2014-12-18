CREATE PROCEDURE [dbo].[Site_Save]
	@Id UNIQUEIDENTIFIER,
	@LocationName VARCHAR(250),
	@Latitude DECIMAL(9,6),
	@Longitude DECIMAL(9,6),
	@ParentLocationId UNIQUEIDENTIFIER = null,
	@CodeName VARCHAR(10)
AS
	SET NOCOUNT ON;

	UPDATE dbo.Location SET LocationName = @LocationName,
							Latitude = @Latitude,
							Longitude = @Longitude,
							ParentLocationId = @ParentLocationId,
							CodeName = @CodeName
	WHERE LocationId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.Location (LocationId, LocationName, Latitude, Longitude, LocationTypeId, ParentLocationId, CodeName)
		VALUES (@Id, @LocationName, @Latitude, @Longitude, '265f303c-f442-4c3b-a35c-0ec54aa65b70', @ParentLocationId, @CodeName);


RETURN 0
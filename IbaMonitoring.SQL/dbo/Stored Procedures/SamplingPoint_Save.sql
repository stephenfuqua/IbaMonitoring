CREATE PROCEDURE [dbo].[SamplingPoint_Save]
	@Id UNIQUEIDENTIFIER,
	@LocationName VARCHAR(250),
	@Latitude DECIMAL(9,6),
	@Longitude DECIMAL(9,6),
	@ParentLocationId UNIQUEIDENTIFIER
AS
	SET NOCOUNT ON;

	UPDATE dbo.Location SET LocationName = @LocationName,
							Latitude = @Latitude,
							Longitude = @Longitude,
							ParentLocationId = @ParentLocationId
	WHERE LocationId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.Location (LocationId, LocationName, Latitude, Longitude, LocationTypeId, ParentLocationId, CodeName)
		VALUES (@Id, @LocationName, @Latitude, @Longitude, '501888ac-26af-43bd-a47d-ec7d61d42a7d', @ParentLocationId, null);


RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[SamplingPoint_Save] TO [IbaAccounts]
    AS [dbo];


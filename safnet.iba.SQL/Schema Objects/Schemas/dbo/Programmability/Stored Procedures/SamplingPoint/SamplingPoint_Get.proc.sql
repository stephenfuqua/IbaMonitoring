CREATE PROCEDURE [dbo].[SamplingPoint_Get]
	@Id UNIQUEIDENTIFIER = null,
	@ParentId UNIQUEIDENTIFIER = null,
	@All BIT = null
AS
	SET NOCOUNT ON;

	SELECT LocationId,
		LocationName,
		Longitude,
		Latitude,
		ParentLocationId
	FROM dbo.SamplingPoint
	WHERE LocationId = @Id
		OR ParentLocationId = @ParentId
		OR @All = 1
	ORDER BY LocationName;
RETURN 0
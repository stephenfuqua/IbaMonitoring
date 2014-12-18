CREATE PROCEDURE [dbo].[Site_Get]
	@Id UNIQUEIDENTIFIER = null,
	@CodeName VARCHAR(10) = null,
	@All BIT = null
AS
	SET NOCOUNT ON;

	SELECT LocationId,
		LocationName,
		Longitude,
		Latitude,
		CodeName
	FROM dbo.Site 
	WHERE LocationId = @Id
		OR CodeName = @CodeName
		OR @All = 1;
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Site_Get] TO [IbaAccounts]
    AS [dbo];


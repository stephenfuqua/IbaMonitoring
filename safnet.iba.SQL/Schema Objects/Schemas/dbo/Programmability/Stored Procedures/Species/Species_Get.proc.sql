CREATE PROCEDURE [dbo].[Species_Get]
	@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@All BIT = 0
AS
	SET NOCOUNT ON;

	SELECT SpeciesId,
		AlphaCode,
		CommonName,
		ScientificName,
		WarningCount
	FROM dbo.Species
	WHERE SpeciesId = @Id
		OR @All = 1;
RETURN 0
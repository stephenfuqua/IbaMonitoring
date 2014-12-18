CREATE PROCEDURE [dbo].[Species_Save]
	@Id UNIQUEIDENTIFIER,
	@AlphaCode VARCHAR(25),
	@CommonName VARCHAR(100),
	@ScientificName VARCHAR(250),
	@WarningCount INT
AS
	SET NOCOUNT ON;

	UPDATE dbo.Species SET AlphaCode = @AlphaCode,
							CommonName = @CommonName,
							ScientificName = @ScientificName,
							WarningCount = @WarningCount
	WHERE SpeciesId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.Species (SpeciesId, AlphaCode, CommonName, ScientificName, WarningCount)
		VALUES (@Id, @AlphaCode, @CommonName, @ScientificName, @WarningCount);


RETURN 0
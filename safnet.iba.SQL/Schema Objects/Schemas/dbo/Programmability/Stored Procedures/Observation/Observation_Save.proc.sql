CREATE PROCEDURE [dbo].[Observation_Save]
	@Id BIGINT = 0,
	@EventId UNIQUEIDENTIFIER,
	@Comments VARCHAR(1000),
	@SpeciesCode VARCHAR(20),
	@ObservationTypeId UNIQUEIDENTIFIER
AS
	SET NOCOUNT ON;

	DECLARE @SpeciesId UNIQUEIDENTIFIER;
	SELECT @SpeciesId = SpeciesId FROM dbo.Species WHERE AlphaCode = @SpeciesCode;

	UPDATE dbo.Observation SET EventId = @EventId,
								ObservationTypeId = @ObservationTypeId,
								Comments = @Comments,
								SpeciesId = @SpeciesId
	WHERE ObservationId = @Id;

	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO dbo.Observation (EventId, SpeciesId, Comments, ObservationTypeId)
		VALUES (@EventId, @SpeciesId, @Comments, @ObservationTypeId)

		SELECT SCOPE_IDENTITY();
	END;
	ELSE
		SELECT @Id;

RETURN 0
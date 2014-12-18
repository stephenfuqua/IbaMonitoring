
CREATE PROCEDURE [dbo].[Observation_Delete_TopX]
	@EventId UNIQUEIDENTIFIER,
	@ObservationTypeId UNIQUEIDENTIFIER,
	@SpeciesCode VARCHAR(20),
	@TopXCount INT
AS
	SET NOCOUNT ON;
	
	DECLARE @SpeciesId UNIQUEIDENTIFIER;
	SELECT @SpeciesId = SpeciesId FROM dbo.Species WHERE AlphaCode = @SpeciesCode;

	DECLARE @sql NVARCHAR(4000);
	-- INT datatype will not allow SQL injection
	SELECT @sql = N'DELETE TOP(' + CAST(@TopXCount as NVARCHAR(10)) +N') FROM dbo.Observation WHERE EventId = @pEventId AND ObservationTypeId = @pObservationTypeId AND SpeciesId = @pSpeciesId;';
	exec sp_executesql @sql, N'@pEventId UNIQUEIDENTIFIER, @pObservationTypeId UNIQUEIDENTIFIER, @pSpeciesId UNIQUEIDENTIFIER',
		@pEventId = @EventId, @pObservationTypeId = @ObservationTypeId, @pSpeciesId = @SpeciesId;

RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Observation_Delete_TopX] TO [IbaAccounts]
    AS [dbo];


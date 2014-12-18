CREATE PROCEDURE [dbo].[Observation_Delete]
	@Id BIGINT 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.Observation WHERE ObservationId = @Id;
RETURN 0
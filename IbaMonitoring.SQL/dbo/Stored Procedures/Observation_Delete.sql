CREATE PROCEDURE [dbo].[Observation_Delete]
	@Id BIGINT 
AS
	DELETE FROM dbo.Observation WHERE ObservationId = @Id;
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Observation_Delete] TO [IbaAccounts]
    AS [dbo];


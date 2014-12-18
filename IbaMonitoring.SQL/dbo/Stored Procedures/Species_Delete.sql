CREATE PROCEDURE [dbo].[Species_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.Species WHERE SpeciesId = @Id;
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Species_Delete] TO [IbaAccounts]
    AS [dbo];


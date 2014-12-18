CREATE PROCEDURE [dbo].[SamplingPoint_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.Location WHERE LocationId = @Id;
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[SamplingPoint_Delete] TO [IbaAccounts]
    AS [dbo];


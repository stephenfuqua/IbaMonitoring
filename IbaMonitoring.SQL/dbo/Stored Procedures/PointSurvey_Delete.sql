CREATE PROCEDURE [dbo].[PointSurvey_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.PointSurvey WHERE EventId = @Id;
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[PointSurvey_Delete] TO [IbaAccounts]
    AS [dbo];


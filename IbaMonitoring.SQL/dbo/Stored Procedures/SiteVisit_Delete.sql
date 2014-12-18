
CREATE PROCEDURE [dbo].[SiteVisit_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.SiteVisit WHERE EventId = @Id;
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[SiteVisit_Delete] TO [IbaAccounts]
    AS [dbo];


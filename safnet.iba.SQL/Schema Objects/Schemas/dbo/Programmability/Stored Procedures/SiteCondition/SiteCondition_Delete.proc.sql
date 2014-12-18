CREATE PROCEDURE [dbo].[SiteCondition_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.SiteCondition WHERE ConditionId = @Id;
RETURN 0
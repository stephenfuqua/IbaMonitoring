CREATE PROCEDURE [dbo].[SiteCondition_Get]
	@Id UNIQUEIDENTIFIER = null,
	@SiteVisitId UNIQUEIDENTIFIER = null,
	@All BIT = null
AS
	SET NOCOUNT ON;

	SELECT s.ConditionId,
		s.Scale,
		s.SiteVisitId,
		s.Sky,
		s.Temperature,
		s.Wind
	FROM dbo.SiteCondition s
	WHERE ConditionId = @Id
		OR SiteVisitId = @SiteVisitId
		OR @All = 1;
RETURN 0
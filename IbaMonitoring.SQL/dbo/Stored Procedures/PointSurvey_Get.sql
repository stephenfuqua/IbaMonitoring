CREATE PROCEDURE [dbo].[PointSurvey_Get]
	@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@SiteVisitId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@All BIT = 0
AS
	SET NOCOUNT ON;

	SELECT EventId as Id,
		LocationId,
		SiteVisitId,
		NoiseCode,
		StartTime,
		EndTime
	FROM dbo.PointSurvey
	WHERE EventId = @Id
		OR SiteVisitId = @SiteVisitId
		OR @All = 1;
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[PointSurvey_Get] TO [IbaAccounts]
    AS [dbo];


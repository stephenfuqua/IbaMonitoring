CREATE PROCEDURE [dbo].[PointSurvey_Save]
	@Id UNIQUEIDENTIFIER,
	@LocationId UNIQUEIDENTIFIER,
	@StartTime DATETIME,
	@EndTime DATETIME,
	@NoiseCode TINYINT,
	@SiteVisitId UNIQUEIDENTIFIER
AS
	SET NOCOUNT ON;

	UPDATE dbo.PointSurvey SET 
							LocationId = @LocationId,
							StartTime = @StartTime,
							EndTime = @EndTime,
							NoiseCode = @NoiseCode,
							SiteVisitId = @SiteVisitId
	WHERE EventId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.PointSurvey (EventId, LocationId, StartTime, EndTime, NoiseCode, SiteVisitId)
		VALUES (@Id, @LocationId, @StartTime, @EndTime, @NoiseCode, @SiteVisitId);


RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[PointSurvey_Save] TO [IbaAccounts]
    AS [dbo];


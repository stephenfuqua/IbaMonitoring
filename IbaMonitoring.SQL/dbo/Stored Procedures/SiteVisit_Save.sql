
CREATE PROCEDURE [dbo].[SiteVisit_Save]
	@Id UNIQUEIDENTIFIER,
	@IsDataEntryComplete BIT,
	@LocationId UNIQUEIDENTIFIER,
	@StartTime DATETIME,
	@EndTime DATETIME,
	@StartConditionId UNIQUEIDENTIFIER,
	@EndConditionId UNIQUEIDENTIFIER,
	@ObserverId UNIQUEIDENTIFIER,
	@RecorderId UNIQUEIDENTIFIER,
	@Comments VARCHAR(1000)
AS
	SET NOCOUNT ON;

	UPDATE dbo.SiteVisit SET IsDataEntryComplete = @IsDataEntryComplete,
							LocationId = @LocationId,
							StartTime = @StartTime,
							EndTime = @EndTime,
							StartConditionId = @StartConditionId,
							EndConditionId = @EndConditionId,
							ObserverId = @ObserverId,
							RecorderId = @RecorderId,
							Comments = @Comments
	WHERE EventId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.SiteVisit (EventId, IsDataEntryComplete, LocationId, StartTime, EndTime,
			StartConditionId, EndConditionId, ObserverId, RecorderId, Comments)
		VALUES (@Id, @IsDataEntryComplete, @LocationId, @StartTime, @EndTime, @StartConditionId, @EndConditionId,
			@ObserverId, @RecorderId, @Comments);


RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[SiteVisit_Save] TO [IbaAccounts]
    AS [dbo];


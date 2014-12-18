CREATE PROCEDURE [dbo].[SiteVisit_Get]
	@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@LocationId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@All BIT = 0
AS
	SET NOCOUNT ON;

	SELECT EventId as Id,
		IsDataEntryComplete,
		LocationId,
		StartTime,
		EndTime,
		StartConditionId,
		EndConditionId,
		ObserverId,
		RecorderId,
		Comments
	FROM dbo.SiteVisit
	WHERE EventId = @Id
		OR LocationId = @LocationId
		OR @All = 1;
RETURN 0

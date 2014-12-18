
CREATE PROCEDURE [dbo].[SiteCondition_Save]
	@Id UNIQUEIDENTIFIER,
	@Temperature INT,
	@Scale CHAR(1),
	@Wind TINYINT,
	@Sky TINYINT,
	@SiteVisitId UNIQUEIDENTIFIER
AS
	SET NOCOUNT ON;

	UPDATE dbo.SiteCondition SET Temperature = @Temperature,
								 Scale = @Scale,
								 Wind = @Wind,
								 Sky = @Sky,
								 SiteVisitId = @SiteVisitId
	WHERE ConditionId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.SiteCondition (ConditionId, Temperature, Scale, Wind, Sky, SiteVisitId)
		VALUES (@Id, @Temperature, @Scale, @Wind, @Sky, @SiteVisitId);

		
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[SiteCondition_Save] TO [IbaAccounts]
    AS [dbo];


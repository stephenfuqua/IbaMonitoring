﻿CREATE PROCEDURE [dbo].[PointSurvey_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.PointSurvey WHERE EventId = @Id;
RETURN 0
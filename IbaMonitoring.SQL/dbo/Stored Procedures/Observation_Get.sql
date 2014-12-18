
CREATE PROCEDURE [dbo].[Observation_Get]
	@Id BIGINT = 0,
	@EventId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@ObservationTypeId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@All BIT = 0
AS
	SET NOCOUNT ON;

	SELECT p.EventId,
		p.Comments,
		s.AlphaCode as SpeciesCode,
		p.ObservationId as Id
	FROM dbo.Observation p
	INNER JOIN dbo.Species s ON p.SpeciesId = s.SpeciesId
	WHERE ((p.ObservationId = @Id OR @Id = 0)
			AND (p.EventId = @EventId  OR @EventId = '00000000-0000-0000-0000-000000000000'))
		AND ((p.ObservationTypeId = @ObservationTypeId)
			OR (@ObservationTypeId = '00000000-0000-0000-0000-000000000000'));
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Observation_Get] TO [IbaAccounts]
    AS [dbo];


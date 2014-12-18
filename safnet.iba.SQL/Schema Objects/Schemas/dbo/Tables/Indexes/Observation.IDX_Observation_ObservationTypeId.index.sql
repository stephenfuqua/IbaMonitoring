CREATE NONCLUSTERED INDEX IDX_Observation_ObservationTypeId
ON [dbo].[Observation] ([ObservationTypeId])
INCLUDE ([EventId],[SpeciesId])
GO

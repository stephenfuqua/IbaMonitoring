CREATE TABLE [dbo].[Observation] (
    [ObservationId]     BIGINT           IDENTITY (1, 1) NOT NULL,
    [EventId]           UNIQUEIDENTIFIER CONSTRAINT [DF_Observation__EventId] DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [SpeciesId]         UNIQUEIDENTIFIER NOT NULL,
    [Comments]          VARCHAR (1000)   NULL,
    [ObservationTypeId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Observation] PRIMARY KEY CLUSTERED ([ObservationId] ASC),
    CONSTRAINT [FK_Observation__Lookup] FOREIGN KEY ([ObservationTypeId]) REFERENCES [dbo].[Lookup] ([LookupId])
);


GO
CREATE NONCLUSTERED INDEX [IDX_Observation_ObservationTypeId]
    ON [dbo].[Observation]([ObservationTypeId] ASC)
    INCLUDE([EventId], [SpeciesId]);


GO
CREATE NONCLUSTERED INDEX [idx_DCh_1214_1213_Observation]
    ON [dbo].[Observation]([EventId] ASC)
    INCLUDE([SpeciesId], [ObservationTypeId]);


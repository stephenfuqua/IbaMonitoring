﻿CREATE TABLE [dbo].[Observation]
(	
	ObservationId BIGINT IDENTITY(1,1) NOT NULL,
	EventId UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Observation__EventId DEFAULT ('00000000-0000-0000-0000-000000000000'),
	SpeciesId UNIQUEIDENTIFIER NOT NULL,
	Comments VARCHAR(1000) NULL,
	ObservationTypeId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_Observation PRIMARY KEY CLUSTERED  (ObservationId)
)
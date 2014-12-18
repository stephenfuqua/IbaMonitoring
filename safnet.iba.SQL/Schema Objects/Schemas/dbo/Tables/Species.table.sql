CREATE TABLE [dbo].[Species]
(
	SpeciesId UNIQUEIDENTIFIER NOT NULL,
	AlphaCode VARCHAR(25) NOT NULL,
	CommonName VARCHAR(100) NULL,
	ScientificName VARCHAR(250) NULL,
	WarningCount INT NOT NULL CONSTRAINT DF_Species__WarningCount DEFAULT (12),
	UseForCommunityMeasures BIT NOT NULL CONSTRAINT DF_UseForCommunityMeasures DEFAULT((0)),
	[MigrationGuild] [varchar](3) NULL,
	[ConservationGuild] [varchar](1) NULL,
	CONSTRAINT PK_Species PRIMARY KEY CLUSTERED (SpeciesId)
);


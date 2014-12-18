CREATE TABLE [dbo].[Species] (
    [SpeciesId]               UNIQUEIDENTIFIER NOT NULL,
    [AlphaCode]               VARCHAR (25)     NOT NULL,
    [CommonName]              VARCHAR (100)    NULL,
    [ScientificName]          VARCHAR (250)    NULL,
    [WarningCount]            INT              CONSTRAINT [DF_Species__WarningCount] DEFAULT ((12)) NOT NULL,
    [UseForCommunityMeasures] BIT              CONSTRAINT [DF_UseForCommunityMeasures] DEFAULT ((0)) NOT NULL,
    [MigrationGuild]          VARCHAR (3)      NULL,
    [ConservationGuild]       VARCHAR (1)      NULL,
    CONSTRAINT [PK_Species] PRIMARY KEY CLUSTERED ([SpeciesId] ASC)
);


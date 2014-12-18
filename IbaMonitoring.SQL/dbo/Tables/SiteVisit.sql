CREATE TABLE [dbo].[SiteVisit] (
    [EventId]             UNIQUEIDENTIFIER NOT NULL,
    [IsDataEntryComplete] BIT              CONSTRAINT [DF_PointSurvey__IsDataEntryComplete] DEFAULT ((0)) NOT NULL,
    [LocationId]          UNIQUEIDENTIFIER NOT NULL,
    [StartTime]           DATETIME         NOT NULL,
    [EndTime]             DATETIME         NOT NULL,
    [StartConditionId]    UNIQUEIDENTIFIER NOT NULL,
    [EndConditionId]      UNIQUEIDENTIFIER NULL,
    [ObserverId]          UNIQUEIDENTIFIER NULL,
    [RecorderId]          UNIQUEIDENTIFIER NULL,
    [Comments]            VARCHAR (1000)   NULL,
    [StartYear]           AS               (datepart(year,[StartTime])),
    CONSTRAINT [PK_SiteVisit] PRIMARY KEY CLUSTERED ([EventId] ASC)
);


CREATE TABLE [dbo].[PointSurvey] (
    [EventId]     UNIQUEIDENTIFIER NOT NULL,
    [SiteVisitId] UNIQUEIDENTIFIER NULL,
    [LocationId]  UNIQUEIDENTIFIER NOT NULL,
    [StartTime]   DATETIME         NOT NULL,
    [EndTime]     DATETIME         NOT NULL,
    [NoiseCode]   TINYINT          NULL,
    CONSTRAINT [PK_PointSurvey] PRIMARY KEY CLUSTERED ([EventId] ASC)
);


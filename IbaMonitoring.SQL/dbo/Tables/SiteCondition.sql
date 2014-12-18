CREATE TABLE [dbo].[SiteCondition] (
    [ConditionId] UNIQUEIDENTIFIER NOT NULL,
    [Temperature] INT              NOT NULL,
    [Scale]       CHAR (1)         NOT NULL,
    [Wind]        TINYINT          NOT NULL,
    [Sky]         TINYINT          NOT NULL,
    [SiteVisitId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SiteCondition] PRIMARY KEY CLUSTERED ([ConditionId] ASC)
);


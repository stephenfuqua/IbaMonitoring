﻿CREATE TABLE [dbo].[SiteBoundary] (
    [SiteBoundaryId] INT              IDENTITY (1, 1) NOT NULL,
    [SiteId]         UNIQUEIDENTIFIER NOT NULL,
    [Latitude]       DECIMAL (9, 6)   NOT NULL,
    [Longitude]      DECIMAL (9, 6)   NOT NULL,
    [VertexSequence] INT              NOT NULL,
    CONSTRAINT [PK_SiteBoundary] PRIMARY KEY CLUSTERED ([SiteBoundaryId] ASC)
);


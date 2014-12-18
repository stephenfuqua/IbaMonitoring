﻿CREATE TABLE [dbo].[Lookup] (
    [LookupId]       UNIQUEIDENTIFIER NOT NULL,
    [Value]          VARCHAR (250)    NOT NULL,
    [ParentLookupId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED ([LookupId] ASC)
);


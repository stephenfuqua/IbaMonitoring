CREATE TABLE [dbo].[Location] (
    [LocationId]       UNIQUEIDENTIFIER NOT NULL,
    [LocationName]     VARCHAR (250)    NOT NULL,
    [Latitude]         DECIMAL (9, 6)   NULL,
    [Longitude]        DECIMAL (9, 6)   NULL,
    [LocationTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [ParentLocationId] UNIQUEIDENTIFIER NULL,
    [CodeName]         VARCHAR (10)     NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId] ASC),
    CONSTRAINT [FK_Location__Lookup] FOREIGN KEY ([LocationTypeId]) REFERENCES [dbo].[Lookup] ([LookupId])
);


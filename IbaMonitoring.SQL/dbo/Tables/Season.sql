CREATE TABLE [dbo].[Season] (
    [SeasonId]          UNIQUEIDENTIFIER NOT NULL,
    [SeasonDescription] VARCHAR (50)     NOT NULL,
    [IbaProgramId]      UNIQUEIDENTIFIER NOT NULL,
    [StartWeek]         DATETIME         NULL,
    [EndWeek]           DATETIME         NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED ([SeasonId] ASC),
    CONSTRAINT [FK_Season__IbaProgram] FOREIGN KEY ([IbaProgramId]) REFERENCES [dbo].[IbaProgram] ([IbaProgramId])
);


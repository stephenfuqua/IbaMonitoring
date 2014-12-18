CREATE TABLE [dbo].[IbaProgram] (
    [IbaProgramId]       UNIQUEIDENTIFIER NOT NULL,
    [ProgramDescription] VARCHAR (1000)   NULL,
    CONSTRAINT [PK_IbaProgram] PRIMARY KEY CLUSTERED ([IbaProgramId] ASC)
);


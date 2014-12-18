CREATE TABLE [dbo].[Person] (
    [PersonId]       UNIQUEIDENTIFIER NOT NULL,
    [FirstName]      VARCHAR (50)     NOT NULL,
    [LastName]       VARCHAR (50)     NOT NULL,
    [OpenId]         VARCHAR (250)    NULL,
    [EmailAddress]   VARCHAR (250)    NULL,
    [PhoneNumber]    VARCHAR (15)     NULL,
    [PersonStatus]   SMALLINT         CONSTRAINT [DF_Person_PersonStatus] DEFAULT ((0)) NOT NULL,
    [PersonRole]     SMALLINT         CONSTRAINT [DF_Person_PersonRole] DEFAULT ((0)) NOT NULL,
    [HasBeenTrained] BIT              CONSTRAINT [DF_Person_HasBeenTrained] DEFAULT ((0)) NOT NULL,
    [HasClipboard]   BIT              CONSTRAINT [DF_Person_HasClipboard] DEFAULT ((0)) NOT NULL,
    [Address1]       VARCHAR (50)     NULL,
    [Address2]       VARCHAR (50)     NULL,
    [City]           VARCHAR (50)     NULL,
    [State]          VARCHAR (50)     NULL,
    [ZipCode]        VARCHAR (10)     NULL,
    [Country]        VARCHAR (50)     NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([PersonId] ASC)
);


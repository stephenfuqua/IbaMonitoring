CREATE TABLE [dbo].[Person]
(
	PersonId UNIQUEIDENTIFIER NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	OpenId VARCHAR(250) NULL,
	EmailAddress VARCHAR(250) NULL,
	PhoneNumber VARCHAR(15) NULL,
	PersonStatus SMALLINT NOT NULL CONSTRAINT DF_Person_PersonStatus DEFAULT 0,
	PersonRole SMALLINT NOT NULL CONSTRAINT DF_Person_PersonRole DEFAULT 0,
	HasBeenTrained BIT NOT NULL CONSTRAINT DF_Person_HasBeenTrained DEFAULT 0,
	HasClipboard BIT NOT NULL CONSTRAINT DF_Person_HasClipboard DEFAULT 0,
	Address1 varchar(50),
	Address2 varchar(50),
	City varchar(50),
	[State] varchar(50),
	ZipCode varchar(10),
	Country varchar(50)
	CONSTRAINT PK_Person PRIMARY KEY CLUSTERED (PersonId)
)

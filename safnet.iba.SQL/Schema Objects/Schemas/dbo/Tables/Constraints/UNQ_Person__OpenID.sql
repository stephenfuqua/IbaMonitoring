ALTER TABLE [dbo].[Person]
	ADD CONSTRAINT [UNQ_Person__OpenID] 
	UNIQUE (OpenId)

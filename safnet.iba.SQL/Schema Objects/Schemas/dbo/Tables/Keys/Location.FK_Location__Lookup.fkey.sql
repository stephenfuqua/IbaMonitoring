ALTER TABLE dbo.Location
	ADD CONSTRAINT [FK_Location__Lookup] 
	FOREIGN KEY (LocationTypeId)
	REFERENCES dbo.Lookup (LookupId);	


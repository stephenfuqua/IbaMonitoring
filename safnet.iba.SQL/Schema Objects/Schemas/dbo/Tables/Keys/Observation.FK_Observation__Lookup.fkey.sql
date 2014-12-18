ALTER TABLE dbo.Observation
	ADD CONSTRAINT [FK_Observation__Lookup] 
	FOREIGN KEY (ObservationTypeId)
	REFERENCES dbo.Lookup (LookupId);	


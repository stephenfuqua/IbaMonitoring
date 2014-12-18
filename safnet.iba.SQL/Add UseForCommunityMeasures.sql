	--alter table Species add UseForCommunityMeasures BIT NOT NULL CONSTRAINT DF_UseForCommunityMeasures DEFAULT((0))
	
	
	update Species set UseForCommunityMeasures = 1;
	update Species set UseForCommunityMeasures = 0 where CommonName like 'Unkn%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Duck%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Merganser%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Goose%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Heron%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Egret%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%piper%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Teal%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Bittern%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Gull%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Crane%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Pelican%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Cormorant%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Coot%'
	update Species set UseForCommunityMeasures = 0 where CommonName like '%Mallard%'
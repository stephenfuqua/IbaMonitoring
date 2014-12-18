/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* Pre-populate the Lookup Table */

-- Location types
WITH ins as (
	SELECT 'b6f90fee-3729-4eb8-a4be-f37f240a501c' as LookupId, null as ParentLookupId, 'Location' as Value UNION
	SELECT '265f303c-f442-4c3b-a35c-0ec54aa65b70' as LookupId, 'b6f90fee-3729-4eb8-a4be-f37f240a501c' as ParentLookupId, 'Site' as Value UNION
	SELECT '501888ac-26af-43bd-a47d-ec7d61d42a7d' as LookupId, 'b6f90fee-3729-4eb8-a4be-f37f240a501c' as ParentLookupId, 'Sampling Point' as Value 
)
INSERT INTO dbo.Lookup (LookupId, ParentLookupId, Value)
SELECT LookupId, ParentLookupId, Value FROM ins WHERE NOT EXISTS (SELECT 1 FROM dbo.Lookup WHERE LookupId = ins.LookupId);

-- Observation Types
WITH ins as (
	SELECT '45185696-2084-4EB9-9441-21FBBF4DFE09' as LookupId, null as ParentLookupId, 'Observation' as Value UNION
	SELECT '8E4055BC-7644-4670-8C1D-648BF81519D8' as LookupId, '45185696-2084-4EB9-9441-21FBBF4DFE09' as ParentLookupId, 'Less than 50m' as Value UNION
	SELECT 'F7D5E189-233E-4B9E-B15A-4094640C3880' as LookupId, '45185696-2084-4EB9-9441-21FBBF4DFE09' as ParentLookupId, 'Greater than 50m' as Value UNION
	SELECT 'CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF' as LookupId, '45185696-2084-4EB9-9441-21FBBF4DFE09' as ParentLookupId, 'Supplemental' as Value 
)
INSERT INTO dbo.Lookup (LookupId, ParentLookupId, Value)
SELECT LookupId, ParentLookupId, Value FROM ins WHERE NOT EXISTS (SELECT 1 FROM dbo.Lookup WHERE LookupId = ins.LookupId);

-- Event Types
--WITH ins as (
--	SELECT '93653E10-AAED-40D3-8853-763949F5EC79' as LookupId, 'F7D5E189-233E-4B9E-B15A-4094640C3880' as ParentLookupId, 'Site Visit' as Value UNION
--	SELECT 'F4B5ED66-8309-47EB-844B-2D5773975A16' as LookupId, 'F7D5E189-233E-4B9E-B15A-4094640C3880' as ParentLookupId, 'Point Survey' as Value 
--)
--INSERT INTO dbo.Lookup (LookupId, ParentLookupId, Value)
--SELECT LookupId, ParentLookupId, Value FROM ins WHERE NOT EXISTS (SELECT 1 FROM dbo.Lookup WHERE LookupId = ins.LookupId);


/* Stored Proc Permissions */
DECLARE procs CURSOR FOR select [name] from sys.objects where type= 'p'
DECLARE @name as varchar(250)
DECLARE @stmt as varchar(1000)

OPEN procs

FETCH NEXT FROM procs INTO @name

WHILE @@FETCH_STATUS = 0
BEGIN

     SET @stmt = 'GRANT EXECUTE ON ' + @name + ' TO [IbaAccounts]'
     EXEC(@stmt)

     PRINT @stmt

     FETCH NEXT FROM procs INTO @name

END

CLOSE procs
DEALLOCATE procs 


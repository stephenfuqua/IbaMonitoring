﻿CREATE PROCEDURE [dbo].[Person_Delete]
	@Id UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON;

	DELETE FROM dbo.Person WHERE PersonId = @Id;
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Person_Delete] TO [IbaAccounts]
    AS [dbo];

